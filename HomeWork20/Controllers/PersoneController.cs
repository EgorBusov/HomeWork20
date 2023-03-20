﻿using HomeWork20.Context;
using HomeWork20.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork20.Controllers
{

    public class PersoneController : Controller
    {
        HomeWork20Context context;
        public PersoneController(HomeWork20Context context)
        {
            this.context = context;
        }

        /// <summary>
        /// Вывод всех записей
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.Persones = context.Persones;
            return View();
        }
        /// <summary>
        /// Вывод полной информации о записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PersoneView(int id)
        {
            ViewBag.Persone = context.Persones.Where(e => e.Id == id).FirstOrDefault();
            return View();
        }
        /// <summary>
        /// Вывод страницы добавления записи
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PersoneAdd()
        {
            return View();
        }
        /// <summary>
        /// Добавление записи
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="SurName"></param>
        /// <param name="FatherName"></param>
        /// <param name="Telephone"></param>
        /// <param name="Address"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PersoneAdd(string Name, string SurName, string FatherName, string Telephone, string Address, string Description)
        {
            Persone persone = new Persone()
            {
                Name = Name,
                SurName = SurName,
                FatherName = FatherName,
                Telephone = Telephone,
                Address = Address,
                Description = Description
            };

            context.Persones.Add(persone);

            context.SaveChanges();
            
            return Redirect("/Persone/Index");
        }
        /// <summary>
        /// Вывод страницы редактирования записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult PersoneEdit(int id)
        {
            ViewBag.Persone = context.Persones.Where(e => e.Id == id).FirstOrDefault();
            return View();
        }
    }
}
