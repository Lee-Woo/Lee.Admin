using System.Windows.Controls;

namespace RMIMS.Client.Views.Login
{
    /// <summary>
    /// Interaction logic for LoginContent
    /// </summary>
    public partial class LoginContent : UserControl
    {
        public LoginContent()
        {
            
            InitializeComponent();
        }

        private void title_Completed(object sender, System.EventArgs e)
        {
            this.IsEnabled = true;
        }
    }
}
