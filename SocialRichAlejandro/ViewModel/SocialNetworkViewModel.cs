using SocialRichAlejandro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialRichAlejandro.ViewModel
{
    public class SocialNetworkViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Users> SocialNetworkUsers { get; set; }
    }
}
