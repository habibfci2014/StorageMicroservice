using Amazon.S3.Model;
using Amazon.S3;
using System.Text.RegularExpressions;

namespace StorageMicroservice.Services
{
    public class FileValidationService : IFileValidationService
    {
        public string ValidateFile(IFormFile file)
        {
            long _maxAllowedPosterSize = 4194304;

            if (file.Length > _maxAllowedPosterSize)
                return "Max allowed size for poster is 4MB!";


            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                string content = System.Text.Encoding.UTF8.GetString(fileBytes);
                if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy|<php",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                {
                    return "something wrong, can't upload file";
                }
            }

            return "";
        }
    }
}
