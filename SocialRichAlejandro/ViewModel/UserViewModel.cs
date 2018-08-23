using SocialRichAlejandro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialRichAlejandro.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subname { get; set; }
        public SocialNetwork FavouriteNetwork { get; set; }
        public List<SocialNetwork> Networks { get; set; }
    }
}
