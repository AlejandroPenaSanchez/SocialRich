using SocialRichAlejandro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialRichAlejandro.ViewModel
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subname { get; set; }
        public string FavouriteNetwork { get; set; }
        public List<string> Networks { get; set; }
    }
}
