using Microsoft.Extensions.Logging;
using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using RMIMS.Client.Core;
using RMIMS.Client.Core.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RMIMS.Client.ViewModels.Layout
{
    public class UCPartsViewModel : RegionViewModelBase
    {
        private IRegionNavigationJournal _journal;
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;
        private ILogger _logger;

        private bool _isCanExcute;
        public bool IsCanExcute
        {
            get { return _isCanExcute; }
            set { SetProperty(ref _isCanExcute, value); }
        }

        private DelegateCommand _loadedCommand;
        public DelegateCommand LoadedCommand =>
            _loadedCommand ?? (_loadedCommand = new DelegateCommand(ExecuteLoadedCommand));

        private DelegateCommand _goMainLayoutCommand;
        public DelegateCommand GoMainLayoutCommand =>
            _goMainLayoutCommand ?? (_goMainLayoutCommand = new DelegateCommand(ExecuteGoMainLayoutCommand, CanExecuteGoForwardCommand));

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="containerProvider"></param>
        /// <param name="regionManager"></param>
        public UCPartsViewModel(IContainerProvider containerProvider, IRegionManager regionManager) : base(regionManager)
        {
            _regionManager = regionManager;
            _dialogService = containerProvider.Resolve<IDialogService>();
            _logger = containerProvider.Resolve<ILogger>();
        }

        void ExecuteLoadedCommand()
        {

        }
        void ExecuteGoMainLayoutCommand()
        {
            _logger.LogInformation("即将进入到测量主界面");
            Navigate("UCMainLayout");
        }

        #region 导航方法
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate(RegionNames.PartsViewRegion, navigatePath);
        }
        private bool CanExecuteGoForwardCommand()
        {
            this.IsCanExcute = _journal != null && _journal.CanGoForward;
            return true;
        }
        public override bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
        }
        #endregion

    }
}
