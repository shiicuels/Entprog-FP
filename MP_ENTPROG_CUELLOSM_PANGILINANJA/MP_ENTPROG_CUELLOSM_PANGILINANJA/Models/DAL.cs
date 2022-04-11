using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;  //Allows use of DataTable
using System.Configuration; //Allows the use of ConfigurationManager

namespace Entprog.DataAccess
{
    class DAL
    {
        //static are shared members
        //public static int DALCount = 0;
        private string cs = ConfigurationManager
            .ConnectionStrings["Default"].ConnectionString;
        private SqlConnection con = new SqlConnection();
        private SqlTransaction trans;
        private SqlCommand cmd = new SqlCommand();
        public void Open()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.ConnectionString = cs;
                con.Open();
            }
        }
        public void Close()
        {
            if (trans != null)
            {
                trans = null;
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        public void BeginTransaction()
        {
            Open();
            if (trans == null)
            {
                trans = con.BeginTransaction();
            }
        }
        public void Commit()
        {
            trans.Commit();
        }
        public void Rollback()
        {
            trans.Rollback();
        }
        public void SetSql(string sql)
        {
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            else
            {
                cmd.Transaction = null;
            }
            cmd.CommandText = sql;
            cmd.Connection = con;
            cmd.Parameters.Clear();
        }
        public void AddParam(string name, object value)
        {
            cmd.Parameters.AddWithValue(name, value);
        }
        public void Execute()
        {
            cmd.ExecuteNonQuery();
        }

        public object ExecuteScalar()
        {
            return cmd.ExecuteScalar();
        }

        public SqlDataReader GetReader()
        {
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
    }
}
