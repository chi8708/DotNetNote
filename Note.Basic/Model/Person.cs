using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Basic
{
    public class Person
    {
        public Person()
        {
            this.Address = new Address();
        }
        public string Name { get; set; }
         public int Age { get; set; }
        public Address Address { get; set; }

        public Person Clone()
        {
            return (Person)this.MemberwiseClone();
        }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
    }
}
