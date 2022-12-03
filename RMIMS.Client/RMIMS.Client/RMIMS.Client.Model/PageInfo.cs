using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMIMS.Client.Model
{
    /// <summary>
    /// 页面信息
    /// </summary>
    public class PageInfo : BindableBase
    {
        private string _headerName;
        private string _viewName;
        private object _body;

        /// <summary>
        /// 标题
        /// </summary>
        public string HeaderName
        {
            get { return _headerName; }
            set { _headerName = value; }
        }

        /// <summary>
        /// 视图名称
        /// </summary>
        public string ViewName
        {
            get { return _viewName; }
            set { _viewName = value; }
        }

        /// <summary>
        /// 窗口内容
        /// </summary>
        public object Body
        {
            get { return _body; }
            set { _body = value; }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        public DelegateCommand<PageInfo> CloseTabCommand { get; set; }
    }
}
