using System.ComponentModel.DataAnnotations;

namespace CustomerManage.Models
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public EmploymentDetailDto EmploymentDetail { get; set; }
    }

}
