using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO_s
{
    public class RentalDetailDto:IDto
    {
        public string CarColorName { get; set; }
        public string CarBrandName { get; set; }
        public string CarName { get; set; }
        public string CustomerFullName { get; set; }
        public short ModelYear { get; set; }
        public string Description { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime RentDate { get; set; }
    }
}
