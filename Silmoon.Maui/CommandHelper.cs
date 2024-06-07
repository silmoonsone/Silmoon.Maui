using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silmoon.Maui
{
    public class CommandHelper
    {
        public static Command<CheckBox> CheckBoxFireCommand { get; set; } = new Command<CheckBox>((checkBox) => checkBox.IsChecked = !checkBox.IsChecked);
    }
}
