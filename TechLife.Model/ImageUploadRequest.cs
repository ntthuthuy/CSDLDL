using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model
{
    public class ImageUploadRequest
    {
        public List<IFormFile> Images { get; set; }
    }
    public class DocumentUploadRequest
    {
        public List<IFormFile> DocumentFiles { get; set; }
    }
    public class FileUploadRequest
    {
        public IFormFile File { get; set; }
    }

    //HueCIT
    public class ImageUploadExt
    {
        public IFormFile Images { get; set; }
        public string MoTa { get; set; }
    }

    public class ImageUploadExtRequest
    {
        public List<ImageUploadExt> Images { get; set; }
    }
}
