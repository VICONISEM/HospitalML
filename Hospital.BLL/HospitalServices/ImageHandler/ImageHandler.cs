using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.HospitalServices.ImageHandler
{
    public  class ImageHandler
    {
        public static string SavePhoto(IFormFile HospitalImage)
        {
            var FileName = $"{Guid.NewGuid()}-{HospitalImage.FileName}";

            var FilePath =Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "HospitalImages", FileName);
            var FileStream = new FileStream(FilePath,FileMode.Create);
        
            HospitalImage.CopyTo(FileStream);
            FilePath = Path.Combine(FilePath, FileName);
            return $"HospitalImages/{FileName}";
        }
    }
}
