using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheDojoLeague.Models
{
    public class Dojo : BaseEntity
    {

        public Dojo() {
            ninjas = new List<Ninja>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Dojo Name")]
        public string DojoName { get; set; }

        [Required]
        [Display(Name = "Dojo Location")]
        public string DojoLocation { get; set; }

        [Display(Name = "Additional Dojo Information (optional)")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<Ninja> ninjas { get; set; }
    }
}