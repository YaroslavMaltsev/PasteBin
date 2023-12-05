using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PastBin.Web.Models;
using PasteBinWeb.Dto;

namespace PasteBinWeb.Controllers
{
    public class HomeController : Controller
    {

        [HttpPost]
        public async Task<IActionResult> Index(string hash)
        {
            GetPastDto pastDto = new GetPastDto();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44373/api/Past/" + hash))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        GetPastDto getPastDto = JsonConvert.DeserializeObject<GetPastDto>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            return View(pastDto);
        }




        public ViewResult GetPastById() => View();

        [HttpPost]
        public async Task<IActionResult> GetPastById(int id)
        {
            GetPastDto pastDto = new GetPastDto();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44373/api/Past/" + id))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        GetPastDto getPastDto= JsonConvert.DeserializeObject<GetPastDto>(apiResponse);
                    }
                    else
                        ViewBag.StatusCode = response.StatusCode;
                }
            }
            return View(pastDto);
        }
    }
}
