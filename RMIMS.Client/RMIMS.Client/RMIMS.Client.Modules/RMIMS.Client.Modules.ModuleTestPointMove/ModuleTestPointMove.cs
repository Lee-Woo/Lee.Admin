using RMIMS.Client.Modules.ModuleTestPointMove.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using RMIMS.Client.Core;
using System.Linq;

namespace RMIMS.Client.Modules.ModuleTestPointMove
{
    [Module(ModuleName = "ModuleTestPointMove", OnDemand = true)]
    public class ModuleTestPointMove : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleTestPointMove(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(TestPointMove));
            _regionManager.RegisterViewWithRegion(RegionNames.NavbarRegion, typeof(NavbarContent));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<NavbarContent>();
        }
    }
}