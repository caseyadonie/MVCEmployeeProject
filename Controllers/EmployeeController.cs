using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Elev8NetCoreApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Elev8NetCoreApp.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult List()
        {
           EmployeeDetailsInfo edc = new EmployeeDetailsInfo();
           DataTable dt= edc.ReturnEmployeesRecord();

           List<EmployeeDetailsInfo> edcList = new List<EmployeeDetailsInfo>();

            foreach (DataRow dr in dt.Rows)
            {
                EmployeeDetailsInfo ed = new EmployeeDetailsInfo();
                ed.Id =Convert.ToInt32( dr["Id"]);
                ed.surName = dr["surName"].ToString();
                ed.otherNames = dr["otherNames"].ToString();
                ed.mobileNo = dr["mobileNo"].ToString();
                ed.gender = dr["gender"].ToString();
                ed.address = dr["address"].ToString();

                edcList.Add(ed);

            }

           return View(edcList);
        }

        public IActionResult Create(EmployeeDetailsInfo ed)
        {
            if (!String.IsNullOrEmpty(ed.surName))
            {
                EmployeeDetailsInfo edic = new EmployeeDetailsInfo();
                int result = edic.CreateEmployeeRecord(ed);
                if (result > 0)
                {
                    ViewBag.result = "Record created successfully.";
                }
                else
                {
                    ViewBag.result = "Error creating record";
                }
            }
           
            return View();
        }

        public IActionResult Details(int Id)
        {
            EmployeeDetailsInfo edc = new EmployeeDetailsInfo();
            DataTable dt = edc.ReturnEmployeesRecord(Id);

            EmployeeDetailsInfo ed = new EmployeeDetailsInfo();
            foreach (DataRow dr in dt.Rows)
            {
                 
                ed.Id = Convert.ToInt32(dr["Id"]);
                ed.surName = dr["surName"].ToString();
                ed.otherNames = dr["otherNames"].ToString();
                ed.mobileNo = dr["mobileNo"].ToString();
                ed.gender = dr["gender"].ToString();
                ed.address = dr["address"].ToString();

            }

            return View(ed);
        }


    }
}
