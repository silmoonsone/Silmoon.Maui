using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Maui.Enums
{
    [Flags]
    public enum NotificationBehaviorType
    {
        None = 0,
        Badge = 1,
        Sound = 2,
        Alert = 4,
        List = 8,
        Banner = 16
    }
}
