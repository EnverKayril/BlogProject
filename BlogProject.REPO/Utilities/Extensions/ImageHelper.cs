using BlogProject.SERVICE.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.REPO.Utilities.Extensions
{
    public class ImageHelper
    {
        private readonly IWebHostEnvironment _web;

        public ImageHelper(IWebHostEnvironment web)
        {
            _web = web;
        }

        public async Task<string> ImageUpload(IFormFile photoFile)
        {
            string fileExtension = Path.GetExtension(photoFile.FileName);
            DateTime dateTime = DateTime.Now;
            string fileName = $"{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";
            string wwwroot = _web.WebRootPath;
            //var path = Path.Combine($"{wwwroot}/img", fileName);
            var path = Path.Combine(wwwroot, "img", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await photoFile.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}