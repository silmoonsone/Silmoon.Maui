using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views.InputMethods;
using Android.Views;
using Android.Widget;
using Android.InputMethodServices;

namespace Silmoon.Maui.Platforms.Android
{
    public class KeyboardBehaviorFixer
    {
        static MotionEventActions beforeAction;

        public static void DispatchTouchEvent(MotionEvent ev, MauiAppCompatActivity mauiAppCompatActivity)
        {
            if (ev.Action == MotionEventActions.Up && beforeAction == MotionEventActions.Down)
            {
                var inputMethodManager = (InputMethodManager)mauiAppCompatActivity.GetSystemService(global::Android.Content.Context.InputMethodService);

                if (IsSoftInputVisible(mauiAppCompatActivity, inputMethodManager) && mauiAppCompatActivity.CurrentFocus != null)
                {
                    if (mauiAppCompatActivity.CurrentFocus is not EditText || !IsTouchInsideView(ev, mauiAppCompatActivity.CurrentFocus))
                    {
                        inputMethodManager.HideSoftInputFromWindow(mauiAppCompatActivity.CurrentFocus.WindowToken, HideSoftInputFlags.None);
                        if (mauiAppCompatActivity.CurrentFocus is EditText editText) editText.ClearFocus();
                    }
                }
            }
            beforeAction = ev.Action;
        }


        private static bool IsTouchInsideView(MotionEvent ev, global::Android.Views.View view)
        {
            var location = new int[2];
            view.GetLocationOnScreen(location);
            var x = ev.RawX + view.Left - location[0];
            var y = ev.RawY + view.Top - location[1];
            var result = x >= view.Left && x < view.Right && y >= view.Top && y < view.Bottom;
            return result;
        }

        private static bool IsSoftInputVisible(MauiAppCompatActivity mauiAppCompatActivity, InputMethodManager inputMethodManager)
        {
            try
            {
                var inputMethodSubtype = inputMethodManager.CurrentInputMethodSubtype;
                if (inputMethodSubtype != null && inputMethodSubtype.Mode.Equals("keyboard") && mauiAppCompatActivity.CurrentFocus is EditText)
                    return true;
            }
            catch { }
            return false;
        }

    }
}
