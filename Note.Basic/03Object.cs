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
        }

    }
}
