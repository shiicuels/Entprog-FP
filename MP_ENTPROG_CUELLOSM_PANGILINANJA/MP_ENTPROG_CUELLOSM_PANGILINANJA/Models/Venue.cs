using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entprog.DataAccess;
using System.ComponentModel.DataAnnotations;


namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Models
{
    public class Venue
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string VenueName { get; set; }

        [Required]
        public string Building { get; set; }

        private DAL dl = new DAL();

        public List<Venue> ViewAllVenue()
        {
            List<Venue> list = new List<Venue>();
            dl.Open();
            dl.SetSql("SELECT * FROM Venues");
            SqlDataReader dr = dl.GetReader();
            while (dr.Read() == true)
            {
                Venue u = new Venue();
                u.ID = (int)dr[0];
                u.VenueName = dr[1].ToString();
                u.Building = dr[2].ToString();

                list.Add(u);
            }
            dr.Close();
            dl.Close();

            return list;
        }
        public List<Venue> Search()
        {
            List<Venue> list = new List<Venue>();
            dl.Open();
            dl.SetSql("SELECT * FROM Venues WHERE VenueName=@vn OR Building=@b");
            dl.AddParam("@vn", VenueName);
            dl.AddParam("@b", Building);
            SqlDataReader dr = dl.GetReader();
            while (dr.Read() == true)
            {
                Venue u = new Venue();
                u.ID = (int)dr[0];
                u.VenueName = dr[1].ToString();
                u.Building = dr[2].ToString();

                list.Add(u);
            }
            dr.Close();
            dl.Close();

            return list;
        }
        public Venue Get(int id)
        {
            Venue obj = new Venue();

            dl.Open();
            dl.SetSql("SELECT * FROM Venues WHERE ID=@id");
            dl.AddParam("@id", id);
            using (SqlDataReader dr = dl.GetReader())
            {
                while (dr.Read() == true)
                {
                    obj.ID = (int)dr[0];
                   obj.VenueName = dr[1].ToString();
                   obj.Building = dr[2].ToString();
                }
            }
            dl.Close();
            return obj;
        }

        public void Edit()
        {
            dl.Open();
            dl.SetSql("UPDATE Venues SET VenueName=@vn, Building=@b WHERE ID=@id");
            dl.AddParam("@vn", VenueName);
            dl.AddParam("@b", Building);
            dl.AddParam("@id", ID);
            dl.Execute();
            dl.Close();
        }

        public void Add()
        {
            dl.Open();
            dl.SetSql("INSERT INTO Venues VALUES (@vn, @b)");
            dl.AddParam("@vn", VenueName);
            dl.AddParam("@b", Building);
            dl.Execute();
            dl.Close();
        }
    }
}