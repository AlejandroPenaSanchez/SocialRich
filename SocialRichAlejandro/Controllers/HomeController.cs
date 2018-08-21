using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialRich.Entities;
using SocialRichAlejandro.Models;
using SocialRichAlejandro.ViewModel;

namespace SocialRichAlejandro.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //var socialnet = _context.SocialNetwork.Where(s => s.Id == 1).First().User.Name;
            var users = _context.Users.ToList();
            List<UsuarioViewModel> list = new List<UsuarioViewModel>();
            foreach (var user in users) {
                list.Add(new UsuarioViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Subname = user.Subname,
                    favouriteNetwork = _context.SocialNetwork.Where(s => user.FavouriteNetwork == s.Id).Select(s => s.Name).First(),
                    Networks = GetNetworksNames(user.Id)
                });
            }

            return View(new HomeViewModel() {
                UserList = list,
                SocialNetwokList = _context.SocialNetwork.ToList()
            });
        }


        public List<string> GetNetworksNames(int UserId) {
            var networks = _context.Networks.Where(n => n.UserId == UserId).ToList();
            List<string> list = new List<string>();
            foreach (var network in networks)
            {
                list.Add(_context.SocialNetwork.Where(s => s.Id == network.SNId).First().Name);
            }
            return list;
        }



        [HttpPost]
        public IActionResult AddUser(AddUserViewModel model) {
            var id = _context.Users.OrderByDescending(u => u.Id).First().Id + 1;
            _context.Users.Add(new Users
            {
                Id = id,
                Name = model.Name,
                Subname = model.Subname,
                FavouriteNetwork = model.FavouriteNetwork
            });


            foreach (var net in model.Netwoks)
            {
                _context.Networks.Add(new Networks
                {
                    Id = _context.Networks.OrderByDescending(u => u.Id).First().Id + 1,
                    UserId = _context.Users.OrderByDescending(u => u.Id).First().Id + 1,
                    SNId = net,
                });
            }

            _context.SaveChanges();

            return Redirect("Index"); ;
        }























        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
