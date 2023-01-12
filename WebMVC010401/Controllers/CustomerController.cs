using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC010401.Models;

namespace WebMVC010401.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        
        [HttpPost]
        public ActionResult Edit(CClass1 x)
        {
            //CClass1 x = new CClass1();
            //x.fId = Convert.ToInt32(Request.Form["txtID"]);
            //x.fName = Request.Form["txtName"];
            //x.fPhone = Request.Form["txtPhone"];
            //x.fEmail = Request.Form["txtEmail"];
            //x.fAddress = Request.Form["txtAddress"];
            //x.fPassword = Request.Form["txtPassword"];
            (new CCustomeFactory()).update(x);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            CClass1 x = (new CCustomeFactory()).queryById((int)id);
            return View(x);
        }


        public ActionResult Delete(int? id)
        {
           if(id!=null)
            {
                (new CCustomeFactory()).delete((int)id);
            }           
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            List<CClass1> datas = null;
            string keyword = Request.Form["txtKeyword"];
             if(string.IsNullOrEmpty(keyword))
                datas = (new CCustomeFactory()).queryAll();
             else
                datas = (new CCustomeFactory()).queryBykeyword(keyword);
            return View(datas);
        }
        public ActionResult Create() 
        {
            return View();
        }
        public ActionResult Save()
        {
            CClass1 x = new CClass1();
            x.fName = Request.Form["txtName"];
            x.fPhone = Request.Form["txtPhone"];
            x.fEmail = Request.Form["txtEmail"];
            x.fAddress = Request.Form["txtAddress"];
            x.fPassword = Request.Form["txtPassword"];
            (new CCustomeFactory()).create(x);
            return RedirectToAction("Index");
        }
    }
}