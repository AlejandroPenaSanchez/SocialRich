using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialRich.Entities;
using SocialRichAlejandro.Models;
using SocialRichAlejandro.ViewModel;

namespace SocialRichAlejandro.Controllers
{
    public class UserController : Controller
    {
        private readonly Context _context;

        public UserController(Context context)
        {
            _context = context;
        }


        [HttpGet]
        public ActionResult Index()
        {
            var users = _context.Users.ToList();
            List<UserViewModel> list = new List<UserViewModel>();
            foreach (var user in users)
            {
                list.Add(new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Subname = user.Subname,
                    FavouriteNetwork = user.SocialNetworkId == null ? "" : _context.SocialNetwork.Where(s => user.SocialNetworkId == s.Id).Select(s => s.Name).First(),//user.SocialNetwork.Name
                    Networks = GetNetworksNames(user.Id)
                });
            }
            return View(new UsersListViewModel
            {
                UserList = list
            });
        }

        private List<SocialNetwork> GetNetworksNames(int UserId)
        {
            var networks = _context.Networks.Where(n => n.UserId == UserId).ToList();
            List<SocialNetwork> list = new List<SocialNetwork>();
            foreach (var network in networks)
            {
                list.Add(_context.SocialNetwork.Where(s => s.Id == network.SNId).First());
            }
            return list;
        }

        public ActionResult Details(int Id)
        {
            try
            {
                var user = _context.Users.Where(u => u.Id == Id).First();
                var networksList = _context.Networks.Where(n => n.UserId == user.Id).ToList();
                List<SocialNetwork> networks = new List<SocialNetwork>();
                foreach (var net in networksList)
                {
                    networks.Add(_context.SocialNetwork.Where(s => s.Id == net.SNId).First());
                }
                var model = new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Subname = user.Subname,
                    FavouriteNetwork = user.SocialNetworkId == null ? "" : _context.SocialNetwork.Where(s => s.Id == user.SocialNetworkId).First().Name,
                    Networks = networks
                };

                return View(model);
            }
            catch (Exception ex)
            {
                return Redirect("Index");
            }
        }

        // GET: User/Create
        public ActionResult Create(AddUserViewModel model)
        {
            if (model.Name != null && model.Subname != null)
            {
                var newUser = new Users
                {
                    Name = model.Name,
                    Subname = model.Subname
                };
                if (model.FavouriteNetwork > 0)
                {
                    newUser.SocialNetworkId = model.FavouriteNetwork;
                }

                var user = _context.Users.Add(newUser);

                _context.SaveChanges();

                if (model.Netwoks != null)
                {
                    foreach (var net in model.Netwoks)
                    {
                        _context.Networks.Add(new Networks
                        {
                            UserId = user.Entity.Id,
                            SNId = net,
                        });
                    }

                    _context.SaveChanges();
                }
            }
            return Redirect("User");;
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}