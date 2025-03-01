using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGSystem_DataAccessLayer.DTO.RequestModel
{
    public class MembershipsRequest
    {
        [Required(ErrorMessage = "Package name cannot be empty.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Package price cannot be left blank.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be positive.")]
        public decimal Price { get; set; }
        

    }
}
