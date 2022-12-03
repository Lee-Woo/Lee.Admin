using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Services.Dialogs;
using RMIMS.Client.Core;
using RMIMS.Client.Core.Mvvm;
using RMIMS.Client.Core.Utils;
using RMIMS.Client.Model;
using RMIMS.Client.Services.Interfaces.API.Menu;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace RMIMS.Client.ViewModels.Layout
{
    public class UCMainLayoutViewModel : RegionViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private ILogger _logger;
        private IRegionNavigationJournal _journal;
        private IRouterService _routerService;
        private IModuleManager _moduleManager;

        private ObservableCollection<MenuGroup> _treeListMenu;
        /// <summary>
        /// 菜单
        /// </summary>
        public ObservableCollection<MenuGroup> TreeListMenu
        {
            get { return _treeListMenu; }
            set
            {
                _treeListMenu = value;
                SetProperty(ref _treeListMenu, value);
            }
        }

        public ObservableCollection<PageInfo> OpenPageCollection { get; set; } = new ObservableCollection<PageInfo>();

        private bool _menuIsCollapse = false;
        public bool MenuIsCollapse
        {
            get { return _menuIsCollapse; }
            set { SetProperty(ref _menuIsCollapse, value); }
        }

        private string _alarmMessage;
        /// <summary>
        /// 告警信息
        /// </summary>
        public string AlarmMessage
        {
            get { return _alarmMessage; }
            set { SetProperty(ref _alarmMessage, value); }
        }

        private DelegateCommand _loadedCommand;
        public DelegateCommand LoadedCommand =>
            _loadedCommand ?? (_loadedCommand = new DelegateCommand(ExecuteLoadedCommand));

        private DelegateCommand _goBackCommand;
        public DelegateCommand GoBackCommand =>
            _goBackCommand ?? (_goBackCommand = new DelegateCommand(ExecuteGoBackCommand));

        private DelegateCommand<object> _mainTabSelectionChangedCommand;
        public DelegateCommand<object> MainTabSelectionChangedCommand =>
            _mainTabSelectionChangedCommand ?? (_mainTabSelectionChangedCommand = new DelegateCommand<object>(ExecuteMainTabSelectionChangedCommand));

        private DelegateCommand<object> _menuToggleButtonCommand;
        public DelegateCommand<object> MenuToggleButtonCommand =>
            _menuToggleButtonCommand ?? (_menuToggleButtonCommand = new DelegateCommand<object>(ExecuteMenuToggleButtonCommand));

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="container"></param>
        /// <param name="regionManager"></param>
        public UCMainLayoutViewModel(IContainerProvider container, IRegionManager regionManager) : base(regionManager)
        {
            _regionManager = RegionManager;
            _dialogService = container.Resolve<IDialogService>();
            _logger = container.Resolve<ILogger>();
            _routerService = container.Resolve<IRouterService>();
            _moduleManager = container.Resolve<IModuleManager>();
        }
        private void ExecuteLoadedCommand()
        {

        }
        private void ExecuteMainTabSelectionChangedCommand(object o)
        {
            if (o is SelectionChangedEventArgs e)
            {
                TabControl tabControl = (TabControl)e.Source;
                if (tabControl.SelectedItem != null)
                {
                    PageInfo pageInfo = tabControl.SelectedItem as PageInfo;
                    TreeListMenu.ForEach(m =>
                    {
                        MenuGroup sMenu = m.Children.Where(c => c.TargetView.Equals(pageInfo.ViewName)).FirstOrDefault();
                        if (sMenu != null)
                        {
                            sMenu.IsSelected = true;
                            OpenView(sMenu);
                            return;
                        }
                    });
                }
            }
        }
        private void ExecuteMenuToggleButtonCommand(object o)
        {
            if (o is RoutedEventArgs e)
            {
                if (e.Source is ToggleButton tBtn)
                {
                    if (tBtn.IsChecked == true)
                    {
                        //收缩菜单栏
                        MenuIsCollapse = true;
                    }
                    else
                    {
                        //展开菜单栏
                        MenuIsCollapse = false;
                    }
                }
            }
        }

        /// <summary>
        /// 路由打开Tab
        /// </summary>
        /// <param name="menu"></param>
        private void OpenView(MenuGroup menu)
        {
            string moduleName = $"Module{menu.TargetView}";
            if (!_moduleManager.ModuleExists(moduleName))
            {
                _dialogService.ShowMsgDialog("模块不存在或没有权限！", null, "警告", System.Windows.MessageBoxImage.Error);
                return;
            }
            var module = _moduleManager.Modules.Where(m => m.ModuleName.Equals(moduleName)).FirstOrDefault();
            if (module == null)
            {
                _dialogService.ShowMsgDialog("模块不存在或没有权限！", null, "警告", System.Windows.MessageBoxImage.Error);
                return;
            }
            Assembly asm = null;
            Type asmType = Type.GetType(module.ModuleType);
            if (asmType != null)
            {
                asm = asmType.Assembly;
            }
            if (asm == null)
            {
                _dialogService.ShowMsgDialog("模块不存在或没有权限！", null, "警告", System.Windows.MessageBoxImage.Error);
                return;
            }
            var page = OpenPageCollection.ToList().FirstOrDefault(p => p.HeaderName == menu.Header);
            if (page == null)
            {
                var objectType = (from type in asm.GetTypes()
                                  where type.IsClass && type.Name == menu.TargetView
                                  select type);
                if (objectType.Count() > 0)
                {
                    _moduleManager.LoadModule(moduleName);
                    object view = Activator.CreateInstance(objectType.Single());

                    if (view != null)
                    {
                        OpenPageCollection.Add(new PageInfo
                        {
                            HeaderName = menu.Header,
                            ViewName = menu.TargetView,
                            Body = view,
                            IsSelected = true,
                            CloseTabCommand = new DelegateCommand<PageInfo>(ClosePage)
                        });
                    }

                    //Navbar
                    RegisterMainNavbar(asm);
                }
            }
            else
            {
                page.IsSelected = true;

                //Navbar
                RegisterMainNavbar(asm);
            }
        }
        /// <summary>
        /// 注册Navbar
        /// </summary>
        /// <param name="asm"></param>
        private void RegisterMainNavbar(Assembly asm)
        {
            var activeNav = _regionManager.Regions[RegionNames.NavbarRegion].ActiveViews;
            if (activeNav.Count() > 0)
            {
                _regionManager.Regions[RegionNames.NavbarRegion].Remove(activeNav.Single());
            }

            var navType = (from type in asm.GetTypes()
                           where type.IsClass && type.Name == "NavbarContent"
                           select type);
            if (navType.Count() > 0)
            {
                object navView = Activator.CreateInstance(navType.Single());
                _regionManager.Regions[RegionNames.NavbarRegion].Add(navView);
            }
        }
        /// <summary>
        /// 关闭Tab
        /// </summary>
        /// <param name="page"></param>
        private void ClosePage(PageInfo page)
        {
            OpenPageCollection.Remove(page);
            //判断是否为空
            if (OpenPageCollection.Count == 0)
            {
                TreeListMenu.ForEach(m =>
                {
                    m.Children.ForEach(c => c.IsSelected = false);
                });

                //清空Navbar
                RegisterMainNavbar(Assembly.GetExecutingAssembly());
            }
        }


        #region 导航方法
        void ExecuteGoBackCommand()
        {
            _logger.LogInformation("即将返回到零件界面");

            _journal.GoBack();
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
            {
                _regionManager.RequestNavigate(RegionNames.LoginContentRegion, navigatePath);
            }
        }

        public override void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
        }

        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public async override void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;

            #region 渲染菜单
            //获取路由
            var trouteJson = _routerService.GetRouter();
            var timeouttask = Task.Delay(3000);
            var completedTask = await Task.WhenAny(trouteJson, timeouttask);
            if (completedTask == timeouttask)
            {
                AlarmMessage = "请求菜单信息超时!";
            }
            else
            {
                TreeListMenu = new ObservableCollection<MenuGroup>();
                {
                    MenuGroup mg1 = new MenuGroup();
                    mg1.Header = "测点配置";
                    mg1.IconCode = "\ue61a";
                    TreeListMenu.Add(mg1);
                    mg1.Children.Add(new MenuGroup
                    {
                        Header = "测点配置",
                        TargetView = "TestPointEdit",
                        OpenViewCommand = new DelegateCommand<MenuGroup>(OpenView)
                    });
                    mg1.Children.Add(new MenuGroup
                    {
                        Header = "测点调整",
                        TargetView = "TestPointMove",
                        OpenViewCommand = new DelegateCommand<MenuGroup>(OpenView)
                    });
                    mg1.Children.Add(new MenuGroup
                    {
                        Header = "测点校零",
                        TargetView = "TestPointZero",
                        OpenViewCommand = new DelegateCommand<MenuGroup>(OpenView)
                    });
                    MenuGroup mg2 = new MenuGroup();
                    mg2.Header = "开始测量";
                    mg2.IconCode = "\ue61f";
                    TreeListMenu.Add(mg2);
                    mg2.Children.Add(new MenuGroup
                    {
                        Header = "有序测量",
                        TargetView = "OrderTestPage",
                        OpenViewCommand = new DelegateCommand<MenuGroup>(OpenView)
                    });
                    mg2.Children.Add(new MenuGroup
                    {
                        Header = "无序测量",
                        TargetView = "DisOrderPage",
                        OpenViewCommand = new DelegateCommand<MenuGroup>(OpenView)
                    });
                }
            }
            #endregion
        }

        #endregion
    }
}
