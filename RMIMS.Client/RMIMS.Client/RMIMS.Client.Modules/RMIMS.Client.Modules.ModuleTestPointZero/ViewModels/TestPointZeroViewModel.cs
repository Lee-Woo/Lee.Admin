﻿using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMIMS.Client.Modules.ModuleTestPointZero.ViewModels
{
    public class TestPointZeroViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public TestPointZeroViewModel()
        {
            Message = "测点校零";
        }
    }
}
