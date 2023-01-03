using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.Tour
{
    public class TourCreateRequest
    {
        public TourModel Tour { get; set; }

        public ImageUploadRequest Images { get; set; }
    }
}
