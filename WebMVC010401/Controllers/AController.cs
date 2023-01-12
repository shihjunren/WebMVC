using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Versioning;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMVC010401.Models;

namespace WebMVC010401.Controllers
{
    public class AController : Controller
    {
        //public string getResponse() 
        //{
        //    Response.Clear();
        //    Response.ContentType = "application/octet-stream";
        //    Response.Filter.Close();
        //    Response.WriteFile(@"C:\QNote\images.jpg");
        //    Response.End();
        //    return "";
        //}

        public ActionResult demoForm() 
        {
            ViewBag.ANS = "?";
            if (!string.IsNullOrEmpty(Request.Form["txtA"])) 
            {
                double a = Convert.ToDouble(Request.Form["txtA"]);
                double b = Convert.ToDouble(Request.Form["txtB"]);
                double c = Convert.ToDouble(Request.Form["txtC"]);
                ViewBag.a = a; 
                ViewBag.b = b;
                ViewBag.c = c; 
                double d = (b * b - 4 * a * c);
                if (d < 0)
                    ViewBag.ANS = "算術不成立";
                else if (d == 0)
                    ViewBag.ANS = (-b) / (2 * a);
                else 
                { 
                d = System.Math.Sqrt(d);
                double x1 = (-b + d) / (2 * a);
                double x2 = (-b - d) / (2 * a);
                ViewBag.ANS =x1.ToString("0.0#")+"or X = "+x2.ToString("0.0#");
                
                }
            }
            return View();
        }

        public string testingQuery() 
        {
            return "目前客戶數:" +(new CCustomeFactory()).queryAll().Count.ToString();
            
        }

        
        public string testingUpdate(int? id)
        {
            if (id == null)
                return "請指定ID";
            CClass1 x = new CClass1();
            x.fId = (int)id;
            x.fName = "Coco";
            x.fPhone = "0980156794";
            x.fEmail = "tes1223@gmail.com";
            x.fAddress = "test.123..";
            x.fPassword = "456";
            
            (new CCustomeFactory()).update(x);
            return "資料修改成功";
        }
        public string testingDelete(int? id)
        {
            if (id == null)
                return "請指定ID";
            (new CCustomeFactory()).delete((int)id);
            return "資料刪除成功";
        }

        public string testingInsert()
        {
            CClass1 x = new CClass1();
            x.fName = "test";
            //x.fPhone = "0986456123";
            //x.fEmail = "test123";
            x.fAddress = "test...";
            //x.fPassword = "123";
            (new CCustomeFactory()).create(x);
            return "新增成功";
        }

        public ActionResult bindingById(int? id)
        {
            CClass1 x = null;
            if (id != null)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=LAB;Integrated Security=True";
                //Data Source=.;Initial Catalog=LAB;Integrated Security=True
                //伺服器總管>連接伺服器>選擇資料庫>進階
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "select * from fCustomer where fid=" + id.ToString(), con);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                     x = new CClass1()
                    {
                        fEmail = reader["fEmail"].ToString(),
                        fPhone = reader["fPhone"].ToString(),
                        fName = reader["fName"].ToString(),
                        fId = (int)reader["fid"]

                    };            


                }
                con.Close();
            }
            return View(x);
        }


        public ActionResult showById(int? id)
        {
            if (id != null)            
            { 
            SqlConnection con = new SqlConnection();
            con.ConnectionString = @"Data Source=.;Initial Catalog=LAB;Integrated Security=True";
            //Data Source=.;Initial Catalog=LAB;Integrated Security=True
            //伺服器總管>連接伺服器>選擇資料庫>進階
            con.Open();
            SqlCommand cmd = new SqlCommand(
                "select * from fCustomer where fid=" + id.ToString(), con);
            SqlDataReader reader = cmd.ExecuteReader();
            
                if (reader.Read())
                {
                    CClass1 x = new CClass1()
                    {
                        fEmail = reader["fEmail"].ToString(),
                        fPhone = reader["fPhone"].ToString(),
                        fName = reader["fName"].ToString(),
                        fId = (int)reader["fid"]
                        
                    };
                ViewBag.kk = x;
                    

                }
                con.Close();
            }
            return View();
        }

       

        //public ActionResult showById()
        //{
        //    return View();
        //}

        //public string queryByID(int? id)
        //{
        //    if (id == null)
        //        return "請指定要查詢的ID";
        //    SqlConnection con = new SqlConnection();
        //    con.ConnectionString = @"Data Source=.;Initial Catalog=LAB;Integrated Security=True";
        //    //Data Source=.;Initial Catalog=LAB;Integrated Security=True
        //    //伺服器總管>連接伺服器>選擇資料庫>進階
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand(
        //        "select * from fCustomer where fid="+id.ToString(), con);
        //    SqlDataReader reader= cmd.ExecuteReader();
        //    string s = "查無資料";
        //    if (reader.Read())
        //    {
        //        //s = reader["fName"].ToString()+"/"+reader["fPhone"].ToString();
        //        s = reader["fName"].ToString() + "<br/>" + reader["fPhone"].ToString(); //換行

        //    }
        //    con.Close();
        //    return s;
        //}

        public string demoServer() {
            return "目前伺服器的實體位置:"+ Server.MapPath(".");
        }


        public string demoRaraneter(int id)  //不能沒有參數 (若參數為id 可以省略 "id=")
                                              //https://localhost:44390/A/demoRaraneter/?id=0
                                              //https://localhost:44390/A/demoRaraneter/0 (o)
                                              //https://localhost:44390/A/demoRaraneter/  (x)
        {

            if (id == 0)
                return "XBox 加入購物車";
            else if (id == 1)
                return "PSS 加入購物車";
            else if (id == 2)
                return "Switch 加入購物車";
            return "none";
        }

        public string demoRequest() //可以省略參數
               //https://localhost:44390/A/demoRequest/?id=0
               //https://localhost:44390/A/demoRequest/  (o)
        {
            string id = Request.QueryString["id"];
            if (id == "0")
                return "XBox 加入購物車";
            else if (id == "1")
                return "PSS 加入購物車";
            else if (id == "2")
                return "Switch 加入購物車";
            return "none";
        }

        public string sayHello() 
        {
            return "Hello!";
        }

        public string getNum()
        {
            ClottoGen x = new ClottoGen();
            return x.getnumbers();
        }
        // GET: A
        public ActionResult Index()
        {
            return View();
        }
    }
}