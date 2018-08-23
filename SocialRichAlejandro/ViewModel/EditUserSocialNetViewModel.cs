using SocialRichAlejandro.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialRichAlejandro.ViewModel
{
    public class EditUserSocialNetViewModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int SocialNetworkId { get; set; }
        [Required]
        public bool IsFavourite { get; set; }
    }
}
