using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Basic
{
    public class _03Object
    {
        public static void Run()
        {
            CallOrder();
        }

        public static void CallOrder() 
        {
            Student student = new Student();
            student.Name = "sutdent1";


            //重写（override）看变量引用（指向），隐式（new）看变量类型。
            Person student2 = new Student();
            student2.Print();// new重写会调用父类的方法。 若new的方法要调用子类的方法，需要强制类型转换，参考student3。
            student2.Print2(); //override重写会调用子类的方法。

            Person student3 =new Student();
            ((Student)student3).Print();//隐式类型转换调用子类方法。
        }

    }
}
