using System;
using System.ComponentModel.DataAnnotations;

namespace DrugsAPI_New.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Range(0, 150)]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        public string EmailId { get; set; }

        [Required]
        [Phone]
        public string MobileNo { get; set; }

        [StringLength(200)]
        public string Disease { get; set; }

    }
}
