using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeManager.Model
{
    public class Applicant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = "";
        [Required]
        [StringLength(10)]
        public string Gender { get; set; } = "";
        [Required]
        [Range(25,55, ErrorMessage ="Currently, We Have No Position Vacant for Your Age")]
        [DisplayName("Age in Years")]
        public int Age { get; set; }

        [Required]
        [StringLength(50)]
        public string Qualification { get; set; } = "";

        [Required]
        [Range(1, 25, ErrorMessage = "Currently, We Have No Position Vacant for Your Exprience")]
        [DisplayName("Total Exprience in Years")]
        public int TotalExprience { get; set; }

        public string PhotoUrl { get; set; }
       
        [Display(Name ="Profile Photo")]
        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }

        public virtual List<Exprience> Expriences { get; set; } = new List<Exprience>(); // Detal Record for Applicant
    }
}
