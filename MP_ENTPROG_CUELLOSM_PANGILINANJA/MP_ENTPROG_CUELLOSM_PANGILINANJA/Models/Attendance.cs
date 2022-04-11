using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Entprog.DataAccess;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Models
{
    public class Attendance
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int ClasslistID { get; set; }

        [DataType (DataType.DateTime)] // remembr this
        public DateTime Date { get; set; }

        public List<AttendanceDetail> AttendanceItems = new List<AttendanceDetail>();

        private DAL dl = new DAL();

        public List<Attendance> ViewAll()
        {
            List<Attendance> list = new List<Attendance>();
            dl.Open();
            dl.SetSql("SELECT * FROM Attendances");
            using (SqlDataReader dr = dl.GetReader())
            {
                while (dr.Read() == true)
                {
                    Attendance obj = new Attendance();
                    obj.ID = (int)dr[0];
                    obj.ClasslistID = (int)dr[1];
                    obj.Date = (DateTime)dr[2];

                    list.Add(obj);
                }
            }

            return list;
        }

        public void Add()
        {
            try
            {
                dl.BeginTransaction();
                dl.SetSql("INSERT INTO Attendances VALUES(@cid, @d)");
                dl.AddParam("@cid", ClasslistID);
                dl.AddParam("@d", Date);
                dl.Execute();

                dl.SetSql("SELECT MAX(ID) FROM Attendances");
                int MaxID = (int)dl.ExecuteScalar();

                foreach (AttendanceDetail item in AttendanceItems)
                {
                    item.ID = MaxID;
                    dl.SetSql("INSERT INTO AttendanceDetails VALUES(@aid, @stu, @sta)");
                    dl.AddParam("@aid", item.ID);
                    dl.AddParam("@stu", item.Student);
                    dl.AddParam("@sta", item.Status);
                    dl.Execute();
                }
                dl.Commit();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                dl.Close();
            }
        }
        public List<Attendance> ViewMyAttendances(int classlistid)
        {
            List<Attendance> list = new List<Attendance>();
            dl.Open();
            dl.SetSql("SELECT * FROM Attendances JOIN Classlists ON Attendances.ClasslistID = Classlists.ID WHERE Attendances.ClasslistID = @id;");
            dl.AddParam("@id", classlistid);
            using (SqlDataReader dr = dl.GetReader())
            {
                while (dr.Read() == true)
                {
                    Attendance a = new Attendance();
                    a.ID = (int)dr[0];
                    a.ClasslistID = (int)dr[1];
                    a.Date = (DateTime)dr[2];
                    list.Add(a);
                }
            }
            dl.Close();
            return list;
        }
        public List<AttendanceDetail> ViewAttendees(int AttendanceID)
        {
            List<AttendanceDetail> list = new List<AttendanceDetail>();
            dl.Open();
            dl.SetSql("SELECT * FROM AttendanceDetails WHERE AttendanceID = @id");
            dl.AddParam("@id", AttendanceID);
            using (SqlDataReader dr = dl.GetReader())
            {
                while (dr.Read() == true)
                {
                    AttendanceDetail d = new AttendanceDetail();
                    d.ID = (int)dr[0];
                    d.AttendanceID = (int)dr[1];
                    d.Student = dr[2].ToString();
                    d.Status = dr[3].ToString();
                    list.Add(d);
                }
            }
            dl.Close();
            return list;
        }
        public Attendance GetDate(int id)
        {
            Attendance a = new Attendance();
            dl.Open();
            dl.SetSql("SELECT * From Attendances WHERE ID = @id");
            dl.AddParam("@id", id);
            using (SqlDataReader dr = dl.GetReader())
            {
                while (dr.Read() == true)
                {
                    a.ID = (int)dr[0];
                    a.ClasslistID = (int)dr[1];
                    a.Date = (DateTime)dr[2];
                }
            }
            dl.Close();
            return a;
        }
        public void Void(int AttendanceID)
        {
            dl.BeginTransaction();
            dl.SetSql("DELETE FROM Attendances WHERE ID = @id");
            dl.AddParam("@id", AttendanceID);
            dl.Execute();

            dl.SetSql("DELETE FROM AttendanceDetails WHERE AttendanceID = @id");
            dl.AddParam("@id", AttendanceID);
            dl.Execute();
            dl.Commit();
            dl.Close();
        }
        public Attendance GetID(int id)
        {
            Attendance ah = new Attendance();
            dl.Open();
            dl.SetSql("SELECT * FROM Attendances WHERE ID = @id");
            dl.AddParam("@id", id);
            using (SqlDataReader dr = dl.GetReader())
            {
                if (dr.Read() == true)
                {
                    ah.ID = (int)dr[0];
                    ah.ClasslistID = (int)dr[1];
                    ah.Date = (DateTime)dr[2];
                }
            }
            dl.Close();
            return ah;
        }
    }
}