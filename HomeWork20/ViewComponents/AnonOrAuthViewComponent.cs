
using Microsoft.AspNetCore.Mvc;

namespace HomeWork20.ViewComponents
{
    public class AnonOrAuthViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
