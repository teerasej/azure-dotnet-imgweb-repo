using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Web.Pages
{
    public class GalleryModel : PageModel
    {
        private HttpClient _httpClient;
        private Options _options;

        public GalleryModel(HttpClient httpClient, Options options)
        {
            _httpClient = httpClient;
            _options = options;
        }

        [BindProperty]
        public List<string> ImageList { get; private set; }

        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task OnGetAsync()
        {
            var imagesUrl = _options.ApiUrl;

            if (!string.IsNullOrEmpty(imagesUrl))
            {
                string imagesJson = await _httpClient.GetStringAsync(imagesUrl);

                IEnumerable<string> imagesList = JsonConvert.DeserializeObject<IEnumerable<string>>(imagesJson);

                this.ImageList = imagesList.ToList<string>();
            }
            else
            {
                this.ImageList = new List<string>();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Upload != null && Upload.Length > 0 && !string.IsNullOrEmpty(_options.ApiUrl))
            {
                var imagesUrl = _options.ApiUrl;

                using (var image = new StreamContent(Upload.OpenReadStream()))
                {
                    image.Headers.ContentType = new MediaTypeHeaderValue(Upload.ContentType);
                    var response = await _httpClient.PostAsync(imagesUrl, image);
                }
            }
            return RedirectToPage("/Gallery");
        }
    }
}