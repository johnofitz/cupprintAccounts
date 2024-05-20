
namespace cupprintAccounts
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePageView), typeof(HomePageView));
            Routing.RegisterRoute(nameof(PopUpView), typeof(PopUpView));
            Routing.RegisterRoute(nameof(InvoicePageView), typeof(InvoicePageView));
        }
    }
}
