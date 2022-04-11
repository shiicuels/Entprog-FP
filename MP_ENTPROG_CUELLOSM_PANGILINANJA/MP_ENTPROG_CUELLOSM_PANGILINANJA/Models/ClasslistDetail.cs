using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Entprog.DataAccess;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Models
{
    public class ClasslistDetail
    {
        public int ID { get; set; }
        public int ClasslistID { get; set; }
        public string Student { get; set; }

        private DAL dl = new DAL();

        public List<Classlist> ViewAllofStudent(string Student)
        {
            List<Classlist> list = new List<Classlist>();
            dl.Open();
            dl.SetSql("SELECT * FROM ClasslistHeaders, ClasslistDetails WHERE ClasslistDetails.Student = @name");
            dl.AddParam("@name", Student);
            using (SqlDataReader dr = dl.GetReader())
            {
                while (dr.Read() == true)
                {
                    Classlist obj = new Classlist();
                    obj.ID = (int)dr[0];
                    obj.ClassDays = dr[1].ToString();
                    obj.Venue = dr[2].ToString();
                    obj.Subject = dr[3].ToString();
                    obj.Faculty = dr[4].ToString();
                    list.Add(obj);
                }
            }
            dl.Close();
            return list;
        }

        public List<ClasslistDetail> ViewAllStudents()
        {
            List<ClasslistDetail> list = new List<ClasslistDetail>();
            dl.Open();
            dl.SetSql("SELECT * FROM ClasslistDetails WHERE ClasslistID=@id");
            using (SqlDataReader dr = dl.GetReader())
            {
                while (dr.Read() == true)
                {
                    ClasslistDetail dtl = new ClasslistDetail();
                    dtl.ID = (int)dr[0];
                    dtl.ClasslistID = (int)dr[1];
                    dtl.Student = dr[2].ToString();

                    list.Add(dtl); ;
                }

            }
            dl.Close();
            return list;
        }

        public void DeleteStudent()
        {
            dl.Open();
            dl.SetSql("DELETE FROM ClasslistDetails WHERE ID=@id");
            dl.AddParam("@id", ID);
            dl.Execute();
            dl.Close();
        }

        public void AddMoreStudent()
        {
            try
            {
                dl.Open();
                dl.SetSql("INSERT INTO ClasslistDetails VALUES (@cid, @stu)");
                dl.AddParam("@cid", ClasslistID);
                dl.AddParam("@stu", Student);
                dl.Execute();
                dl.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}