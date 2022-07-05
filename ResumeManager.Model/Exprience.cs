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
    public class Exprience
    {
        public Exprience()
        {

        }
        [Key]
        public int ExprienceId { get; set; }
        [ForeignKey("Applicant")]
        public int ApplicantId { get; set; }
        public virtual Applicant Applicant { get; private set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }

        [Required]
        [Range(1, 25, ErrorMessage = "Years Must be betweeen 1 and 25")]
        [DisplayName("Years Must be betweeen 1 and 25")]
        public int YearWorked { get; set; }

        [NotMapped]
        public bool IsDeleted { get; set; } = false;


    }
}
