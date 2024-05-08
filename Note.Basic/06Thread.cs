using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Note.Basic
{
    internal class _06Thread
    {
        public static void Run()
        {
            //ThreadDemo();
            //ThreadPoolDemo();
            //TaskDemo();

            new _06Thread().ShareData();
        }

        int count = 0;
        /// <summary>
        ///5个线程 处理同一个程序Method2 最后得到累计数据500。
        /// </summary>
        private void ShareData()
        {
            // 5个Task同时修改 count造成数据不一致问题。
            for (int i = 0; i < 5; i++)
            {
                Task.Run(() => { Method2(); });
            }

            Thread.Sleep(1000);
            MessageBox.Show(count.ToString());
        }

        private static void Method(object c)
        {
            MessageBox.Show($"{c.ToString()},当前线程ID：{Thread.CurrentThread.ManagedThreadId}");
        }

        /// <summary>
        /// 1.Thread类是C#中用于创建和管理线程的标准类。new Thread()创建，thread.Start()执行。
        /// </summary>
        public static void ThreadDemo()
        {
            // Thread thread = new Thread(new ThreadStart(Method));//ThreadStart不能传递参数
            Thread thread = new Thread(new ParameterizedThreadStart(Method));//ParameterizedThreadStart 只能传递object参数
            thread.IsBackground = true;//是否后台线程。
            thread.Start("Thread方式创建线程");
        }


        // <summary>
        /// 2.ThreadPool 可以使用QueueUserWorkItem。
        /// </summary>
        private static void ThreadPoolDemo()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(Method), "ThreadPool方式创建线程");
        }

        // <summary>
        /// 3.Task 可以使用Task.Run() 或 new TaskFactory().StartNew() 创建并执行线程
        /// </summary>
        private static void TaskDemo()
        {
            Task.Run(() => { Method("Task方式创建线程"); });
            new TaskFactory().StartNew(() => { Method("TaskFactory方式创建线程"); });
        }

        private static object o = new object();
        Semaphore semaphore = new Semaphore(1, int.MaxValue);

        public void Method2()
        {

            for (int i = 0; i < 100; i++)
            {
                //lock (o)
                //{

                semaphore.WaitOne();
                count++;
                semaphore.Release();
               // }
            }
        }
    }
}
