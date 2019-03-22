using System;
using System.Collections.Generic;

namespace SQLTeacher.Models
{
    public partial class People
    {
        public People()
        {
            Classes = new HashSet<Classes>();
            Scores = new HashSet<Scores>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Acronym { get; set; }
        public int ClasseId { get; set; }
        public int RoleId { get; set; }
        public int PinCode { get; set; }

        public Classes Classe { get; set; }
        public Roles Role { get; set; }
        public ICollection<Classes> Classes { get; set; }
        public ICollection<Scores> Scores { get; set; }
    }
}
