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
    public class SocialNetworkController : Controller
    {
        #region Properties and constructor
        private readonly Context _context;

        public SocialNetworkController(Context context)
        {
            _context = context;
        }
        #endregion

        #region Index
        [HttpGet]
        public ActionResult Index()
        {
            List<SocialNetworkViewModel> SNVMList = new List<SocialNetworkViewModel>();
            var SNList = _context.SocialNetwork.ToList();

            foreach (var socialNet in SNList)
            {
                List<Users> SNUsersList = new List<Users>();
                var NetList = _context.Networks.Where(n => n.SocialNetworksId == socialNet.Id).ToList();
                foreach (var net in NetList)
                {
                    SNUsersList.Add(_context.Users.First(u => u.Id == net.UsersId));
                }

                SocialNetworkViewModel model = new SocialNetworkViewModel
                {
                    Id = socialNet.Id,
                    Name = socialNet.Name,
                    Url = socialNet.Url,
                    SocialNetworkUsers = SNUsersList
                };

                SNVMList.Add(model);
            }
            return View(new SocialNetworkListViewModel
            {
                 SocialNetworkList = SNVMList
            });
        }
        #endregion

        #region Details
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }
        #endregion

        #region Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
       

        [HttpPost]
        public ActionResult Create(AddSocialNetwork model)
        {
            try
            {
                _context.SocialNetwork.Add(new SocialNetwork
                {
                    Name = model.Name,
                    Url = model.Url
                });

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Edit
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
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
        #endregion

        #region Delete
        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
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
        #endregion
    }
}