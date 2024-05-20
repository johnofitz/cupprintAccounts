
using Mopups.Services;

namespace cupprintAccounts.ViewModel
{
    public partial class PopUpViewModel: ParentViewModel
    {
        // Object instantiated for passing prop from PopupModel
        public InvoiceInfo? InvoiceInfo { get; }

        // Retrive data on start
        public PopUpViewModel(InvoiceInfo invoiceInfo) { 
            InvoiceInfo = invoiceInfo;
        }

        // Command to move to Invoice reconciliation page
        [RelayCommand]
        public async Task GoToInvoiceAsync()
        {
            await MopupService.Instance.PopAsync();
            await Shell.Current.GoToAsync(nameof(InvoicePageView), true);
        }


    }
}
