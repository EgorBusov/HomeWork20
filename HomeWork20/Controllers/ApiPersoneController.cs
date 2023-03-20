using HomeWork20.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeWork20.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPersoneController : ControllerBase
    {
        HomeWork20Context context;
        public ApiPersoneController(HomeWork20Context context)
        {
            this.context = context;
        }
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <returns></returns>
        [Route("DeletePersone/{id}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersone(int id)
        {
            var persone = context.Persones.Where(e => e.Id == id).FirstOrDefault();            
            if (persone == null)
            {
                return NotFound();
            }

            context.Persones.Remove(persone);
            await context.SaveChangesAsync();
            
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
        public async Task<IActionResult> EditPersone(int id, string Name, string SurName, string FatherName,
                                    string Telephone, string Address, string Description)
        {
            
                var persone = context.Persones.Where(e => e.Id == id).FirstOrDefault();
                persone.Name = Name;
                persone.SurName = SurName;
                persone.FatherName = FatherName;
                persone.Telephone = Telephone;
                persone.Address = Address;
                persone.Description = Description;
            
                await context.SaveChangesAsync();
            
            return Redirect("/Persone/Index");
        }
    }
}
