using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Security.Claims;
using C300.Models;
using Microsoft.AspNetCore.Authorization;

namespace C300.Controllers
{
    public class MovieController : Controller
    {
        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        [Authorize(Roles = "manager, member")]
        public IActionResult Index()
        {
            DataTable dt = DBUtl.GetTable("SELECT * FROM Movie");
            return View("Index", dt.Rows);

        }

        [Authorize(Roles = "manager")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "manager")]
        [HttpPost]
        public IActionResult Create(Movie perform)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Message"] = "Invalid Input";
                ViewData["Msgtype"] = "warning";
                return View("Create");
            }
            else
            {
                string insert = @"INSERT INTO Movie(Title, Director, PerformDT, Duration, Price, Theater)
                                  VALUES ('{0}', '{1}', '{2:yyyy-MM-dd HH:mm}', {3}, {4}, '{5}')";

                int res = DBUtl.ExecSQL(insert, perform.Title, perform.Director, perform.PerformDT,
                                                perform.Duration, perform.Price, perform.Theater);

                if (res == 1)
                {
                    TempData["Message"] = "Movie Created";
                    TempData["MsgType"] = "success";
                }
                else
                {
                    TempData["Message"] = DBUtl.DB_Message;
                    TempData["MsgType"] = "danger";
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "manager")]
        public IActionResult VerifyDate(DateTime performDT)
        {
            if (performDT < DateTime.Today.AddDays(14))
            {
                return Json($"Date 14 days in advance");
            }
            return Json(true);
        }

        [Authorize(Roles = "manager")]
        public IActionResult Delete(string id)
        {
            string delete = "DELETE FROM Movie WHERE Mid='{0}'";
            int res = DBUtl.ExecSQL(delete, id);
            if (res == 1)
            {
                TempData["Message"] = "Movie Deleted";
                TempData["MsgType"] = "success";
            }
            else
            {
                TempData["Message"] = DBUtl.DB_Message;
                TempData["MsgType"] = "danger";
            }

            return RedirectToAction("Index");
        }
    }
}