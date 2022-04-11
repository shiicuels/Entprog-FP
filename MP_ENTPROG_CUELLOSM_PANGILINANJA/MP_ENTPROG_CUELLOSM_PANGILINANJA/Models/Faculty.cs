using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Entprog.DataAccess;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Models
{
    public class Faculty
    {
        public int ID { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }

        private DAL dl = new DAL();

        public List<Faculty> ViewAllFaculties()
        {
            List<Faculty> list = new List<Faculty>();
            dl.Open();
            dl.SetSql("SELECT * FROM Faculties");
            SqlDataReader dr = dl.GetReader();
            while (dr.Read() == true)
            {
                Faculty u = new Faculty();
                u.ID = (int)dr[0];
                u.Lastname = dr[1].ToString();
                u.Firstname = dr[2].ToString();

                list.Add(u);
            }
            dr.Close();
            dl.Close();

            return list;
        }

        public List<Faculty> Search()
        {
            List<Faculty> list = new List<Faculty>();
            dl.Open();
            dl.SetSql("SELECT * FROM Faculties WHERE Lastname=@ln OR Firstname=@fn");
            dl.AddParam("@ln", Lastname);
            dl.AddParam("@fn", Firstname);
            SqlDataReader dr = dl.GetReader();
            while (dr.Read() == true)
            {
                Faculty u = new Faculty();
                u.ID = (int)dr[0];
                u.Lastname = dr[1].ToString();
                u.Firstname = dr[2].ToString();
                list.Add(u);
            }
            dr.Close();
            dl.Close();

            return list;
        }

        public List<Faculty> SearchID()
        {
            List<Faculty> list = new List<Faculty>();
            dl.Open();
            dl.SetSql("SELECT * FROM Faculties WHERE ID=@id");
            dl.AddParam("@ID", ID);
            SqlDataReader dr = dl.GetReader();
            while (dr.Read() == true)
            {
                Faculty u = new Faculty();
                u.ID = (int)dr[0];
                u.Lastname = dr[1].ToString();
                u.Firstname = dr[2].ToString();
                list.Add(u);
            }
            dr.Close();
            dl.Close();

            return list;
        }

        public Faculty Get(int id)
        {
            Faculty obj = new Faculty();

            dl.Open();
            dl.SetSql("SELECT * FROM Faculties WHERE ID=@id");
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
            dl.SetSql("UPDATE Faculties SET Lastname=@ln, Firstname=@fn WHERE ID=@id");
            dl.AddParam("@ln", Lastname);
            dl.AddParam("@fn", Firstname);
            dl.AddParam("@id", ID);
            dl.Execute();
            dl.Close();
        }
    }
}