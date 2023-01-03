using System;
using System.Collections.Generic;
using System.Text;

namespace TechLife.Data.Entities
{
    public class Amenities
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int TypeOfBusinessId { get; set; }
        public int AmenityId { get; set; }
        public string Name { get; set; }
    }
}
