using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entprog.DataAccess;
using System.ComponentModel.DataAnnotations;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }

        private DAL dl = new DAL();

        public List<Student> ViewAllStudents()
        {
            List<Student> list = new List<Student>();
            dl.Open();
            dl.SetSql("SELECT * FROM Students");
            SqlDataReader dr = dl.GetReader();
            while (dr.Read() == true)
            {
                Student u = new Student();
                u.ID = (int)dr[0];
                u.Lastname = dr[1].ToString();
                u.Firstname = dr[2].ToString();

                list.Add(u);
            }
            dr.Close();
            dl.Close();

            return list;    
        }

        public List<Student> Search()
        {
            List<Student> list = new List<Student>();
            dl.Open();
            dl.SetSql("SELECT * FROM Students WHERE Lastname=@ln OR Firstname=@fn");
            dl.AddParam("@ln", Lastname);
            dl.AddParam("@fn", Firstname);
            SqlDataReader dr = dl.GetReader();
            while (dr.Read() == true)
            {
                Student u = new Student();
                u.ID = (int)dr[0];
                u.Lastname = dr[1].ToString();
                u.Firstname = dr[2].ToString();
                list.Add(u);
            }
            dr.Close();
            dl.Close();

            return list;
        }

        public List<Student> SearchID()
        {
            List<Student> list = new List<Student>();
            dl.Open();
            dl.SetSql("SELECT * FROM Students WHERE ID=@id");
            dl.AddParam("@id", ID);
            SqlDataReader dr = dl.GetReader();
            while (dr.Read() == true)
            {
                Student u = new Student();
                u.ID = (int)dr[0];
                u.Lastname = dr[1].ToString();
                u.Firstname = dr[2].ToString();
                list.Add(u);
            }
            dr.Close();
            dl.Close();

            return list;
        }

        public Student Get(int id)
        {
            Student obj = new Student();

            dl.Open();
            dl.SetSql("SELECT * FROM Students WHERE ID=@id");
            dl.AddParam("@id", id);
            using (SqlDataReader dr = dl.GetReader())
            {
                while (dr.Read() == true)
                {
                    obj.ID = (int)dr[0];
                    obj.Lastname = dr[1].ToString();
                    obj.Firstname = dr[2].ToString();
                }
            }
            dl.Close();
            return obj;
        }

        public void Edit()
        {
            dl.Open();
            dl.SetSql("UPDATE Students SET Lastname=@ln, Firstname=@fn WHERE ID=@id");
            dl.AddParam("@ln", Lastname);
            dl.AddParam("@fn", Firstname);
            dl.AddParam("@id", ID);
            dl.Execute();
            dl.Close();
        }

    }
}