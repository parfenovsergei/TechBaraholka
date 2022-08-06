using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechBaraholka.Service.Interfaces;

namespace TechBaraholka.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            var response = await _profileService.GetProfile(User.Identity.Name);
            ViewBag.Path = "/AvatarPics/DefaultAvatar.jpg";
            return View(response.Data);
        }
    }
}
