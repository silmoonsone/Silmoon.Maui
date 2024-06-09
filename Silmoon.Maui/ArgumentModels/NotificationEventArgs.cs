using Newtonsoft.Json.Linq;
using Silmoon.Maui.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Silmoon.Maui.ArgumentModels
{
    public class NotificationEventArgs
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Message { get; set; }
        public string Identifier { get; set; }
        public JObject Data { get; set; }
        public PlatformType PushPlatform { get; set; }
    }
}
