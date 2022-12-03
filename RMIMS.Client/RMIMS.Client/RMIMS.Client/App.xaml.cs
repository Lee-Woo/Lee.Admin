using Prism.Ioc;
using Prism.Modularity;
using RMIMS.Client.Views.Login;
using System.Windows;
using RMIMS.Client.Core.View.Dialog;
using RMIMS.Client.Core.ViewModels.Dialog;
using RMIMS.Client.Views.Layout;
using Microsoft.Extensions.Logging;
using RMIMS.Client.Services.Interfaces.API.Menu;
using RMIMS.Client.Services.API.Menu;
using RMIMS.Client.Core.Constants;
using RMIMS.Client.Services.API.Login;
using RMIMS.Client.Services.Interfaces.API.Login;
using Prism.DryIoc;
using RMIMS.Client.Core.AOP;

namespace RMIMS.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<LoginWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //var container = PrismIocExtensions.GetContainer(containerRegistry);
    
            containerRegistry.Register<ILoginService, LoginService>()
                             .InterceptAsync<ILoginService, AspectAsyncInterceptor>();

            containerRegistry.Register<Global>();

            containerRegistry.Register<IRouterService, RouterService>();

            containerRegistry.RegisterForNavigation<LoginContent>();

            //导航界面
            containerRegistry.RegisterForNavigation<UCPartsView>();
            containerRegistry.RegisterForNavigation<UCMainLayout>();

            //日志
            NLog.LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(@"Config\NLog.config");
            var factory = new NLog.Extensions.Logging.NLogLoggerFactory();
            ILogger logger = factory.CreateLogger("NLog");
            containerRegistry.RegisterInstance(logger);

            containerRegistry.RegisterDialog<MessageDialog, MessageDialogViewModel>();

        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //moduleCatalog.AddModule<ModuleTestPointEdit>();
        }
        protected override IModuleCatalog CreateModuleCatalog()
        {
            //使用目录文件扫描注册
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };

            //使用配置文件App.config注册
            //return new ConfigurationModuleCatalog();
        }
    }
}
