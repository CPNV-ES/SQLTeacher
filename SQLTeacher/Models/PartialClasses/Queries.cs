using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;

namespace SQLTeacher.Models
{
    [ModelMetadataType(typeof(QueriesMetadata))]
    public partial class Queries
    {
        public Boolean checkStatement(string query)
        {
            ArrayList al = new ArrayList();

            SqlConnection conn = new SqlConnection("Server=localhost;Database=TestExercice1;Trusted_Connection=True;");
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
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
                    al.Add(numb);
                }
            }
            finally
            {
                conn.Close();
                conn.Dispose();
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
