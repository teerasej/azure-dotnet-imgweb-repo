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
            ImageList = new List<string>();
            
            // Check if API URL is configured
            if (string.IsNullOrEmpty(_options.ApiUrl))
            {
                // For development/demo purposes, show empty gallery
                // In production, this would be configured with actual API URL
                return;
            }

            try
            {
                var imagesUrl = _options.ApiUrl;
                string imagesJson = await _httpClient.GetStringAsync(imagesUrl);
                IEnumerable<string> imagesList = JsonConvert.DeserializeObject<IEnumerable<string>>(imagesJson);
                this.ImageList = imagesList.ToList<string>();
            }
            catch
            {
                // If API call fails, show empty gallery
                ImageList = new List<string>();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Upload != null && Upload.Length > 0)
            {
                // Check if API URL is configured
                if (!string.IsNullOrEmpty(_options.ApiUrl))
                {
                    try
                    {
                        var imagesUrl = _options.ApiUrl;

                        using (var image = new StreamContent(Upload.OpenReadStream()))
                        {
                            image.Headers.ContentType = new MediaTypeHeaderValue(Upload.ContentType);
                            var response = await _httpClient.PostAsync(imagesUrl, image);
                        }
                    }
                    catch
                    {
                        // If API call fails, silently continue - in production you'd want proper error handling
                    }
                }
            }
            return RedirectToPage("/Gallery");
        }
    }
}