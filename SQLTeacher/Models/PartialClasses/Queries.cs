using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace SQLTeacher.Models
{
    [ModelMetadataType(typeof(QueriesMetadata))]
    public partial class Queries
    {
    }

    public class QueriesMetadata
    {
        public int Id { get; set; }
        [DisplayName("Requête")]
        public string Statement { get; set; }
        [DisplayName("Question")]
        public string Formulation { get; set; }
        [DisplayName("Rang")]
        public int Rank { get; set; }
        [DisplayName("Exercice")]
        public int ExerciseId { get; set; }

        [DisplayName("Exercice")]
        public Exercises Exercise { get; set; }
    }
}
