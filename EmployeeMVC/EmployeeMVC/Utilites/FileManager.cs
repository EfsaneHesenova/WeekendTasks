using EmployeeMVC.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualBasic;

namespace EmployeeMVC.Utilites
{
    public static class FileManager
    {

        public static bool CheckType(this IFormFile formFile)
            => formFile.ContentType.Contains("image");

        public static bool CheckSize(this IFormFile formFile, int Size)
        {
            if (formFile.Length > Size * 1024 * 1024)
            {
                return false;
            }
            return true;
        }
        public static string Upload(this IFormFile formFile, string envPath, string folder)
        {
            string path = envPath + folder;
            string fileName = Path.GetFileNameWithoutExtension(formFile.FileName);
            if (fileName.Length > 50 )
            {
                fileName = fileName.Substring(0, 49) + Path.GetExtension(formFile.FileName);
            }

            fileName= Guid.NewGuid().ToString() + fileName;
            using (FileStream fileStream = new FileStream(path + fileName, FileMode.Create))
            {
              formFile.CopyTo(fileStream);
            }
        
            return fileName;
        }
    }
}
