using System;
using System.Collections.Generic;

namespace SQLTeacher.Models
{
    public partial class Scores
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public int Attempts { get; set; }
        public int PeopleId { get; set; }
        public int QuerieId { get; set; }

        public People People { get; set; }
        public Queries Querie { get; set; }
    }
}
