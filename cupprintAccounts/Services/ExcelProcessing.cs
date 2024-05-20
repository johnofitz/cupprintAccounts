using ClosedXML.Excel; 
using System.Text.RegularExpressions;

namespace cupprintAccounts.Services
{


        public partial class ExcelProcessor
        {
            protected int JobIndex;
            protected int BaseRateIndex;
            protected int QuantiityIndex;
            protected int InvoiceNoIndex;
            protected int DownloadDateIndex;
            protected int DeliveryDateIndex;

            public List<UpsInvoice> ProcessUpsExcel(string filePath, List<int> cellNumbers, string invoiceType)
            {
                List<UpsInvoice> dataList = new();
                // Load Excel file using ClosedXML
                using (XLWorkbook workbook = new(filePath))
                {
                    // Assuming its the first worksheet
                    IXLWorksheet worksheet = workbook.Worksheet(1);

                    // Determine the column index for each required column with ups
                    // if UPS ever changes these will need to be altered
                    if (invoiceType == "UPS")
                    {
                        DownloadDateIndex = cellNumbers[0];
                        InvoiceNoIndex = cellNumbers[1];
                        DeliveryDateIndex = cellNumbers[2];
                        JobIndex = cellNumbers[3];
                        QuantiityIndex = cellNumbers[4];
                        BaseRateIndex = cellNumbers[5];
                    }

                    // Get the count of used rows
                    int rowCount = worksheet.RowsUsed().Count();
                    // Excel is not 0th index 
                    // Dictionary to store job number as key and corresponding UpsInvoice object as value
                    Dictionary<int, UpsInvoice> jobDictionary = new();

                    // Excel is not 0th index 
                    for (int row = 1; row <= rowCount; row++)
                    {
                        string jobNumberCell = worksheet.Cell(row, JobIndex).Value.ToString();

                        if (invoiceType == "UPS")
                        {
                            // Check if the job number cell contains a number
                            if (MyRegex().IsMatch(jobNumberCell))
                            {
                                // Extract only the numeric part of the job number
                                int jobNumber = int.Parse(MyRegex().Match(jobNumberCell).Value);

                                // Create or update UpsInvoice object in the dictionary based on parcel count
                                UpsInvoice invoice = new()
                                {
                                    InvoiceDownLoadDate = worksheet.Cell(row, DownloadDateIndex).Value.ToString(),
                                    InvoiceNumber = worksheet.Cell(row, InvoiceNoIndex).Value.ToString(),
                                    DeliveryDate = worksheet.Cell(row, DeliveryDateIndex).Value.ToString(),
                                    JobNumber = jobNumber.ToString(),
                                    Parcels = worksheet.Cell(row, QuantiityIndex).Value.ToString(),
                                    BaseRate = worksheet.Cell(row, BaseRateIndex).Value.ToString()
                                };

                                if (!jobDictionary.ContainsKey(jobNumber))
                                {
                                    jobDictionary.Add(jobNumber, invoice);
                                }
                                else
                                {
                                    // If job number already exists, compare parcel counts and update if current parcel count is higher
                                    if (int.Parse(invoice.Parcels) > int.Parse(jobDictionary[jobNumber].Parcels))
                                    {
                                        jobDictionary[jobNumber] = invoice;
                                    }
                                }
                            }
                        }
                    }

                    // Add all UpsInvoice objects from the dictionary to dataList
                    dataList.AddRange(jobDictionary.Values);

                }
                return dataList;
            }

            [GeneratedRegex(@"\d+")]
            private static partial Regex MyRegex();



            // Function to output the list to Excel
            public void OutputToExcel(List<UpsReconcile> dataList, string filePath)
            {
                var workbook = new XLWorkbook();



                var worksheet = workbook.Worksheets.Add("UpsInvoices");

                worksheet.Column(1).Width = 20;
                worksheet.Column(2).Width = 20;
                worksheet.Column(3).Width = 20;
                worksheet.Column(4).Width = 20;
                worksheet.Column(5).Width = 20;
                worksheet.Column(6).Width = 20;
                worksheet.Column(7).Width = 20;
                worksheet.SetAutoFilter(true);

                // Add headers
                worksheet.Cell(1, 1).Value = "Delivery Country";
                worksheet.Cell(1, 2).Value = "Thar JobNo";
                worksheet.Cell(1, 3).Value = "UPS JobNo";
                worksheet.Cell(1, 4).Value = "Thar Parcels";
                worksheet.Cell(1, 5).Value = "UPS Parcels";
                worksheet.Cell(1, 6).Value = "UPS Base Rate";
                worksheet.Cell(1, 7).Value = "Rate Card";



                // Populate data
                int row = 2;
                foreach (UpsReconcile invoice in dataList)
                {
                    worksheet.Cell(row, 1).Value = invoice.TharCountry;
                    worksheet.Cell(row, 2).Value = invoice.TharJob;
                    worksheet.Cell(row, 3).Value = invoice.UpsJobNumber;
                    worksheet.Cell(row, 4).Value = invoice.TharQuantity;
                    worksheet.Cell(row, 5).Value = invoice.UpsParcels;
                    worksheet.Cell(row, 6).Value = invoice.UpsBaseRate;
                    worksheet.Cell(row, 7).Value = invoice.RateCard;
                    row++;
                }
                // Save the Excel file
                workbook.SaveAs(filePath);

            }
        }

    }
