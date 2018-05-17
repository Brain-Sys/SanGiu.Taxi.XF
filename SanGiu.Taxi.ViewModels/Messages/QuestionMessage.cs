using System;
using System.Collections.Generic;
using System.Text;

namespace SanGiu.Taxi.ViewModels.Messages
{
    public class QuestionMessage : ShowDialogMessage
    {
        public string CancelCaption { get; set; } = "No";
        public Action Yes { get; set; }
        public Action No { get; set; }

        public QuestionMessage()
        {
            this.Title = "Conferma!";
            this.Caption = "Sì";
        }
    }
}