using Microsoft.AspNetCore.Mvc;

namespace ApiConsume.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ViewResult AddFile() => View();

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile file)
        {
            string apiResponse = "";
            using (HttpClient client = new HttpClient())
            {
                var form = new MultipartFormDataContent();
                using (var fileStream = file.OpenReadStream())
                {
                    form.Add(new StreamContent(fileStream), "file", file.FileName);
                }
                using (var response = await client.PostAsync
                    ("https://localhost:44385/api/Reservation/UploadFile/UploadFile", form))
                {
                    response.EnsureSuccessStatusCode();
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return View((object)apiResponse);
        }
    }
}
