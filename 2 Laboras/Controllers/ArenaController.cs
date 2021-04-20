using System;
using System.Web.Mvc;
using System.Collections.Generic;
using _2_Laboras.Repos;
using _2_Laboras.ViewModels;

namespace _2_Laboras.Controllers
{
    public class ArenaController: Controller
    {
        ArenaRepository arenaRepository = new ArenaRepository();

        // GET: Arenos
        public ActionResult Index()
        {
            // Grazinamas arenų sąrašas
            return View(arenaRepository.getArena());
        }

        // GET: Arena/Create
        public ActionResult Create()
        {
            ArenaEditViewModel arena = new ArenaEditViewModel();
            PopulateSelections(arena);
            return View(arena);
        }

        // POST: Arena/Create
        [HttpPost]
        public ActionResult Create(ArenaEditViewModel collection)
        {
            try
            {       
                bool exist = arenaRepository.findDublicates(collection.Pavadinimas);
                if (exist)
                {
                    ViewBag.klaida = "Arena su tokiu pavadinimu jau egzistuoja";
                    PopulateSelections(collection);
                    return View(collection);
                }

                if (ModelState.IsValid)
                {
                    arenaRepository.addArena(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }
        
        // GET: Arena/Edit
        public ActionResult Edit(int id)
        {
            ArenaEditViewModel arena = arenaRepository.getArena(id);
            PopulateSelections(arena);
            return View(arena);
        }

        // GET: Arena/Edit
        [HttpPost]
        public ActionResult Edit(ArenaEditViewModel collection)
        {
            try
            {
                bool exist = arenaRepository.findDbulicatesForEdit(collection.Pavadinimas,collection.id) ;
                if (exist)
                {
                    ViewBag.klaida = "Arena su tokiu pavadinimu jau egzistuoja";
                    PopulateSelections(collection);
                    return View(collection);
                }

                if (ModelState.IsValid)
                {
                    arenaRepository.updateArena(collection);
                }

                return RedirectToAction("Index");
            }

            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Arena/Delete

        public ActionResult Delete(int id)
        {
            ArenaEditViewModel arena = arenaRepository.getArena(id);
            return View(arena);
        }

        // POST: Arena/Delete
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
 
            try
            {
                arenaRepository.deleteArena(id);
                return RedirectToAction("Index");
            }

            catch
            {
                return View();
            }
        }

        public void PopulateSelections(ArenaEditViewModel arenaEditViewModel)
        {
            var miestai = arenaRepository.getMiestai();
            List<SelectListItem> selectListMiestai = new List<SelectListItem>();

            foreach (var item in miestai)
            {
                selectListMiestai.Add(new SelectListItem() { Value = Convert.ToString(item.id), Text = item.Pavadinimas });
            }

            arenaEditViewModel.MiestaiList = selectListMiestai;

        }


    }
}