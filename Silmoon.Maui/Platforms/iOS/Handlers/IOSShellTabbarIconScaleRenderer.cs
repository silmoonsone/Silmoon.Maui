using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreGraphics;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using UIKit;

namespace Silmoon.Maui.Handlers
{
    public class IOSShellTabbarIconScaleRenderer : ShellRenderer
    {
        protected override IShellTabBarAppearanceTracker CreateTabBarAppearanceTracker()
        {
            return new IOSShellTabBarAppearanceTabbarIconScaleTracker();
        }
    }
    public class IOSShellTabBarAppearanceTabbarIconScaleTracker : IShellTabBarAppearanceTracker
    {
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void ResetAppearance(UITabBarController controller)
        {
            //throw new NotImplementedException();
        }

        public void SetAppearance(UITabBarController controller, ShellAppearance appearance)
        {
            //throw new NotImplementedException();
        }

        public void UpdateLayout(UITabBarController controller)
        {
            foreach (var tabbarItem in controller.TabBar.Items)
            {
                tabbarItem.Image = tabbarItem.Image?.Scale(new CGSize(24, 24));
            }
        }
    }
}
