using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    public class Person
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Person() { }

        public Person(int id, string Name, int age) {
            this.id = id;
            this.Name = Name;
            this.Age = age;
        }

        public override string ToString()
        {
            return "Name:"+Name+",Age:"+Age;
        }
    }
}
