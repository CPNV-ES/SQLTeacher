using System;
using System.Collections.Generic;

namespace SQLTeacher.Models
{
    public partial class Queries
    {
        public Queries()
        {
            Scores = new HashSet<Scores>();
        }

        public int Id { get; set; }
        public string Statement { get; set; }
        public string Formulation { get; set; }
        public int Rank { get; set; }
        public int ExerciseId { get; set; }

        public Exercises Exercise { get; set; }
        public ICollection<Scores> Scores { get; set; }
    }
}
