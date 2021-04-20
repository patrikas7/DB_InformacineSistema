using System;
using System.Collections.Generic;
using System.Web.Mvc;
using _2_Laboras.Repos;
using _2_Laboras.ViewModels;

namespace _2_Laboras.Controllers
{
    public class AsistentasController : Controller
    {
        AsistentasRepository asistentasRepository = new AsistentasRepository();
        TrenerisRepository trenerisRepository = new TrenerisRepository();
        public ActionResult Index()
        {
            return View(asistentasRepository.getAsistentai());
        }

        public ActionResult Create()
        {
            AsistentasEditViewModel asistentas = new AsistentasEditViewModel();
            PopulateSelections(asistentas);
            return View(asistentas);
        }

        [HttpPost]
        public ActionResult Create(AsistentasEditViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                    asistentasRepository.addAsistentas(collection);

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        public ActionResult Edit(int id)
        {
            AsistentasEditViewModel asistentas = asistentasRepository.getAsistentas(id);
            PopulateSelections(asistentas);
            return View(asistentas);
        }

        [HttpPost]
        public ActionResult Edit(int id, AsistentasEditViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                    asistentasRepository.updateAsistentas(collection);

                return RedirectToAction("Index");
            }

            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        public ActionResult Delete(int id)
        {
            AsistentasEditViewModel asistentas = asistentasRepository.getAsistentas(id);
            return View(asistentas);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                asistentasRepository.deleteAsistentas(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void PopulateSelections(AsistentasEditViewModel asistentas)
        {
            var treneriai = trenerisRepository.getTreneris();
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach(var item in treneriai)
            {
                selectListItems.Add(new SelectListItem() { Value = Convert.ToString(item.id), Text = item.Vardas + " " + item.Pavarde });
            }

            asistentas.TreneriaiList = selectListItems;
        }
    }
}