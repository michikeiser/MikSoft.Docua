namespace MikSoft.Docua.Client.ViewModels
{
    using MikSoft.Docua.Client.Services;

    using Prism.Commands;
    using Prism.Mvvm;

    internal class MainViewModel : BindableBase
    {
        private readonly SettingsService _settingsService;

        private string _repositoryPath;

        public MainViewModel(SettingsService settingsService)
        {
            _settingsService = settingsService;

            CmdLoad = new DelegateCommand(OnCmdLoad);
            CmdSave = new DelegateCommand(OnCmdSave);
        }

        public DelegateCommand CmdSave { get; }

        public DelegateCommand CmdLoad { get; }

        public string RepositoryPath
        {
            get
            {
                return _repositoryPath;
            }

            set
            {
                SetProperty(ref _repositoryPath, value);
            }
        }

        private void OnCmdSave()
        {
            _settingsService.RepositoryPath = RepositoryPath;
        }

        private void OnCmdLoad()
        {
            RepositoryPath = _settingsService.RepositoryPath;
        }
    }
}