
namespace cupprintAccounts.View;

public partial class InvoicePageView : ContentPage
{
	public InvoicePageView(InvoiceViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}