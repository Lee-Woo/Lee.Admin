using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMIMS.Client.Core.Utils
{
    public static class Msg
    {
        public static void ShowMsgDialog(this IDialogService dialogService,
            string message,
            Action<IDialogResult> callback,
            string title = "提示",
            MessageBoxImage image = MessageBoxImage.Information)
        {
            var p = new DialogParameters();
            p.Add("message", message);
            p.Add("title", title);
            p.Add("image", image);

            dialogService.ShowDialog("MessageDialog", p, callback);
        }
    }
}
