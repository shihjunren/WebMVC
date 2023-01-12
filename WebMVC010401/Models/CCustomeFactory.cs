using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace WebMVC010401.Models
{
    public class CCustomeFactory
    {
        public CClass1 queryById(int? fid)
        {
            string sql = " select * from fCustomer where fid = @K_FID";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_FID", (Object)fid));
            List<CClass1> list = queryBySql(sql, paras);
            if(list.Count==0)
                return null;
            return list[0];

        }

        public List<CClass1> queryAll()
        {
            string sql = " select * from fCustomer ";
            return queryBySql(sql,null);
        }

        private List<CClass1> queryBySql(string sql, List<SqlParameter> paras)
        {
            List<CClass1> list = new List<CClass1>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=LAB;Integrated Security=True";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            if(paras != null)
                cmd.Parameters.AddRange(paras.ToArray());
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
               CClass1  x = new CClass1()
                {
                    fEmail = reader["fEmail"].ToString(),
                    fPhone = reader["fPhone"].ToString(),
                    fName = reader["fName"].ToString(),
                    fId = (int)reader["fid"],
                    fAddress = reader["fAddress"].ToString(),
                   fPassword = reader["fPassword"].ToString()

               };

                list.Add(x);
            }
            con.Close();
            return list;
        }

        private void executeSql(string sql, List<SqlParameter> para)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=LAB;Integrated Security=True";

            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddRange(para.ToArray());

            cmd.ExecuteNonQuery();
        }

        public void delete(int id)
        {
            //string sql = "Delete from fCustomer where fid =" + id.ToString();
            //executeSql(sql);
            string sql = "Delete from fCustomer where fid =@K_FID";
            List<SqlParameter> paras = new List<SqlParameter>();
            paras.Add(new SqlParameter("K_FID", (Object)id));
            executeSql(sql, paras);
        }

        public void update(CClass1 p)
        {
            List<SqlParameter> paras = new List<SqlParameter>();
            string sql = " Update fCustomer set ";
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += "fName=@K_FNAME,";
                paras.Add(new SqlParameter("K_FNAME", (Object)p.fName));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += "fEmail=@K_FEMAIL,";
                paras.Add(new SqlParameter("K_FEMAIL", (Object)p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += "fPhone=@K_FPHONE,";
                paras.Add(new SqlParameter("K_FPHONE", (Object)p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += "fAddress=@K_FADDRESS,";
                paras.Add(new SqlParameter("K_FADDRESS", (Object)p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += "fPassword=@K_FPASSWORD,";
                paras.Add(new SqlParameter("K_FPASSWORD", (Object)p.fPassword));
            }
            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += " where fid =@K_FID ";
            paras.Add(new SqlParameter("K_FID", (Object)p.fId));
            executeSql(sql, paras);
        }

        public void create(CClass1 p)
        {
            List<SqlParameter> paras= new List<SqlParameter>();
            string sql = "Insert into fCustomer(";
            if(! string.IsNullOrEmpty(p.fName))
                sql += "fName,";
            if (!string.IsNullOrEmpty(p.fEmail))
                sql += "fEmail,";
            if (!string.IsNullOrEmpty(p.fPhone))
                sql += "fPhone,";
            if (!string.IsNullOrEmpty(p.fAddress))
                sql += "fAddress,";
            if (!string.IsNullOrEmpty(p.fPassword))
                sql += "fPassword";
            if(sql.Substring(sql.Length-1,1)==",")
                sql =sql.Substring(0,sql.Length-1);
            sql += ")Values(";
            //sql += "'" + p.fName + "',";
            //sql += "'" + p.fEmail + "',";
            //sql += "'" + p.fPhone + "',";            
            //sql += "'" + p.fAddress + "',";
            //sql += "'" + p.fPassword + "')";
            
            if (!string.IsNullOrEmpty(p.fName))
            {
                sql += "@K_FNAME,"; 
                paras.Add(new SqlParameter("K_FNAME",(Object)p.fName));
            }
            if (!string.IsNullOrEmpty(p.fEmail))
            {
                sql += "@K_FEMAIL,";
                paras.Add(new SqlParameter("K_FEMAIL", (Object)p.fEmail));
            }
            if (!string.IsNullOrEmpty(p.fPhone))
            {
                sql += "@K_FPHONE,";
                paras.Add(new SqlParameter("K_FPHONE", (Object)p.fPhone));
            }
            if (!string.IsNullOrEmpty(p.fAddress))
            {
                sql += "@K_FADDRESS,";
                paras.Add(new SqlParameter("K_FADDRESS", (Object)p.fAddress));
            }
            if (!string.IsNullOrEmpty(p.fPassword))
            {
                sql += "@K_FPASSWORD,";
                paras.Add(new SqlParameter("K_FPASSWORD", (Object)p.fPassword));
            }

            if (sql.Substring(sql.Length - 1, 1) == ",")
                sql = sql.Substring(0, sql.Length - 1);
            sql += ")";
            executeSql(sql, paras);

            //SqlConnection con = new SqlConnection();
            //con.ConnectionString = @"Data Source=.;Initial Catalog=LAB;Integrated Security=True";
            //con.Open();
            //SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.ExecuteNonQuery();
            //executeSql(sql, para);
        }

        

        internal List<CClass1> queryBykeyword(string keyword)
        {
            string sql = " select * from fCustomer where fName = @K_KEYWORD";
            sql += " or fPhone like @K_KEYWORD";
            sql += " or fEmail like @K_KEYWORD";
            sql += " or fAddress like @K_KEYWORD";
            List<SqlParameter> paras= new List<SqlParameter>();
            paras.Add(new SqlParameter("@K_KEYWORD", "%" + (object)keyword + "%"));
            return queryBySql(sql, paras);
        }
    }
}