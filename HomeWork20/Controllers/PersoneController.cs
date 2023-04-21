using HomeWork20.Context;
using HomeWork20.Interfaces;
using HomeWork20.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork20.Controllers
{
    [Authorize]
    public class PersoneController : Controller
    {
        private readonly IPersoneData personeData;
        public PersoneController(IPersoneData personeData)
        {
            this.personeData = personeData;
        }

        /// <summary>
        /// Вывод всех записей
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            ViewBag.Persones = await personeData.GetPersones();
            return View();
        }
        /// <summary>
        /// Вывод полной информации о записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PersoneView(int id)
        {
            return View(await personeData.GetOnePersone(id));
        }
        /// <summary>
        /// Вывод страницы добавления записи
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
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
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> PersoneAdd(string Name, string SurName, string FatherName, string Telephone, 
            string Address, string Description)
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

            await personeData.AddPersone(persone);
            return Redirect("/Persone/Index");
        }
        /// <summary>
        /// Вывод страницы редактирования записи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PersoneEdit(int id)
        {
            return View( await personeData.GetOnePersone(id));
        }
    }
}
