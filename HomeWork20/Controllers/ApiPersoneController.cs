using HomeWork20.Context;
using HomeWork20.Interfaces;
using HomeWork20.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork20.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPersoneController : ControllerBase
    {
        private readonly IPersoneData personeData;
        public ApiPersoneController(IPersoneData personeData)
        {
            this.personeData = personeData;
        }
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <returns></returns>
        [Route("DeletePersone/{id}")]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePersone(int id)
        {
            var persone = await personeData.GetOnePersone(id);            
            if (persone == null)
            {
                return NotFound();
            }
            await personeData.DeletePersone(id);
            
            return Redirect("/Persone/Index");
        }

        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Name"></param>
        /// <param name="SurName"></param>
        /// <param name="FatherName"></param>
        /// <param name="Telephone"></param>
        /// <param name="Address"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        [Route("EditPersone/{id}")]
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditPersone(int id, string Name, string SurName, string FatherName,
                                    string Telephone, string Address, string Description)
        {

            Persone persone = new Persone()
            {
                Id = id,
                Name = Name,
                SurName = SurName,
                FatherName = FatherName,
                Telephone = Telephone,
                Address = Address,
                Description = Description
            };

            await personeData.EditPersone(persone);
            
            return Redirect("/Persone/Index");
        }
    }
}
