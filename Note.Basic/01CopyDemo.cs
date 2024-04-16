using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Note.Basic.MainWindow;
using System.Windows;
using System.Reflection;
using Note.Util;
using System.Linq.Expressions;
using Expression = System.Linq.Expressions.Expression;

namespace Note.Basic
{
    internal class CopyDemo
    {
        /// <summary>
        /// 浅拷贝
        /// </summary>
        public static void Copy1()
        {
            Person person1 = new Person()
            {
                Name = "张三",
                Address = new Address()
                {
                    City = "北京",
                }
            };

            //Person person2 = person1;//对象引用
            Person person2 = person1.Clone();//浅拷贝

            //修改原对象的属性
            person1.Address.City = "上海";
            person1.Age = 12;
            //修改副本对象的属性
            person2.Name = "李四";
            person2.Age = 13;
            person2.Address.City = "昆明";
            string result = $"原对象{JsonConvert.SerializeObject(person1)}。副本{JsonConvert.SerializeObject(person2)}";
            MessageBox.Show($"浅拷贝：原对象和副本相互影响。{result}");
        }

        /// <summary>
        /// 深拷贝
        /// </summary>
        public static void Copy2()
        {
            Person person1 = new Person()
            {
                Name = "张三",
                Address = new Address()
                {
                    City = "北京",
                }
            };

            //Person person2 = CreateDeepCopy(person1);//深拷贝1反射
            // Person person2 =JsonConvert.DeserializeObject<Person>(JsonConvert.SerializeObject(person1));//深拷贝2序列化
            //Person person2 =person1.MapTo<Person,Person>();//深拷贝3对象映射
            Person person2= ExpressionGenericMapper<Person, Person>.Trans(person1);//深拷贝4表达式目录树
            //修改原对象的属性
            person1.Address.City = "上海";
            //修改副本对象的属性
            person2.Name = "李四";
            person2.Address.City = "昆明";
            string result = $"原对象{JsonConvert.SerializeObject(person1)}。副本{JsonConvert.SerializeObject(person2)}";
            MessageBox.Show($"深拷贝：原对象和副本不相互影响。{result}");
        }


        /// <summary>
        /// 使用反射进行深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <returns></returns>
        static T CreateDeepCopy<T>(T original)
        {
            if (original == null)
            {
                return default(T);
            }

            Type type = original.GetType();
            object newObject = Activator.CreateInstance(type);

            foreach (FieldInfo fieldInfo in type.GetFields())
            {
                if (fieldInfo.IsStatic)
                {
                    continue;
                }

                object value = fieldInfo.GetValue(original);
                fieldInfo.SetValue(newObject, CreateDeepCopy(value));
            }

            return (T)newObject;
        }

        /// <summary>
        /// 生成表达式目录树  泛型缓存
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        public class ExpressionGenericMapper<TIn, TOut>//`2
        {
            private static Func<TIn, TOut> _FUNC = null;
            static ExpressionGenericMapper()
            {
                ParameterExpression parameterExpression = System.Linq.Expressions.Expression.Parameter(typeof(TIn), "p");
                List<MemberBinding> memberBindingList = new List<MemberBinding>();
                foreach (var item in typeof(TOut).GetProperties())
                {
                    MemberExpression property = System.Linq.Expressions.Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                    MemberBinding memberBinding = System.Linq.Expressions.Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }
                foreach (var item in typeof(TOut).GetFields())
                {
                    MemberExpression property = Expression.Field(parameterExpression, typeof(TIn).GetField(item.Name));
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }
                MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
                Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[]
                {
                    parameterExpression
                });
                _FUNC = lambda.Compile();//
            }
            public static TOut Trans(TIn t)
            {
                return _FUNC(t);
            }
        }
    }
}
