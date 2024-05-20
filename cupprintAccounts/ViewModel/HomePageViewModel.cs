using Mopups.Services;


namespace cupprintAccounts.ViewModel
{
    public partial class HomePageViewModel : ParentViewModel
    {
        // Short collection of Invoice company names
        public ObservableCollection<string> InvoiceCompany { get; set; } = new();

        // File path
        [ObservableProperty]
        private string? selectedPath;

        // Company selected
        [ObservableProperty]
        private string? selectedCompany;


        // Generate Companys on startup
        public HomePageViewModel()
        {
            Heading = "";
            GetInvoices();
        }

        /*
         Method for file picker restricten to excel file types
         */
        [RelayCommand]
        public async Task GetFilePathAsync()
        {
            try
            {
                // Define allowed file types for Excel files
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "com.microsoft.excel.xls", "org.openxmlformats.spreadsheetml.sheet" } }, // iOS UTI
                    { DevicePlatform.Android, new[] { "application/vnd.ms-excel", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" } }, // MIME types
                    { DevicePlatform.WinUI, new[] { ".xls", ".xlsx" } }, // File extensions
                    { DevicePlatform.MacCatalyst, new[] { "com.microsoft.excel.xls", "org.openxmlformats.spreadsheetml.sheet" } } // MacCatalyst UTI
                });

                var options = new PickOptions
                {
                    FileTypes = customFileType
                };

                // Show the file picker dialog
                var result = await FilePicker.PickAsync(options);

                if (result != null)
                {
                    // Get the full file path
                    SelectedPath = result.FullPath;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Command that instantiates a Modal pop up
        /// </summary>
        [RelayCommand]
        public async Task GoToSelectionAsync()
        {
            try
            {
                if (!File.Exists(SelectedPath))
                {
                    await Shell.Current.DisplayAlert("Error", "The selected file path is invalid.", "OK");
                    return;
                }
                else if (string.IsNullOrEmpty(SelectedCompany))
                {
                    await Shell.Current.DisplayAlert("Error", "Invoice company is blank.", "OK");
                    return;
                }

                InvoiceInfo invoiceInfo = new()
                {
                   InvoiceType = SelectedCompany,
                   SelectedPath = SelectedPath
                };

                // Push a new PopUpView to the navigation stack.
                await MopupService.Instance.PushAsync(new PopUpView(new PopUpViewModel(invoiceInfo)));
                
            }
            catch(Exception ex){
                await Shell.Current.DisplayAlert("Error!",ex.Message, "OK");
            }
        }

        // Ideally from database TODO
        private void GetInvoices()
        {
            InvoiceCompany.Add("UPS");
            InvoiceCompany.Add("STL");
            InvoiceCompany.Add("Eoin Gavin");
            InvoiceCompany.Add("DB Schenker");
        }
    }
}
