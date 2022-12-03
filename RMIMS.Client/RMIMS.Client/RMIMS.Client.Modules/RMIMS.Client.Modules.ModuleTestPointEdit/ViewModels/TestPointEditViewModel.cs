using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using RMIMS.Client.Core.Mvvm;
using RMIMS.Client.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RMIMS.Client.Modules.ModuleTestPointEdit.ViewModels
{
    public class TestPointEditViewModel : RegionViewModelBase
    {
        public TestPointEditViewModel(IRegionManager regionManager, IMessageService messageService) :
            base(regionManager)
        {
            Message = messageService.GetMessage();
        }
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            //do something
        }
    }
}
