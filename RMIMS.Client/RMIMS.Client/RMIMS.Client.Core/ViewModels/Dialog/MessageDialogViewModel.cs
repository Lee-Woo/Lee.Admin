using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RMIMS.Client.Core.ViewModels.Dialog
{
    public class MessageDialogViewModel : BindableBase, IDialogAware
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private string _title = "提示";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private BitmapImage _imgIcon;
        public BitmapImage ImgIcon
        {
            get { return _imgIcon; }
            set { SetProperty(ref _imgIcon, value); }
        }

        public event Action<IDialogResult> RequestClose;

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Message = parameters.GetValue<string>("message");
            Title = parameters.GetValue<string>("title");
            MessageBoxImage image = parameters.GetValue<MessageBoxImage>("image");
            switch (image)
            {
                case MessageBoxImage.Information:
                    CoverImageIcon("pack://application:,,,/RMIMS.Client.Core;Component/Images/information.png");
                    break;
                case MessageBoxImage.Question:
                    CoverImageIcon("pack://application:,,,/RMIMS.Client.Core;Component/Images/Question.png");
                    break;
                case MessageBoxImage.Error:
                    CoverImageIcon("pack://application:,,,/RMIMS.Client.Core;Component/Images/error.png");
                    break;
                case MessageBoxImage.Warning:
                    CoverImageIcon("pack://application:,,,/RMIMS.Client.Core;Component/Images/warning.png");
                    break;
            }
        }
        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }
        private DelegateCommand _getMessageCommand;
        /// <summary>
        /// 确定按钮
        /// </summary>
        public DelegateCommand GetMessageCommand
        {
            get => _getMessageCommand = new DelegateCommand(() =>
            {
                var parameter = new DialogParameters();
                parameter.Add("Message", Message);
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, parameter));
            });
        }
        private DelegateCommand _cancelMessageCommand;
        /// <summary>
        /// 窗体关闭按钮
        /// </summary>
        public DelegateCommand CancelMessageCommand
        {
            get => _cancelMessageCommand = new DelegateCommand(() =>
            {
                RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
            });
        }
        private void CoverImageIcon(string uri)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(uri, UriKind.Absolute);
            img.EndInit();

            ImgIcon = img;
        }

    }
}
