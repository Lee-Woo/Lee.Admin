using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using RMIMS.Client.Core.Utils;
using RMIMS.Client.Views.Login;
using RMIMS.Client.Views;
using System.Windows.Controls;
using Prism.Regions;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using RMIMS.Client.Services.Interfaces.API.Login;
using RMIMS.Client.Model;
using RMIMS.Client.Core.Constants;
using RMIMS.Client.Services.Interfaces;

namespace RMIMS.Client.ViewModels.Login
{
    public class LoginContentViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        private IDialogService _dialogService;
        private ILoginService _loginService;
        private ILogger _logger;

        private string _userName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private bool _userChecked;
        /// <summary>
        /// 记住密码
        /// </summary>
        public bool UserChecked
        {
            get { return _userChecked; }
            set { SetProperty(ref _userChecked, value); }
        }

        private string _password;
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _loginResult;
        /// <summary>
        /// 进度报告
        /// </summary>
        public string LoginResult
        {
            get { return _loginResult; }
            set { SetProperty(ref _loginResult, value); }
        }

        private bool _isLoginEnabled = true;
        /// <summary>
        /// 禁用按钮
        /// </summary>
        public bool IsLoginEnabled
        {
            get { return _isLoginEnabled; }
            set { SetProperty(ref _isLoginEnabled, value); }
        }

        private bool _isCanExcute;
        public bool IsCanExcute
        {
            get { return _isCanExcute; }
            set { SetProperty(ref _isCanExcute, value); }
        }

        private DelegateCommand<object> _loadedCommand;
        public DelegateCommand<object> LoadedCommand =>
            _loadedCommand ?? (_loadedCommand = new DelegateCommand<object>(ExecuteLoadedCommand));

        private DelegateCommand _closeWindowCommand;
        public DelegateCommand CloseWindowCommand =>
            _closeWindowCommand ?? (_closeWindowCommand = new DelegateCommand(ExecuteCloseWindowCommand));


        private DelegateCommand<PasswordBox> _loginCommand;
        public DelegateCommand<PasswordBox> LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand<PasswordBox>(ExecuteLoginCommand));

        private DelegateCommand<object> _previewTextInputCommand;
        public DelegateCommand<object> PreviewTextInputCommand =>
            _previewTextInputCommand ?? (_previewTextInputCommand = new DelegateCommand<object>(ExecutePreviewTextInputCommand));

        private DelegateCommand<object> _selectionChangedCommand;
        public DelegateCommand<object> SelectionChangedCommand =>
            _selectionChangedCommand ?? (_selectionChangedCommand = new DelegateCommand<object>(ExecuteSelectionChangedCommand));

        private DelegateCommand<object> _passwordChangedCommand;
        public DelegateCommand<object> PasswordChangedCommand =>
            _passwordChangedCommand ?? (_passwordChangedCommand = new DelegateCommand<object>(ExecutePasswordChangedCommand));

        public bool KeepAlive => true;
        public LoginContentViewModel(IContainerProvider containerProvider, IRegionManager regionManager)
        {
            IContainerProvider container = containerProvider;
            _regionManager = regionManager;
            _dialogService = container.Resolve<IDialogService>();
            _logger = container.Resolve<ILogger>();
            _loginService = container.Resolve<ILoginService>();
        }
        void ExecuteLoadedCommand(object o)
        {

        }
        void ExecuteCloseWindowCommand()
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            System.Environment.Exit(0);
        }
        void ExecutePreviewTextInputCommand(object o)
        {

        }
        void ExecuteSelectionChangedCommand(object o)
        {

        }
        void ExecutePasswordChangedCommand(object o)
        {

        }
        public async void ExecuteLoginCommand(PasswordBox passwordBox)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                LoginResult = "用户名不能为空";
                return;
            }
            if (passwordBox == null || string.IsNullOrEmpty(Password))
            {
                LoginResult = "密码不能为空";
                return;
            }

            IsLoginEnabled = false;
            LoginResult = "正在验证登录 . . .";

            var loginResp = await _loginService.Login(new LoginBody() { username = UserName, password = Password });
            if (loginResp != null && loginResp.Success)
            {
                //登录成功
                if (loginResp.Code.Equals("200"))
                {
                    //token
                    string token = loginResp.Results;
                    Global.Token = token;
                    _logger.LogInformation($"登录成功！username:{UserName}");

                    //获取用户信息及权限
                    var userInfoRsp = await _loginService.GetUserInfo();
                    if (userInfoRsp.Success)
                    {
                        //解析用户信息
                        string resdata = userInfoRsp.Results;

                    }
                    else
                    {
                        //获取用户信息失败
                        LoginResult = userInfoRsp.Message;
                        IsLoginEnabled = true;
                        return;
                    }

                    LoginResult = "";
                    ShellSwitcher.Switch<LoginWindow, MainWindow>();
                }
                else
                {
                    //登录失败
                    LoginResult = loginResp.Message;
                    IsLoginEnabled = true;
                    return;
                }
            }
            else
            {
                //登录失败
                LoginResult = (loginResp == null) ? "登录异常(loginResp == null)" : loginResp.Message;
                IsLoginEnabled = true;
                return;
            }
        }



    }
}
