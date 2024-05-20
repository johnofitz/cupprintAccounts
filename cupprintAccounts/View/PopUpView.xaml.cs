namespace cupprintAccounts.View;

using Mopups.Interfaces;
public partial class PopUpView
{
	public PopUpView(PopUpViewModel viewModel)
	{
        InitializeComponent();
		BindingContext = viewModel;
	}
}