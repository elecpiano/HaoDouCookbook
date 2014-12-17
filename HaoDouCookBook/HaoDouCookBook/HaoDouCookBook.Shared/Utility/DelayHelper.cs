using System;
using Windows.UI.Xaml;

namespace HaoDouCookBook.Utility
{
    public class DelayHelper
    {
        public static void Delay(TimeSpan delay, Action action)
        {
            if(action == null)
            {
                return;
            }

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = delay;
            timer.Tick += (s, o) =>
            {
                if (action != null)
                {
                    action.Invoke();
                }

                DispatcherTimer t = s as DispatcherTimer;
                if (t != null)
                {
                    t.Stop();
                }
            };

            timer.Start();
        }
    }
}
