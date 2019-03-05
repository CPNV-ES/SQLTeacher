using System;
using System.Collections.Generic;

namespace SQLTeacher.Models
{
    public partial class Classes
    {
        public Classes()
        {
            Participants = new HashSet<Participants>();
            People = new HashSet<People>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? TeacherId { get; set; }

        public People Teacher { get; set; }
        public ICollection<Participants> Participants { get; set; }
        public ICollection<People> People { get; set; }
    }
}
