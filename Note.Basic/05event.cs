using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Note.Basic.Page;

namespace Note.Basic
{
    public class _05event
    {
        public static void Run()
        {
            // 推荐，事件是委托的一种特殊用法，事件只能在类的内部定义，外部只能订阅和取消订阅事件。
            var page = new Page();
            page.OnBtnClick +=Page_OnBtnClick;
            page.BtnClick();

            //委托实现不推荐， 因为会把委托暴露给外部（即page类的委托类型暴露给了客户端）
            DelBtnClick delBtnClick = Page_OnBtnClick;
            page.BtnClickByDelegate(delBtnClick);
        }

        private static void Page_OnBtnClick(ClickEventArgs eventArgs)
        {
            MessageBox.Show($"客户端，按钮点击后回调 eventArgs:{eventArgs.ClickCount}");
        }
    }

    public class Page 
    {
        public delegate void DelBtnClick(ClickEventArgs eventArgs);

        public event DelBtnClick OnBtnClick;

        /// <summary>
        /// 事件参数
        /// </summary>
        public class ClickEventArgs : EventArgs
        {
            public ClickEventArgs(int clickCount)
            {
                ClickCount = clickCount;
            }

            public int ClickCount { get; private set; }
        }

        /// <summary>
        /// 通过事件实现
        /// </summary>
        public void BtnClick() 
        {
            MessageBox.Show("事件实现");
            OnBtnClick?.Invoke(new ClickEventArgs(1));//触发事件时 把事件参数传递给订阅者。
        }

        /// <summary>
        /// 通过委托变量实现按钮点击事件，不推荐
        /// </summary>
        public void BtnClickByDelegate(DelBtnClick delBtn)
        {
            MessageBox.Show("委托实现");
            delBtn(new ClickEventArgs(2));
        }
    }
}
