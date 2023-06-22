using Microsoft.AspNetCore.Http;

namespace ADSeek.Models
{
    public class ActiveDirectoryUserPhotoUploadModel
    {
        public string DistinguishedName { get; set; }
        public IFormFile FormFile { get; set; }
    }
}