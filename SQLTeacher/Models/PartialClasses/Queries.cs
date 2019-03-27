using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;

namespace SQLTeacher.Models
{
    [ModelMetadataType(typeof(QueriesMetadata))]
    public partial class Queries
    {
        public Boolean checkStatement(string query)
        {
            ArrayList dataFromStudent = new ArrayList();
            ArrayList dataCorrect = new ArrayList();

            SqlConnection conn = new SqlConnection("Server=localhost;Database=TestExercice1;Trusted_Connection=True;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                // Get student result
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.Connection.Open();

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Object[] numb = new Object[dr.FieldCount];

                    // Get the Row with all its column values..
                    dr.GetValues(numb);

                    // Add this Row to ArrayList.
                    dataFromStudent.Add(numb);
                }
                dr.Close();

                // Get correct value
                cmd.CommandText = this.Statement;

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Object[] numb = new Object[dr.FieldCount];

                    // Get the Row with all its column values..
                    dr.GetValues(numb);

                    // Add this Row to ArrayList.
                    dataCorrect.Add(numb);
                }
                dr.Close();
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            for (int i = 0; i < dataFromStudent.Count; i++)
            {
                if (dataFromStudent[i].Equals(dataCorrect[i]))
                {
                    var tutu = false;
                    break;
                }
            }

            return (query == this.Statement);
        }
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
