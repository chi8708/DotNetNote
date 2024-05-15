using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace Note.Basic
{
    /// <summary>
    /// 特性
    /// </summary>
    static class _11Attribute
    {
        public static void Run() 
        {
            GetAttribute();
        }

        /// <summary>
        /// 获取特性
        /// </summary>
        public static void GetAttribute() 
        {

            // 获取 MyClass 类上的 MyCustom 特性
            var classAttributes = typeof(MyClass).GetCustomAttributes(typeof(MyCustomAttribute), false);
            foreach (MyCustomAttribute attribute in classAttributes)
            {
                MessageBox.Show($"MyClass类使用的MyCustom特性: {attribute.Description}");
            }

            // 获取 MyClass 类中 MyMethod 方法上的 MyCustom 特性
            var methodInfo = typeof(MyClass).GetMethod("MyMethod");
            var methodAttributes = methodInfo.GetCustomAttributes(typeof(MyCustomAttribute), false);
            foreach (MyCustomAttribute attribute in methodAttributes)
            {
                MessageBox.Show($"MyMethod方法使用的MyCustom特性: {attribute.Description}");
            }
        }
    }

    /// <summary>
    /// 定义特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class MyCustomAttribute : Attribute
    {
        public string Description { get; }

        public MyCustomAttribute(string description)
        {
            Description = description;
        }
    }

    /// <summary>
    /// 应用特性
    /// </summary>
    [MyCustom("类使用自定义特性")]
    public class MyClass
    {
        [MyCustom("方法使用自定义特性")]
        public void MyMethod()
        {
        }
    }



}
