using System;
using System.ComponentModel.DataAnnotations;

namespace TheDojoLeague.Models
{
    public class Ninja : BaseEntity {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ninja Name")]
        public string NinjaName { get; set; }

        [Required]
        [Display(Name = "Ninjaing Level (1-10)")]
        public int NinjaLevel { get; set; }

        [Display(Name = "Assigned Dojo?")]
        public Dojo dojo { get; set; }

        [Display(Name = "Description (optional)")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}