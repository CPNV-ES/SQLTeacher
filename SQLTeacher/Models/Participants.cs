using System;
using System.Collections.Generic;

namespace SQLTeacher.Models
{
    public partial class Participants
    {
        public int Id { get; set; }
        public int ClasseId { get; set; }
        public int ExerciseId { get; set; }

        public Classes Classe { get; set; }
        public Exercises Exercise { get; set; }
    }
}
