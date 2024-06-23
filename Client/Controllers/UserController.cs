using Client.Identity;
using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Client.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly HttpClient _httpClient;
        public UserController(ILogger<UserController> logger, HttpClient httpClient, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;

            _userManager = userManager;

            _httpClient = httpClient;
        }

        public async Task<IActionResult> Dashboard()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

          
            if (userId == null)
            {
                return Unauthorized();
            }

            try
            {
                var url = $"https://localhost:5003/combined-data/{userId}";

                var response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var user = await _userManager.GetUserAsync(User);

                var userName = user?.Name;

                CombinedDataModel? combinedData = JsonConvert.DeserializeObject<CombinedDataModel>(content);

                combinedData.Name = userName;

                return View(combinedData);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the user dashboard");

                return BadRequest(new { Message = "An error occurred while processing your request" });
            }
        }
    }
}
