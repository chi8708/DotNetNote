using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Note.Basic
{
    public class _08Generic
    {
        public static void Run()
        {
            
            // 泛型方法
            int a = 1, b = 2;
            Swap<int>(ref a, ref b);
            MessageBox.Show($"a={a}, b={b}");

            // 泛型类
            var stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            MessageBox.Show(stack.Pop().ToString());
            MessageBox.Show(stack.Pop().ToString());
            MessageBox.Show(stack.Pop().ToString());
            
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public class Stack<T> where T : struct
        {
            int position;
            T[] data = new T[Array.MaxLength];

            public void Push(T obj)
            {
                data[position++] = obj;
            }

            public T Pop()
            {
                return data[--position];
            }
        }
    }
}
