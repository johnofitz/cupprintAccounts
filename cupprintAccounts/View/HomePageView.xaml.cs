
namespace cupprintAccounts.View;

public partial class HomePageView : ContentPage
{


	public HomePageView(HomePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}