using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialRichAlejandro.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(150)]
        public string Subname { get; set; }


        public int? SocialNetworkId { get; set; }
        public virtual SocialNetwork SocialNetwork { get; set; }
    }
}
