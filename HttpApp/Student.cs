using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApp
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Student() { }

        public Student(string Name, int age) {
            this.Name = Name;
            this.Age = age;
        }

        public override string ToString()
        {
            return "Name:"+Name+",Age:"+Age;
        }
    }
}
