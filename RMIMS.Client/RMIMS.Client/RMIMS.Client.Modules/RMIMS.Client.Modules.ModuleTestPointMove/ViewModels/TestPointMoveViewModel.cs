﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMIMS.Client.Modules.ModuleTestPointMove.ViewModels
{
    public class TestPointMoveViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public TestPointMoveViewModel()
        {
            Message = "测点调整";
        }
    }
}
