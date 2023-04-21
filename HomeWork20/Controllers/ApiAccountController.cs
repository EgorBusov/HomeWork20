using HomeWork20.AuthModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace HomeWork20.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiAccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        public ApiAccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        /// <summary>
        /// удаление User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("DeleteUser/{id}")]
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var thisUser = await _userManager.GetUserAsync(HttpContext.User);          
            var deleteUser = await _userManager.FindByIdAsync(id);
            if (thisUser.Id == deleteUser.Id)
            {
                ModelState.AddModelError("", "Своего пользователя удалить нельзя");
                return BadRequest(ModelState);
            }
            if (deleteUser == null)
            {
                ModelState.AddModelError("", "Пользователь не найден");

                return BadRequest(ModelState);
            }

            var result = await _userManager.DeleteAsync(deleteUser);
            if (result.Succeeded)
            {
                 return RedirectToAction("GetUsers", "Account");
            }
            else 
            {
                ModelState.AddModelError("", "Возникла какая-то ошибка");

                return BadRequest(ModelState);
            }
        }
    }
}
