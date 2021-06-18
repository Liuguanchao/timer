using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using System.Windows.Threading;
using System.Timers;
using System.Windows.Threading;

namespace timer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Timer newtime = null;

        private delegate void TimerDispatcherDelegate();

        private int count = 0;
        private string strtextbox = "";
        private int hours = 0;
        private int minute = 0;
        private int second = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (newtime.Enabled)
            {
                newtime.Stop();
            }
            string strhours = comhour.SelectionBoxItem.ToString();
            string strminute = comminute.SelectionBoxItem.ToString();
            string strsecond = comsecond.SelectionBoxItem.ToString();
            hours = int.Parse(strhours);
            minute = int.Parse(strminute);
            second = int.Parse(strsecond);
            count = int.Parse(strhours) * 3600 + int.Parse(strminute) * 60 + int.Parse(strsecond);
            strtextbox = hours + "小时" + minute + "分" + second + "秒";
            this.Dispatcher.Invoke(DispatcherPriority.Normal,
             new TimerDispatcherDelegate(updateTextUI));
            newtime.Start();
        }

        private void TimersTimerHandler(object sender, EventArgs args)
        {
            if (hours == 0 && minute == 0 && second == 0)
            {
                textBox.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
                {

                }));
                newtime.Stop();//计时停止 
                this.Dispatcher.Invoke(DispatcherPriority.Normal,
                new TimerDispatcherDelegate(updateWindowsUI));

            }
            else
            {

                if (second != 0)
                {
                    second--;
                }
                else if (minute != 0)
                {
                    minute--;
                    second = 59;
                }
                else
                {
                    hours--;
                    minute = 59;
                }
                strtextbox = hours + "小时" + minute + "分" + second + "秒";
                this.Dispatcher.Invoke(DispatcherPriority.Normal,
                 new TimerDispatcherDelegate(updateTextUI));
            }
        }
        private void updateTextUI()
        {
            textBox.Text = strtextbox;
        }
        private void updateWindowsUI()
        {
            this.Show();
            this.Activate();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            newtime = new Timer();
            newtime.Elapsed += new ElapsedEventHandler(TimersTimerHandler);
            newtime.Interval = 1000;
        }

        private void comhour_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //Delta:代表鼠标滚轮滚动的方向,如果>0代表向前滚动,如果<0代表向后滚动
            int a = e.Delta;
            if (e.Delta > 0)
            {
                comhour.SelectedIndex += 1;
            }
            else
            {
                if (comhour.SelectedIndex > 0)
                {
                    comhour.SelectedIndex -= 1;
                }
            }
        }

        private void comminute_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int a = e.Delta;
            if (e.Delta > 0)
            {
                comminute.SelectedIndex += 1;
            }
            else
            {
                if (comminute.SelectedIndex > 0)
                {
                    comminute.SelectedIndex -= 1;
                }
            }
        }

        private void comsecond_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            int a = e.Delta;
            if (e.Delta > 0)
            {
                comsecond.SelectedIndex += 1;
            }
            else
            {
                if (comsecond.SelectedIndex > 0)
                {
                    comsecond.SelectedIndex -= 1;
                }
            }
        }
    }
}
