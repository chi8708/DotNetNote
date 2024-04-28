using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace Note.Basic
{
    internal class _02TypeInfo
    {
        public static void Run() 
        {
            ArrayConvert();
            ThreadData();
            ThreadShareDate();
        }

        

        /// <summary>
        /// 数组转换
        /// </summary>
        private static void ArrayConvert() 
        {
            var strArr = new string[] { "2015-02-03", "2015-03-11" };
            DateTime[] dtArr = Array.ConvertAll(strArr, new Converter<string, DateTime>(p => DateTime.Parse(p)));
        }

        /// <summary>
        /// 线程数据槽 Thread.SetData共享数据
        /// </summary>
        private static void ThreadData() 
        {

            // 获取当前线程的ID
            int threadId = Thread.CurrentThread.ManagedThreadId;

            // 创建一个数据槽并命名为 "UserId" 多个线程可以共享数据
            LocalDataStoreSlot userIdSlot;
            userIdSlot= Thread.GetNamedDataSlot("UserId");
            if (userIdSlot == null) { Thread.AllocateNamedDataSlot("UserId"); }


            // 将当前线程的用户ID存储在数据槽中
            Thread.SetData(userIdSlot, threadId);

            // 从数据槽中检索当前线程的用户ID
            int retrievedUserId = (int)Thread.GetData(userIdSlot);

            // 打印当前线程的用户ID
            Console.WriteLine("User ID for thread {0}: {1}", threadId, retrievedUserId);
        }

        
        class Counter
        {

            [ThreadStatic]
            private static int count;

            public static void Increment()
            {
                ++count;
                MessageBox.Show ($"Thread {Thread.CurrentThread.ManagedThreadId}: Count = {count}");
                //输出结果：10次 Thread 1 Count=1
            }
        }

        /// <summary>
        /// 线程ThreadStatic 共享数据
        /// </summary>
        public static void ThreadShareDate()
        {
            for (int i = 0; i < 10; i++)
            {
                //Counter.Increment();
                new Thread(Counter.Increment).Start();
            }
        }
    }
}
