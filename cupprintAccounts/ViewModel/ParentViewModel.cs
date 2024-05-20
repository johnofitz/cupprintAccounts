using CommunityToolkit.Mvvm.ComponentModel;

namespace cupprintAccounts.ViewModel
{
    public partial class ParentViewModel : ObservableObject
    {
        // Source gennerators will  automatically complete through partial class generation 
        // This generates basic getters and setters
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        public bool isBusy;

        [ObservableProperty]
        public string? heading;

        [ObservableProperty]
        public string? option;

        // Lambda function to check if not busy
        public bool IsNotBusy => !IsBusy;
    }
}
