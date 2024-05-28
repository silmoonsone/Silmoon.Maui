using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Maui.Services.NotificationManager
{
    public class NotificationEventArgs
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Message { get; set; }
        public ReceiveType Type { get; set; }
        public string Identifier { get; set; }
        public string Data { get; set; }
        public PushPlatform PushPlatform { get; set; }
    }
}
