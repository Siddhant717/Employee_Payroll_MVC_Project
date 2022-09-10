using EmployeePayRoll.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EmployeePayRoll.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeContext employeeContext;

        public EmployeeController(EmployeeContext employeeContext)
        {
            this.employeeContext = employeeContext;

        }
        
        public IActionResult Getall()
        {
            try
            {

                return View(this.employeeContext.EmployeeDetails.ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult GetByEmployeeId(int Id)
        {
            try
            {
                var empDetail = employeeContext.EmployeeDetails.Where(x => x.EmployeeId == Id).FirstOrDefault();
                return View(empDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Get
        public IActionResult Remove(int Id)
        {
            try
            {
                var empInfo = employeeContext.EmployeeDetails.Where(x => x.EmployeeId == Id).FirstOrDefault();
                if (empInfo == null)
                {
                    return NotFound();
                }
                return View(empInfo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // POST
        [HttpPost, ActionName("RemoveConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveConfirmed(int Id)
        {
            try
            {
                var employeeDetail = employeeContext.EmployeeDetails.Find(Id);
                if (employeeDetail == null)
                {
                    return NotFound();
                }
                employeeContext.EmployeeDetails.Remove(employeeDetail);
                employeeContext.SaveChanges();
                return Json(new { html = Helper.RenderRazorViewToString(this, "Getall", employeeContext.EmployeeDetails.ToList()) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult AddEmployee(int id = 0)
        {
            if (id == 0)
            {
                return View(new EmployeeModel());
            }
            else
            {
                var emp = this.employeeContext.EmployeeDetails.Find(id);
                if (emp == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(emp);
                }
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddEmployee([Bind("EmployeeId,FirstName,LastName,Age,Department,Salary,JoinedDate")] EmployeeModel employeeModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (employeeModel != null)
                    {
                        employeeModel.JoinedDate = DateTime.Now;
                        employeeContext.EmployeeDetails.Add(employeeModel);
                        employeeContext.SaveChanges();
                    }
                    return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "Getall", this.employeeContext.EmployeeDetails.ToList()) });
                }
                return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "Add", this.employeeContext.EmployeeDetails.ToList()) });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult Edit(int Id = 0)
        {
            try
            {
                if (Id == 0)
                {
                    return NotFound();
                }
                var employeeDataModel = employeeContext.EmployeeDetails.Find(Id);
                if (employeeDataModel == null)
                {
                    return NotFound();
                }
                return View(employeeDataModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, [Bind("EmployeeId,FirstName,LastName,Age,Department,Salary")] EmployeeModel employeeDataModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (Id != 0)
                    {
                        employeeDataModel.JoinedDate = employeeDataModel.JoinedDate;
                        employeeContext.EmployeeDetails.Update(employeeDataModel);
                        employeeContext.SaveChanges();
                    }
                    return Json(new { IsValid = true, html = Helper.RenderRazorViewToString(this, "Getall", this.employeeContext.EmployeeDetails.ToList()) });
                }
                return Json(new { IsValid = false, html = Helper.RenderRazorViewToString(this, "Edit", this.employeeContext.EmployeeDetails.ToList()) });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
