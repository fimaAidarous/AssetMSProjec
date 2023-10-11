using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace AssetMSProject.Controllers
{
    [Authorize]
    public class AppRoleController : Controller
    {
        
        private readonly RoleManager<IdentityRole> _roleManager;
        public AppRoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        //List All the roles created by users
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpPost]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(IdentityRole model) 
        {
            // avoid duplicate role
            if(!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");  
        }
    }
}
