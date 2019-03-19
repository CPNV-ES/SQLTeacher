using System;
using System.Collections.Generic;
using System.ComponentModel;

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
        [DisplayName("Script")]
        public string DbScript { get; set; }
        [DisplayName("Titre")]
        public string Title { get; set; }
        [DisplayName("Actif")]
        public bool IsActive { get; set; }

        public ICollection<Participants> Participants { get; set; }
        public ICollection<Queries> Queries { get; set; }
    }
}
