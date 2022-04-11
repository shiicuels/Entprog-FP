using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entprog.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Models
{
    public class Subject
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name ="Subject Code")]
        public string SubjectCode { get; set; }

        [Required]
        [Display (Name= "Description")]
        public string SubjectDescription { get; set; }

        private DAL dl = new DAL();

        public List<Subject> ViewAllSubject()
        {
            List<Subject> list = new List<Subject>();
            dl.Open();
            dl.SetSql("SELECT * FROM Subjects");
            SqlDataReader dr = dl.GetReader();
            while (dr.Read() == true)
            {
                Subject u = new Subject();
                u.ID = (int)dr[0];
                u.SubjectCode = dr[1].ToString();
                u.SubjectDescription = dr[2].ToString();

                list.Add(u);
            }
            dr.Close();
            dl.Close();

            return list;
        }
        public List<Subject> Search()
        {
            List<Subject> list = new List<Subject>();
            dl.Open();
            dl.SetSql("SELECT * FROM Subjects WHERE SubjectCode=@sc");
            dl.AddParam("@sc", SubjectCode);
            SqlDataReader dr = dl.GetReader();
            while (dr.Read() == true)
            {
                Subject u = new Subject();
                u.ID = (int)dr[0];
                u.SubjectCode = dr[1].ToString();
                u.SubjectDescription = dr[2].ToString();

                list.Add(u);
            }
            dr.Close();
            dl.Close();

            return list;
        }
        public Subject Get(int id)
        {
            Subject obj = new Subject();

            dl.Open();
            dl.SetSql("SELECT * FROM Subjects WHERE ID=@id");
            dl.AddParam("@id", id);
            using (SqlDataReader dr = dl.GetReader())
            {
                while (dr.Read() == true)
                {
                    obj.ID = (int)dr[0];
                    obj.SubjectCode = dr[1].ToString();
                    obj.SubjectDescription = dr[2].ToString();
                }
            }
            dl.Close();
            return obj;
        }
        public void Edit()
        {
            dl.Open();
            dl.SetSql("UPDATE Subjects SET SubjectCode=@sc, SubjectDescription=@sd WHERE ID=@id");
            dl.AddParam("@sc", SubjectCode);
            dl.AddParam("@sd", SubjectDescription);
            dl.AddParam("@id", ID);
            dl.Execute();
            dl.Close();
        }
        public void Add()
        {
            dl.Open();
            dl.SetSql("INSERT INTO Subjects VALUES (@sc, @sd)");
            dl.AddParam("@sc", SubjectCode);
            dl.AddParam("@sd", SubjectDescription);
            dl.Execute();
            dl.Close();
        }
    }
}