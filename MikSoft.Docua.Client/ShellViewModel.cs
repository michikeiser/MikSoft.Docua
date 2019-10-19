namespace MikSoft.Docua.Client
{
    using MikSoft.Docua.Client.Constants;
    using MikSoft.Docua.Client.Views;

    using Prism.Mvvm;
    using Prism.Regions;

    internal class ShellViewModel : BindableBase
    {
        public ShellViewModel(IRegionManager regionManager)
        {
            regionManager.RegisterViewWithRegion(RegionNames.REGION_SHELL_CONTENT, typeof(MainView));
        }
    }
}