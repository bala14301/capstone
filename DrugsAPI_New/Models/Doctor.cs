using System.ComponentModel.DataAnnotations;

namespace DrugsAPI_New.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string DoctorName { get; set; }

        [Required]
        [StringLength(100)]
        public string Specialization { get; set; }

        [Required]
        public int Experience { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string EmailId { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public string MobileNo { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }
    }
}
