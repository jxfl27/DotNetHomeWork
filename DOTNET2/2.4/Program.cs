using System;

namespace _1._4
{
    class Program
    {
        static void Main(string[] args)
        {
            clock clock = new clock();
            showEvent sub = new showEvent();


            sub.HandleTick(clock);
            while (clock.now < clock.obj)
            {
                System.Threading.Thread.Sleep(1000);
                clock.showSecondChange();
            }
            sub.HandleAlarm(clock);
            clock.wyCloudTime();
        }
        public class clock
        {
            public delegate void ClockEventHandle(object sender, EventArgs e);

            public event ClockEventHandle Alarm;

            public event ClockEventHandle Tick;

            public DateTime now = new DateTime(2020, 9, 30, 23, 59, 45);

            public DateTime obj = new DateTime(2020, 10, 01, 00, 00, 00);

            public void showSecondChange()
            {
                EventArgs e = new EventArgs();
                if (this.Tick != null)
                {
                    now = now.AddSeconds(1);
                    this.Tick(this, e);
                    Console.WriteLine(now );
                }
            }

            public void wyCloudTime()
            {
                EventArgs e = new EventArgs();
                if (this.Alarm != null)
                {
                    Console.WriteLine("到点了!");
                    this.Alarm(this, e);
                }
            }


        }
        public class showEvent
        {
            public void oneTick(object sender, EventArgs e)
            {
                Console.WriteLine("+1s");
            }
            public void HandleTick(clock clock)
            {
                clock.Tick += new clock.ClockEventHandle(oneTick);
            }

          public void timeOut(object sender, EventArgs e)
            {
                Console.WriteLine("TIME OUT");
            }

            public void HandleAlarm(clock clock)
            {
                clock.Alarm += new clock.ClockEventHandle(timeOut);
            }

        }
    }
}
