using Prism.Commands;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using RMIMS.Client.Core;
using RMIMS.Client.Core.Utils;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace RMIMS.Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private Window _MainWindow;

        private IModuleManager _moduleManager;
        private IDialogService _dialogService;

        private DelegateCommand _minimizeWindowCommand;
        public DelegateCommand MinimizeWindowCommand =>
            _minimizeWindowCommand ?? (_minimizeWindowCommand = new DelegateCommand(ExecuteMinimizeWindowCommand));

        private DelegateCommand _maximizeWindowCommand;
        public DelegateCommand MaximizeWindowCommand =>
            _maximizeWindowCommand ?? (_maximizeWindowCommand = new DelegateCommand(ExecuteMaximizeWindowCommand));

        private DelegateCommand _closeWindowCommand;
        public DelegateCommand CloseWindowCommand =>
            _closeWindowCommand ?? (_closeWindowCommand = new DelegateCommand(ExecuteCloseWindowCommand));

        private DelegateCommand<object> _loadedCommand;
        public DelegateCommand<object> LoadedCommand =>
            _loadedCommand ?? (_loadedCommand = new DelegateCommand<object>(ExecuteLoadedCommand));

        private DelegateCommand<CancelEventArgs> _closingCommand;
        public DelegateCommand<CancelEventArgs> ClosingCommand =>
            _closingCommand ?? (_closingCommand = new DelegateCommand<CancelEventArgs>(ExecuteClosingCommand));

        private DelegateCommand<MouseEventArgs> _titleMouseLeftButtonMoveCommand;
        public DelegateCommand<MouseEventArgs> TitleMouseLeftButtonMoveCommand =>
            _titleMouseLeftButtonMoveCommand ?? (_titleMouseLeftButtonMoveCommand = new DelegateCommand<MouseEventArgs>(ExecuteTitleMouseLeftButtonMoveCommand));

        private string _title = "数字化测量系统（Dins）谛因斯";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public IRegionManager RegionMannager { get; }

        public MainWindowViewModel(IModuleManager moduleManager, IRegionManager regionManager, IDialogService dialogService)
        {
            _moduleManager = moduleManager;
            RegionMannager = regionManager;
            _dialogService = dialogService;
            _moduleManager.LoadModuleCompleted += _moduleManager_LoadModuleCompleted;
        }
        void ExecuteMinimizeWindowCommand()
        {
            SystemCommands.MinimizeWindow(_MainWindow);
        }
        void ExecuteMaximizeWindowCommand()
        {
            Window w = _MainWindow;
            if (w.WindowState == WindowState.Maximized)
                SystemCommands.RestoreWindow(w);
            else
                SystemCommands.MaximizeWindow(w);
        }
        void ExecuteCloseWindowCommand()
        {
            SystemCommands.CloseWindow(_MainWindow);
        }
        void ExecuteLoadedCommand(object o)
        {
            _MainWindow = (o as RoutedEventArgs).Source as Window;

            IRegion region = RegionMannager.Regions[RegionNames.PartsViewRegion];
            region.RequestNavigate("UCPartsView", NavigationCompelted);
        }
        void ExecuteClosingCommand(CancelEventArgs e)
        {
            _dialogService.ShowMsgDialog("确认退出系统运行吗？", (r) =>
            {
                var result = r.Result;
                if (result == ButtonResult.OK)
                {
                    CloseWindow();
                }
                else
                {
                    e.Cancel = true;
                }
            });

        }
        void ExecuteTitleMouseLeftButtonMoveCommand(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _MainWindow.DragMove();
            }
        }
        private void _moduleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            
        }
        private void NavigationCompelted(NavigationResult result)
        {
            //if (result.Result == true)
            //{
            //    Thread.Sleep(1000);
            //    _dialogService.Show("SuccessDialog", new DialogParameters($"message={"导航到LoginMainContent页面成功"}"), null);
            //}
            //else
            //{
            //    _dialogService.Show("WarningDialog", new DialogParameters($"message={"导航到LoginMainContent页面失败"}"), null);
            //}
        }
        public void CloseWindow()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            System.Environment.Exit(0);
        }


    }
}
