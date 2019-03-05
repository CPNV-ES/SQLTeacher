using System;
using System.Collections.Generic;

namespace SQLTeacher.Models
{
    public partial class Exercises
    {
        public Exercises()
        {
            Participants = new HashSet<Participants>();
            Queries = new HashSet<Queries>();
        }

        public int Id { get; set; }
        public string DbScript { get; set; }
        public string Title { get; set; }

        public ICollection<Participants> Participants { get; set; }
        public ICollection<Queries> Queries { get; set; }
    }
}
