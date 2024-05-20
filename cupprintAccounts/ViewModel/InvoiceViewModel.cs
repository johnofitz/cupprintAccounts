
namespace cupprintAccounts.ViewModel
{
 
    public partial class InvoiceViewModel: ParentViewModel
    {

     

        public InvoiceViewModel() {

            _ = LoadDataAsync();
        }


        //
        private async Task LoadDataAsync()
        {
            IsBusy = true;

            // Simulate a delay
            await Task.Delay(3000);

            IsBusy = false;
        }
    }
}
