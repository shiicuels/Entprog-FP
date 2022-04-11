using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Entprog.DataAccess;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Models
{
    public class AttendanceDetail
    {
        private DAL dl = new DAL();
        public int ID { get; set; }
        public int AttendanceID { get; set; }
        public string Student { get; set; }
        public string Status { get; set; }

        public AttendanceDetail GetID(int ID)
        {
            AttendanceDetail obj = new AttendanceDetail();

            dl.Open();
            dl.SetSql("SELECT * FROM AttendanceDetails WHERE ID = @id");
            dl.AddParam("@id", ID);
            using (SqlDataReader dr = dl.GetReader())
            {
                if (dr.Read() == true)
                {
                    obj.ID = (int)dr[0];
                    obj.AttendanceID = (int)dr[1];
                    obj.Student = dr[2].ToString();
                    obj.Status = dr[3].ToString();
                }
            }
            dl.Close();

            return obj;
        }
        public void Edit()
        {
            dl.Open();
            dl.SetSql("UPDATE AttendanceDetails SET Status = @stat WHERE ID = @id");
            dl.AddParam("@id", ID);
            dl.AddParam("@stat", Status);
            dl.Execute();
            dl.Close();
        }
    }
}