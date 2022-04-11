using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Entprog.DataAccess;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Models
{
    public class Classlist
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display (Name = "Class Days")]
        public string ClassDays { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Faculty { get; set; }

        private DAL dl = new DAL();

        public List<ClasslistDetail> ClasslistItems = new List<ClasslistDetail>();

        public List<Classlist> ViewAll()
        {
            List<Classlist> list = new List<Classlist>();
            dl.Open();
            dl.SetSql("SELECT * FROM Classlists");
            using (SqlDataReader dr = dl.GetReader())
            {
                while (dr.Read() == true)
                {
                    Classlist obj = new Classlist();
                    obj.ID = (int)dr[0];
                    obj.ClassDays = dr[1].ToString();
                    obj.Venue = dr[2].ToString();
                    obj.Subject = dr[3].ToString();
                    obj.Faculty = dr[4].ToString() ;
                    list.Add(obj);
                }
            }
            dl.Close();
            return list;
        }

        public List<Classlist> ViewAllofFaculty()
        {
            List<Classlist> list = new List<Classlist>();
            dl.Open();
            dl.SetSql("SELECT * FROM Classlists WHERE Faculty=@fac");
            dl.AddParam("@fac", Faculty);
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

        public List<Classlist> SelectAllEnrolledSubjects(string name)
        {
            List<Classlist> list = new List<Classlist>();
            dl.BeginTransaction();
            dl.SetSql("SELECT * FROM Classlists JOIN ClasslistDetails ON Classlists.ID = ClasslistDetails.ClassListID WHERE Student=@sn");
            dl.AddParam("@sn", name);
            using (SqlDataReader dr = dl.GetReader())
            {
                while (dr.Read() == true)
                {
                    Classlist c = new Classlist();
                    c.ID = (int)dr[0];
                    c.ClassDays = dr[1].ToString();
                    c.Venue = dr[2].ToString();
                    c.Subject = dr[3].ToString();
                    c.Faculty = dr[4].ToString();

                    list.Add(c);
                }
            }
            dl.Commit();
            dl.Close();

            return list;
        }

        public void Add()
        {
            try
            {
                dl.BeginTransaction();
                dl.SetSql("INSERT INTO Classlists VALUES (@cd, @vid, @subid, @fid)");
                dl.AddParam("@cd", ClassDays);
                dl.AddParam("@vid", Venue);
                dl.AddParam("@subid", Subject);
                dl.AddParam("@fid", Faculty);
                dl.Execute();

                dl.SetSql("SELECT MAX(ID) FROM Classlists");
                int maxID = (int)dl.ExecuteScalar();

                foreach (ClasslistDetail item in ClasslistItems)
                {
                    item.ClasslistID = maxID;
                    dl.SetSql("INSERT INTO ClasslistDetails VALUES (@cid, @stu)");
                    dl.AddParam("@cid", item.ClasslistID);
                    dl.AddParam("@stu", item.Student);
                    dl.Execute();
                }

                dl.Commit();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                dl.Close();
            }

        }


        public Classlist GetID(int id)
        {
            Classlist obj = new Classlist();

            dl.Open();
            dl.SetSql("SELECT * FROM Classlists WHERE ID=@id");
            dl.AddParam("@id", id);
            using (SqlDataReader dr = dl.GetReader())
            {
                while (dr.Read() == true)
                {
                    obj.ID = (int)dr[0];
                    obj.ClassDays = dr[1].ToString();
                    obj.Venue = dr[2].ToString();
                    obj.Subject = dr[3].ToString();
                    obj.Faculty = dr[4].ToString();
                }
            }
            dl.Close();
            return obj;
        }

        public void Edit()
        {
            dl.Open();
            dl.SetSql("UPDATE CLasslists SET CLassDays=@cd, Venue=@v, Subject=@s, Faculty=@f WHERE ID=@id");
            dl.AddParam("@cd", ClassDays);
            dl.AddParam("@v", Venue);
            dl.AddParam("@s", Subject);
            dl.AddParam("@f", Faculty);
            dl.AddParam("@id", ID);
            dl.Execute();
            dl.Close();
        }

        public void Delete(int id)
        {
            dl.SetSql("DELETE FROM Classlists WHERE ID=@id");
            dl.AddParam("@id", id);
            dl.Execute();
            dl.Close();
        }

        public Classlist Get(int id)
        {
            Classlist obj = new Classlist();

            dl.Open();
            dl.SetSql("SELECT * FROM Classlists WHERE ID=@id");
            dl.AddParam("@id", id);
            using (SqlDataReader dr = dl.GetReader())
            {
                while (dr.Read() == true)
                {
                    obj.ID = (int)dr[0];
                    obj.ClassDays = dr[1].ToString();
                    obj.Venue = dr[2].ToString();
                    obj.Subject = dr[3].ToString();
                    obj.Faculty = dr[4].ToString();
                }
            }
            dl.Close();
            return obj;
        }

        
        public List<ClasslistDetail> ViewAllDetails(int id)
        {
            List<ClasslistDetail> list = new List<ClasslistDetail>();
            dl.Open();
            dl.SetSql("SELECT * FROM ClasslistDetails WHERE ClasslistID=@id");
            dl.AddParam("@id", id);
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

        public List<ClasslistDetail> SelectAllStudentsFaculty(string Facultyname, int id)
        {
            List<ClasslistDetail> list = new List<ClasslistDetail>();
            dl.BeginTransaction();
            dl.SetSql("SELECT * FROM ClasslistDetails JOIN Classlists ON ClasslistDetails.ClasslistID=Classlists.ID WHERE Faculty = @name AND ClasslistDetails.ClasslistID = @id;");
            dl.AddParam("@id", id);
            dl.AddParam("@name", Facultyname);
            using (SqlDataReader dr = dl.GetReader())
            {
                while (dr.Read() == true)
                {
                    ClasslistDetail c = new ClasslistDetail();
                    c.ID = (int)dr[0];
                    c.ClasslistID = (int)dr[1];
                    c.Student = dr[2].ToString();

                    list.Add(c);
                }
            }
            dl.Commit();
            dl.Close();

            return list;
        }
    }
}