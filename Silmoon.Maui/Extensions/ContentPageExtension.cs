using System;
using System.Collections.Generic;
using System.Text;

namespace Silmoon.Maui.Extensions
{
    public static class ContentPageExtension
    {
        extension(ContentPage page)
        {
            public bool IsModalPage()
            {
                if (page.Navigation is not null)
                    return page.Navigation.ModalStack.Contains(page);
                else return false;
            }
            public async Task<bool> PopAsync()
            {
                if (page.Navigation is not null)
                {
                    if (page.IsModalPage())
                        await page.Navigation.PopModalAsync();
                    else
                        await page.Navigation.PopAsync();
                    return true;
                }
                else return false;
            }
        }
    }
}
