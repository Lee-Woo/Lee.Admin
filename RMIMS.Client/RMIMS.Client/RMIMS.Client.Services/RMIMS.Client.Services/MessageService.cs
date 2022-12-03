using RMIMS.Client.Services.Interfaces;

namespace RMIMS.Client.Services
{
    public class MessageService : IMessageService
    {
        public MessageService()
        {

        }
        public string GetMessage()
        {
            return "ViewA,ModuleA,主界面";
        }
    }
}
