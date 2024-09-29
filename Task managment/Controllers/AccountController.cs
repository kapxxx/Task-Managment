using DTOs.RequestDTO;
using Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;

namespace Task_managment.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly JwtTokenHelper _jwtTokenHelper;

		public AccountController(UserManager<ApplicationUser> userManager, JwtTokenHelper jwtTokenHelper)
		{
			_userManager = userManager;
			_jwtTokenHelper = jwtTokenHelper;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto model)
		{
			var user = await _userManager.FindByNameAsync(model.UserName);
			if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
			{
				var token = _jwtTokenHelper.GenerateToken(user);
				return Ok(new { Token = token });
			}

			return Unauthorized("Invalid login attempt.");
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDTO model)
		{
			if (ModelState.IsValid)
			{
				// Create a new ApplicationUser
				var user = new ApplicationUser
				{
					UserName = model.UserName,
					Email = model.Email
				};

				// Create the user using the UserManager
				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
				{
					// Optionally: Generate token for the newly registered user
					return Ok(new { Message = "User registered successfully!" });
				}

				// If creation failed, return errors
				var errors = result.Errors.Select(e => e.Description).ToList();
				return BadRequest(new { Errors = errors });
			}

			return BadRequest("Invalid data submitted.");
		}
	}
}
