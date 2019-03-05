using System;
using System.Collections.Generic;

namespace SQLTeacher.Models
{
    public partial class Roles
    {
        public Roles()
        {
            People = new HashSet<People>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<People> People { get; set; }
    }
}
