using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.Tour
{
    public class TourUpdateRequest
    {
        public TourVm Tour { get; set; }

        public ImageUploadRequest Images { get; set; }
    }
}
