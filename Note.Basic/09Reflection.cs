using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace Note.Basic
{
   
    //反射
    internal class _09Reflection
    {
        public static void Run()
        {
            GetAssemblyInfo();
            GetTypeInfo();
           
        }

        /// <summary>
        /// 获取程序集信息
        /// </summary>
        public static void GetAssemblyInfo() 
        {
            // 获取当前程序集
            Assembly assembly = Assembly.GetExecutingAssembly();
           //获取dll程序集
            Assembly assembly2 = Assembly.LoadFrom("Note.Basic.dll");
            //获取程序集下的类型信息
           var typesInfo= assembly.DefinedTypes;

            //获取程序集下的模块信息
            var modules = assembly.GetLoadedModules();//.GetModules();
            foreach (var module in modules) 
            {
                //获取模块下的类型信息， 不是Com和嵌套类型
               var types= module.GetTypes().Where(p=>p.Namespace.StartsWith("Note")&&
                p.IsCOMObject==false&&!p.IsNested
               );
               
            }
        }

        /// <summary>
        /// 获取类型信息
        /// </summary>
        public static void GetTypeInfo() 
        {
            // 获取 Person 类型的 Type 对象
            Type personType = Type.GetType("Note.Basic.Person");

            // 获取 Person 类型的名称
            string typeName = personType.FullName;
            Console.WriteLine("Type name: {0}", typeName);

            // 获取 Person 类型的字段
            FieldInfo[] fields = personType.GetFields();
            foreach (FieldInfo field in fields)
            {
                MessageBox.Show(string.Format("Field: {0} ({1})", field.Name, field.FieldType.Name));
            }

            // 获取 Person 类型的属性
            PropertyInfo[] properties = personType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                MessageBox.Show(string.Format("Property: {0} ({1})", property.Name, property.PropertyType.Name));
            }

            // 获取 Person 类型的构造函数
            ConstructorInfo[] constructors = personType.GetConstructors();
            foreach (ConstructorInfo constructor in constructors)
            {
                ParameterInfo[] parameters = constructor.GetParameters();
                MessageBox.Show(string.Format("Constructor: {0}({1})", constructor.Name, string.Join(", ", parameters.Select(p => p.ParameterType.Name))));
            }

            // 获取 Person 类型的 SayHello 方法
            MethodInfo sayHelloMethod = personType.GetMethod("Print");
            MessageBox.Show(string.Format("Method: {0}({1})", sayHelloMethod.Name, string.Join(", ", sayHelloMethod.GetParameters().Select(p => p.ParameterType.Name))));

            // 创建 Person 类型的实例
            object? personObject = personType.Assembly.CreateInstance("Note.Basic.Person");

            // 将 object 对象转换为 Person 对象
            Person person = (Person)personObject;

            // 设置 Person 对象的属性
            person.Name = "John Doe";
            person.Age = 30;

            // 调用 Person 对象的方法
            sayHelloMethod.Invoke(person, null);
        }
    }
}
