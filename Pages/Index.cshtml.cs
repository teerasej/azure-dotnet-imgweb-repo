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
    public class IndexModel : PageModel
    {
        private HttpClient _httpClient;
        private Options _options;

        public IndexModel(HttpClient httpClient, Options options)
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
            // Initialize ImageList to avoid null reference exception
            this.ImageList = new List<string>();

            var imagesUrl = _options?.ApiUrl;

            // Only attempt to fetch images if API URL is configured
            if (!string.IsNullOrEmpty(imagesUrl))
            {
                try
                {
                    string imagesJson = await _httpClient.GetStringAsync(imagesUrl);
                    IEnumerable<string> imagesList = JsonConvert.DeserializeObject<IEnumerable<string>>(imagesJson);
                    this.ImageList = imagesList?.ToList() ?? new List<string>();
                }
                catch
                {
                    // If there's any error fetching images, keep the empty list
                    this.ImageList = new List<string>();
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Upload != null && Upload.Length > 0 && !string.IsNullOrEmpty(_options?.ApiUrl))
            {
                var imagesUrl = _options.ApiUrl;

                try
                {
                    using (var image = new StreamContent(Upload.OpenReadStream()))
                    {
                        image.Headers.ContentType = new MediaTypeHeaderValue(Upload.ContentType);
                        var response = await _httpClient.PostAsync(imagesUrl, image);
                    }
                }
                catch
                {
                    // If there's any error uploading, continue without error to avoid breaking the page
                }
            }
            return RedirectToPage("/Index");
        }
    }
}