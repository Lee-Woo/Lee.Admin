using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using RMIMS.Client.Core;
using RMIMS.Client.Modules.ModuleTestPointEdit.Views;
using RMIMS.Client.Services.Interfaces;
using RMIMS.Client.Services;
using System.ComponentModel;
using NetTaste;
using System.Linq;

namespace RMIMS.Client.Modules.ModuleTestPointEdit
{
    [Module(ModuleName = "ModuleTestPointEdit", OnDemand = true)]
    //[Module(ModuleName = "ModuleTestPointEdit")]
    public class ModuleTestPointEdit : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleTestPointEdit(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(TestPointEdit));
            _regionManager.RegisterViewWithRegion(RegionNames.NavbarRegion, typeof(NavbarContent));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<TestPointEdit>();
            containerRegistry.Register<IMessageService, MessageService>();
        }
    }
}