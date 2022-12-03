using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RMIMS.Client.Model
{
    public class MenuGroup : BindableBase
    {
        private string _iconCode;
        private string _header;
        private string _targetView;
        private bool _isSelected = false;
        public string IconCode
        {
            get { return _iconCode; }
            set { SetProperty(ref _iconCode, value); }
        }
        public string Header
        {
            get { return _header; }
            set { SetProperty(ref _header, value); }
        }
        public string TargetView
        {
            get { return _targetView; }
            set { SetProperty(ref _targetView, value); }
        }
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
        public DelegateCommand<MenuGroup> OpenViewCommand { get; set; }

        public ObservableCollection<MenuGroup> Children { get; set; } = new ObservableCollection<MenuGroup>();
    }
}
