using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialRichAlejandro.ViewModel
{
    public class AddUserViewModel
    {
        public string Name { get; set; }
        public string Subname { get; set; }
        public int FavouriteNetwork { get; set; }
        public List<int> Netwoks { get; set; } 
    }
}
