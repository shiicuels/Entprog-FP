using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using Entprog.DataAccess;

namespace MP_ENTPROG_CUELLOSM_PANGILINANJA.Models
{
    public class User
    {
        private DAL dl = new DAL();

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Compare ("Password")]
        [DataType(DataType.Password)]
        [Display (Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display (Name = "User Level")]
        public string UserLevel { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(50, MinimumLength = 3)]
        public string Lastname { get; set; }

        [Required]
        [Display(Name = "First name")]
        [StringLength(50, MinimumLength = 3)]
        public string Firstname { get; set; }

        public bool Active { get; set; }

        public List<string> UserLevelOptions = new List<string>()
        {
             "Administrator", "Chairperson", "Faculty", "Student"
        };


        public List<User> ViewAll()
        {
            List<User> list = new List<User>();
            dl.Open();
            dl.SetSql("SELECT * FROM Users");
            SqlDataReader dr = dl.GetReader();
            while (dr.Read() == true)
            {
                User u = new User();
                u.ID = (int)dr[0];
                u.Username = dr[1].ToString();
                u.UserLevel = dr[3].ToString();
                u.Lastname = dr[4].ToString();
                u.Firstname = dr[5].ToString();
                u.Active = (bool)dr[6];

                list.Add(u);
            }
            dr.Close();
            dl.Close();

            return list;
        }

        public List<User> ViewMe(int id)
        {
            List<User> list = new List<User>();
            dl.Open();
            dl.SetSql("SELECT * FROM Users WHERE ID=@id");
            dl.AddParam("@id", id);
            SqlDataReader dr = dl.GetReader();
            while (dr.Read() == true)
            {
                User u = new User();
                u.ID = (int)dr[0];
                u.Username = dr[1].ToString();
                u.UserLevel = dr[3].ToString();
                u.Lastname = dr[4].ToString();
                u.Firstname = dr[5].ToString();
                u.Active = (bool)dr[6];

                list.Add(u);
            }
            dr.Close();
            dl.Close();

            return list;
        }

        public void Add()
        {
            try
            {
                dl.Open();
                dl.SetSql("INSERT INTO Users Values (@un, @p, @ul, @ln, @fn, @a)");
                dl.AddParam("@un", Username);
                dl.AddParam("@p", Password);
                dl.AddParam("@ul", UserLevel);
                dl.AddParam("@ln", Lastname);
                dl.AddParam("@fn", Firstname);
                dl.AddParam("@a", Active);
                dl.Execute();

                dl.SetSql("SELECT MAX(ID) FROM Users");
                int maxID = (int)dl.ExecuteScalar();
                ID = maxID;

                if (UserLevel == "Student")
                {
                    dl.SetSql("INSERT INTO Students VALUES (@ln, @fn)");
                    dl.AddParam("@ln", Lastname);
                    dl.AddParam("@fn", Firstname);
                    dl.Execute();
                }
                else if (UserLevel == "Faculty")
                {
                    dl.SetSql("INSERT INTO Faculties VALUES (@ln, @fn)");
                    dl.AddParam("@ln", Lastname);
                    dl.AddParam("@fn", Firstname);
                    dl.Execute();
                }

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

        // GETTING A RECORD
        public User Get(int id)
        {
            User obj = new User();

            dl.Open();
            dl.SetSql("SELECT * FROM Users WHERE ID = @id");
            dl.AddParam("@id", id);
            using (SqlDataReader dr = dl.GetReader())
            {
                while (dr.Read() == true)
                {
                    obj.ID = (int)dr[0];
                    obj.Username = dr[1].ToString();
                    obj.UserLevel = dr[3].ToString();
                    obj.Lastname = dr[4].ToString();
                    obj.Firstname = dr[5].ToString();
                    obj.Active = (bool)dr[6];
                }
            }
            dl.Close();
            return obj;
        }

        public void Edit()
        {
            try
            {
                dl.Open();
                dl.SetSql("UPDATE Users SET Username=@un, Password=@p, UserLevel=@ul, Lastname=@ln, Firstname=@fn," +
                    "Active=@a WHERE ID=@id");
                dl.AddParam("@un", Username);
                dl.AddParam("@p", Password);
                dl.AddParam("@ul", UserLevel);
                dl.AddParam("@ln", Lastname);
                dl.AddParam("@fn", Firstname);
                dl.AddParam("@a", Active);
                dl.AddParam("@id", ID);
                dl.Execute();
            }
            catch (Exception)
            {
                Exception ex = new Exception("You need to fill-up all required fields");
                ex.Source = "System User";
                throw ex;
            }
            finally
            {
                dl.Close();
            }

        }

        public void Delete()
        {
            dl.Open();
            dl.SetSql("DELETE FROM Users WHERE ID=@id");
            dl.AddParam("@id", ID);
            dl.Execute();
            dl.Close();
        }
        public User Get(string Username, string Password)
        {
            try
            {
                User obj = new User();
                dl.Open();
                dl.SetSql("SELECT * FROM Users WHERE Username=@un AND Password=@pwd");
                dl.AddParam("@un", Username);
                dl.AddParam("@pwd", Password);
                using (SqlDataReader dr = dl.GetReader())
                {
                    while (dr.Read() == true)
                    {
                        obj.ID = (int)dr[0];
                        obj.Username = dr[1].ToString();
                        obj.UserLevel = dr[3].ToString();
                        obj.Lastname = dr[4].ToString();
                        obj.Firstname = dr[5].ToString();
                        obj.Active = (bool)dr[6];
                    }
                }

                return obj;

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

    }
}