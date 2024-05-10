using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Note.Basic.Page;

namespace Note.Basic
{
    internal class _07Task
    {
        public  static void Run()
        {
            //MessageBox.Show("开始创建并执行Task");
            //TaskCreateRun();
            //MessageBox.Show("Task执行完毕");

            //RunAsyncMethod();

            TaskCanceled();
            Thread.Sleep(3000);
            cts.Cancel();
        }



        public static void TaskCreateRun() 
        {
            // 方式1： Task.Run
            Task.Run(() =>
            {
                MessageBox.Show("Task.Run");
            });

            // 方式2： Task.Factory.StartNew
            Task.Factory.StartNew(() =>
            {
                MessageBox.Show("Task.Factory.StartNew");
            });

            //方式3：new Task<返回类型>
            Task<int> t=new Task<int>(() => { MessageBox.Show("new Task"); return 1;});
            t.Start();
        }

        /// <summary>
        /// 调用异步方法
        /// </summary>
        public async static void RunAsyncMethod()
        {
            var r = GetStringAsync().Result;
            GetStringAsync().ContinueWith(t =>
            {
                MessageBox.Show(t.Result);
            });

            MessageBox.Show(await GetStringAsync2());

            //GetStringAsync2().RunSynchronously();//同步，获取不到返回值，只适合无返回值的方法
            GetStringAsync2().Wait();//同步，获取不到返回值，只适合无返回值的方法

            Task.WaitAll(GetStringAsync2(), GetStringAsync());//同步阻塞，所有执行完成。获取不到返回值，只适合无返回值的方法。
            Task.WaitAny(new Task[] { GetStringAsync2(), GetStringAsync() });//同步阻塞，其中1个异步执行完成。获取不到返回值，只适合无返回值的方法。

            //同步阻塞，获取到所有异步方法的返回值
            Task.WhenAll(GetStringAsync2(), GetStringAsync()).ContinueWith(p => {
                p.Result.ToList().ForEach(t => MessageBox.Show(t));
            });
        }

        /// <summary>
        /// 创建异步方法
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetStringAsync()
        {
            //方式1： 使用await的方法，方法定义必须添加async
            //return await Task.Run(() =>
            //{
            //    return "Task.FromResult 返回结果";
            //});

            //方式2： 使用Task.FromResult
            return await Task.FromResult("GetStringAsync Task.FromResult 返回结果");
        }

        public static Task<string> GetStringAsync2()
        {
            return  Task.FromResult("GetStringAsync2 Task.FromResult 返回结果");
        }

       static  CancellationTokenSource cts = new CancellationTokenSource();
       static CancellationToken token = cts.Token;

        /// <summary>
        /// 取消异步方法
        /// </summary>
        public static void TaskCanceled() 
        {
            Task task = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        MessageBox.Show("任务已取消");
                        return;
                    }

                    MessageBox.Show($"正在执行操作：{i}");
                    Thread.Sleep(300);
                }

                MessageBox.Show("任务完成");
            }, token);
        }


    }
}
