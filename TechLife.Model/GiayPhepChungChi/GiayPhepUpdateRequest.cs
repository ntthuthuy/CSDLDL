using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Model.GiayPhepChungChi
{
    public class GiayPhepUpdateRequest
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public int[] LoaiHinhId { get; set; }
    }
}
