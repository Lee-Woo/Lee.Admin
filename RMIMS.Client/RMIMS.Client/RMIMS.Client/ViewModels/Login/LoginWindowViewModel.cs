using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using RMIMS.Client.Core;
using RMIMS.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RMIMS.Client.ViewModels.Login
{
    public class LoginWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;


        private DelegateCommand _loginLoadingCommand;
        public DelegateCommand LoginLoadingCommand =>
            _loginLoadingCommand ?? (_loginLoadingCommand = new DelegateCommand(ExecuteLoginLoadingCommand));

        public LoginWindowViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
        }
        void ExecuteLoginLoadingCommand()
        {
            IRegion region = _regionManager.Regions[RegionNames.LoginContentRegion];
            region.RequestNavigate("LoginContent", NavigationCompelted);
        }
        private void NavigationCompelted(NavigationResult result)
        {
            if (result.Result == true)
            {
                //Thread.Sleep(1000);
                //_dialogService.Show("SuccessDialog", new DialogParameters($"message={"导航到LoginMainContent页面成功"}"), null);
            }
            else
            {
                //_dialogService.Show("WarningDialog", new DialogParameters($"message={"导航到LoginMainContent页面失败"}"), null);
            }
        }

    }
}
