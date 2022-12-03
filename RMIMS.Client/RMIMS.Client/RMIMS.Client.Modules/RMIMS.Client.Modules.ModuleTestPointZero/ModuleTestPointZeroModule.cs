using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using RMIMS.Client.Core;
using RMIMS.Client.Modules.ModuleTestPointZero.Views;

namespace RMIMS.Client.Modules.ModuleTestPointZero
{
    [Module(ModuleName = "ModuleTestPointZero")]
    public class ModuleTestPointZero : IModule
    {
        private readonly IRegionManager _regionManager;
        public ModuleTestPointZero(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(TestPointZero));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}