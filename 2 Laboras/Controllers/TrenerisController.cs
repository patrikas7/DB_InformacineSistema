using System;
using System.Web.Mvc;
using _2_Laboras.Models;
using _2_Laboras.Repos;
using System.Collections.Generic;

namespace _2_Laboras.Controllers
{
    public class TrenerisController: Controller
    {
        TrenerisRepository trenerisRepository = new TrenerisRepository();

        public ActionResult Index()
        {
            return View(trenerisRepository.getTreneris());
        }

        public ActionResult Create()
        {
            Treneris treneris = new Treneris();
            return View(treneris);
        }

        [HttpPost]
        public ActionResult Create(Treneris treneris)
        {
            try
            {
                if (ModelState.IsValid)
                    trenerisRepository.addTreneris(treneris);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(treneris);
            }
        }

        public ActionResult Edit(int id)
        {
            return View(trenerisRepository.getTreneris(id));
        }

        [HttpPost]
        public ActionResult Edit(Treneris treneris)
        {
            try
            {
                if (ModelState.IsValid)
                    trenerisRepository.updateTreneris(treneris);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(treneris);
            }
        }

        public ActionResult Delete(int id)
        {
            return View(trenerisRepository.getTreneris(id));
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Treneris treneris = trenerisRepository.getTreneris(id);
                List<Asistentas> asistentas = trenerisRepository.getAsistentai(id);

                if(asistentas.Count > 0)
                {
                    ViewBag.klaida = "Negalima ištrinti trenerio, nes jis turi asistentus";
                    return View(treneris);
                }
                else
                {
                    trenerisRepository.deleteTreneris(id);
                }
                
                return RedirectToAction("Index");
            }

            catch
            {
                return View();
            }
        }

    }

}