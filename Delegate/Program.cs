using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegate
{
    //委托类型的名称都应该以EventHandler结束
    //有一个void返回值，并接受两个输入参数：一个Object 类型，一个 EventArgs类型(或继承自EventArgs)
    public delegate void BoiledEventHandler(Object sender, BoiledEventArgs e);

    // 定义BoiledEventArgs类，传递给Observer所感兴趣的信息
    public class BoiledEventArgs : EventArgs
    {
        public readonly int temperature;
        public BoiledEventArgs(int temperature)
        {
            this.temperature = temperature;
        }
    }


    class Program
    {
        
        public class Heater
        {
            private int _temperature;
            public string type = "RealFire 001";       // 添加型号作为演示
            public string area = "China Xian";         // 添加产地作为演示
            //event封装过的委托(保留对某一类相同签名方法注册的可能)
            //即可看做一个数组/链表,其他类的方法可以往里面注册,当到一定时候自动遍历数组并挨个调用数组里的方法
            //事件的命名为 委托去掉 EventHandler之后剩余的部分
            public event BoiledEventHandler Boiled;
            public void BoilWater()
            {
                for (var i = 0; i <= 100; i++)
                {
                    _temperature = i;

                    if (_temperature <= 95) continue;
                    if (Boiled != null)
                    { 
                        //如果有对象注册
                        BoiledEventArgs e = new BoiledEventArgs(_temperature);
                        OnBoiled(e);  // 调用 OnBolied方法
                    }
                }
            }

            // 可以供继承自 Heater 的类重写，以便继承类拒绝其他对象对它的监视
            protected virtual void OnBoiled(BoiledEventArgs e)
            {
                if (Boiled != null)
                { 
                    // 如果有对象注册
                    Boiled(this, e);  // 调用所有注册对象的方法
                }
            }
        }



        // 警报器
        public class Alarm
        {
            public void MakeAlert(Object sender, BoiledEventArgs e)
            {
                var heater = sender as Heater;
                Console.WriteLine("Alarm：{0} - {1}: ", heater.area, heater.type);
                Console.WriteLine("Alarm: 嘀嘀嘀，水已经 {0} 度了：", e.temperature);
                Console.WriteLine();
            }
        }

        // 显示器
        public class Display
        {
            public static void ShowMsg(Object sender, BoiledEventArgs e)
            {
                var heater = sender as Heater;
                Console.WriteLine("Display：{0} - {1}: ", heater.area, heater.type);
                Console.WriteLine("Display：水快烧开了，当前温度：{0}度。", e.temperature);
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            var heater = new Heater();
            var alarm = new Alarm();
            heater.Boiled += alarm.MakeAlert;    //注册方法
            //heater.Boiled += new BoiledEventHandler(alarm.MakeAlert);    //也可以这么注册（实例化委托并传入方法即为push/注册方法）
            heater.Boiled += Display.ShowMsg;    //注册静态方法

            heater.BoilWater();   //烧水，会自动调用注册过对象的方法
        }
    }
}
