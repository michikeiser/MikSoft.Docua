namespace MikSoft.Docua.Client
{
    using System.Windows;

    using DryIoc;

    using MikSoft.Docua.Client.Views;
    using MikSoft.Docua.Common;
    using MikSoft.Docua.Common.Data;
    using MikSoft.Docua.Common.Interfaces;
    using MikSoft.Docua.Common.Services;

    using Prism.DryIoc;
    using Prism.Ioc;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Rules CreateContainerRules()
        {
            var containerRules = base.CreateContainerRules();
            var rules = containerRules.WithDefaultIfAlreadyRegistered(IfAlreadyRegistered.AppendNotKeyed);
            return rules;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView>();

            var container = Container.GetContainer();

            container.Register<IDryIocRegistration, ClientTypeRegistration>();
            container.Register<IDryIocRegistration, CommonTypeRegistration>();
            container.Register<IDryIocRegistration, CommonDataTypeRegistration>();
            container.Register<IDryIocRegistration, CommonServicesTypeRegistration>();

            RegisterAssemblies();
        }

        private void RegisterAssemblies()
        {
            var container = Container.GetContainer();
            var dryIocRegistrations = container.ResolveMany<IDryIocRegistration>();

            foreach (var dryIocRegistration in dryIocRegistrations)
            {
                dryIocRegistration.Load(container);
            }
        }

        protected override Window CreateShell()
        {
            var mainView = Container.Resolve<ShellView>();
            return mainView;
        }
    }
}