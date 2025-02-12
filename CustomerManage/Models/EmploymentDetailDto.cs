using System.ComponentModel.DataAnnotations;

namespace CustomerManage.Models
{
    public class EmploymentDetailDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(100)]
        public string JobTitle { get; set; }

        [Required]
        [Range(1000, 1000000)]
        public decimal Salary { get; set; }

        public int CustomerId { get; set; }
    }

}
