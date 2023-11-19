using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace AddMeTour.Service.Helpers.Images
{
    public static class ImageHelper 
    {

        public static bool CheckFileType(this IFormFile file, string type) => file.ContentType.Contains(type);
        public static bool CheckFileSize(this IFormFile file, int kb) => kb * 1024 > file.Length;

        public static string SaveFile(this IFormFile ImageFile , string path)
        {
            string fileName = Path.GetFileName(ImageFile.FileName);
            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                ImageFile.CopyTo(stream);
            }
            return fileName;
        }

        public static string CheckValidate(this IFormFile ImageFile ,string type, int kb)
        {
            int mbSize = kb / 1024;
            string result = "";
            if (!CheckFileType(ImageFile, type))
            {
                result += $"{ImageFile.FileName} file type must be {type}.";
            }
            if (!CheckFileSize(ImageFile, kb))
            {
                result += $"{ImageFile.FileName} file memory must be {mbSize} mb";
            }
            return result;
        }


        public static string ReplaceInvalidChars(string fileName)
        {
            return fileName.Replace("İ", "I")
                 .Replace("ı", "i")
                 .Replace("Ğ", "G")
                 .Replace("ğ", "g")
                 .Replace("Ü", "U")
                 .Replace("ü", "u")
                 .Replace("ş", "s")
                 .Replace("Ş", "S")
                 .Replace("Ö", "O")
                 .Replace("ö", "o")
                 .Replace("Ç", "C")
                 .Replace("ç", "c")
                 .Replace("é", "")
                 .Replace("!", "")
                 .Replace("'", "")
                 .Replace("^", "")
                 .Replace("+", "")
                 .Replace("%", "")
                 .Replace("/", "")
                 .Replace("(", "")
                 .Replace(")", "")
                 .Replace("=", "")
                 .Replace("?", "")
                 .Replace("_", "")
                 .Replace("*", "")
                 .Replace("æ", "")
                 .Replace("ß", "")
                 .Replace("@", "")
                 .Replace("€", "")
                 .Replace("<", "")
                 .Replace(">", "")
                 .Replace("#", "")
                 .Replace("$", "")
                 .Replace("½", "")
                 .Replace("{", "")
                 .Replace("[", "")
                 .Replace("]", "")
                 .Replace("}", "")
                 .Replace(@"\", "")
                 .Replace("|", "")
                 .Replace("~", "")
                 .Replace("¨", "")
                 .Replace(",", "")
                 .Replace(";", "")
                 .Replace("`", "")
                 .Replace(".", "")
                 .Replace(":", "")
                 .Replace(" ", "");
        }


        public static void DeleteFile(this string file, string root, string folder)
        {
            string path = Path.Combine(root, folder, file);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
