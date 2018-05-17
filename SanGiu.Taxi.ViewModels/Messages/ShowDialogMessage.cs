using System;
using System.Collections.Generic;
using System.Text;

namespace SanGiu.Taxi.ViewModels.Messages
{
    public class ShowDialogMessage
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Caption { get; set; } = "Chiudi";

        public static ShowDialogMessage OperationCanceled()
        {
            var msg = new ShowDialogMessage();
            msg.Message = "Operazione annullata!";
            msg.Title = "Ok";
            return msg;
        }
    }
}