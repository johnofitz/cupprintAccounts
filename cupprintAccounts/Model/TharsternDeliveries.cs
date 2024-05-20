using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace cupprintAccounts.Model
{
 
    [Keyless]
    [Table("cpViewInvoiceDeliveries")]
    public class TharsternDeliveries
    {
        public string? DelNoteNumber { get; set; }

        public string? DeliveryName { get; set; }

        public string? DeliveryContact { get; set; }

        public DateTime DelivereyDate { get; set; }

        public DateTime ReqDate { get; set; }

        public string OrderNo { get; set; }

        public string? DeliveryCountry { get; set; }

        public string? DelConsignType { get; set; }

        public string? ConsignType { get; set; }

        public string NumberOfItems { get; set; }

        public string? Location { get; set; }


    }
}
