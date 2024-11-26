using Hospital.BLL.HospitalServices.Dto;
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
        public static async Task<string> SavePhoto(IFormFile HospitalImage)
        {

            var FileName = $"{Guid.NewGuid()}-{HospitalImage.FileName}";

            var FilePath =Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "HospitalImages", FileName);
            using (var FileStream = new FileStream(FilePath, FileMode.Create))
            {
                HospitalImage.CopyTo(FileStream);

                return $"HospitalImages/{FileName}";
            }

                
        }


        public static async Task DeletePhoto(string hospitalDto)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", hospitalDto);
            if(File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
            else
            {
                return;
            }
        }
    }
}
