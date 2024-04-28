using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Note.Basic
{
    internal class _04Delegate
    {
        //委托定义一种类型，该类型封装一个或多个方法（一个或多个方法指向委托实例）。
        private  delegate string MyDelegate(string str);
        public static void Run()
        {
            //1.创建委托实例
            MyDelegate myDelegate = MyMethod;
            myDelegate += MyMethod2; //委托链（多播）,先调用 MyMethod，在调用 MyMethod2
            MessageBox.Show(myDelegate("Hello World!"));
            //上面的代码等价于 可以使用GetInvocationList()方法获取委托链中的所有方法，然后逐个调用，获取每个方法的放回值。
            foreach (var del in myDelegate.GetInvocationList())
            {
                MessageBox.Show(del.DynamicInvoke("Hello World!").ToString());
            }

            //2.委托作为方法参数
            MyDelegate myDelegate2 = MyMethod2;
            MessageBox.Show(DelegateParm(myDelegate));
            MessageBox.Show(DelegateParm(myDelegate2));
        }

        private static string MyMethod(string str)
        {
            return $" MyMethod{str}";
        }

        private static string MyMethod2(string str)
        {
            return $" MyMethod2{str}";
        }


        private static string DelegateParm(MyDelegate myDelegate)
        {
           return myDelegate(myDelegate.Method.Name);
        }
    }
}
