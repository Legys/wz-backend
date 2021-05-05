using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WzBeatsApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;

namespace wz_backend.Controllers.AuthController
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : Controller
  {
    private WzBeatsApiContext _context;
    public AuthController(WzBeatsApiContext context)
    {
      _context = context;
    }


    [HttpPost("Login")]
    public async Task<ActionResult<User>> Login(UserLogin model)
    {
      User user = await _context.Users.FirstOrDefaultAsync(u => u.Nickname == model.Nickname && u.Password == model.Password);
      if (user != null)
      {
        await Authenticate(model.Nickname, user.IsAdmin);

        return user;

      }
      return NotFound();
    }

    [HttpPost("Register")]
    public async Task<ActionResult<User>> Register(UserRegister model)
    {

      User user = await _context.Users.FirstOrDefaultAsync(u => u.Nickname == model.Nickname);
      if (user == null)
      {
        _context.Users.Add(new User(model.Nickname, model.Password, false));
        await _context.SaveChangesAsync();

        await Authenticate(model.Nickname, user.IsAdmin);

      }
      User newUser = await _context.Users.FirstOrDefaultAsync(u => u.Nickname == model.Nickname && u.Password == model.Password);
      return newUser;

    }

    private async Task Authenticate(string userName, bool isAdmin)
    {
      var claims = new List<Claim>
        {
          new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
          new Claim(ClaimsIdentity.DefaultRoleClaimType, isAdmin ? "admin" : "user" ),
        };

      ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
      await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
       new ClaimsPrincipal(id),
       new AuthenticationProperties
       {
         IsPersistent = true
       });
    }
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      return Ok();
    }
  }
}
