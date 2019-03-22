using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace SQLTeacher.Models
{
    [ModelMetadataType(typeof(ExercisesMetadata))]
    public partial class Exercises
    {

    }

    public class ExercisesMetadata
    {
        public int Id { get; set; }
        [DisplayName("Script")]
        public string DbScript { get; set; }
        [DisplayName("Titre")]
        public string Title { get; set; }
        [DisplayName("Actif")]
        public bool IsActive { get; set; }
    }
}
