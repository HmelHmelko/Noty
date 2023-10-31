namespace Noty.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public FileTabViewModel FileTabVM { get; set; }
        
        public MainWindowViewModel()
        {
            FileTabVM = new FileTabViewModel();
        }
    }
}
