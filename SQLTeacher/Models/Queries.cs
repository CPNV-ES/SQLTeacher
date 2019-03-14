using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SQLTeacher.Models
{
    public partial class Queries
    {
        public Queries()
        {
            Scores = new HashSet<Scores>();
        }

        public int Id { get; set; }
        [DisplayName("Requête")]
        public string Statement { get; set; }
        [DisplayName("Question")]
        public string Formulation { get; set; }
        [DisplayName("Rang")]
        public int Rank { get; set; }
        public int ExerciseId { get; set; }

        [DisplayName("Exercice")]
        public Exercises Exercise { get; set; }
        public ICollection<Scores> Scores { get; set; }
    }
}
