using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using _2_Laboras.Models;
using _2_Laboras.Repos;
using _2_Laboras.ViewModels;

namespace _2_Laboras.Controllers
{
    public class Krepsinio_lygaController : Controller
    {
        Krepsinio_LygaRepository krepsinio_LygaRepository = new Krepsinio_LygaRepository();
        ImoneRepository ImoneRepository = new ImoneRepository();
        PrezidentasRepository prezidentasRepository = new PrezidentasRepository();
        public ActionResult Index()
        {
            return View(krepsinio_LygaRepository.getLyga());
        }

        public ActionResult Create()
        {
            KrepsinioLygaEditViewModel krepsinioLyga = new KrepsinioLygaEditViewModel();
            PopulateSelections(krepsinioLyga);
            return View(krepsinioLyga);
        }

        [HttpPost]
        public ActionResult Create(KrepsinioLygaEditViewModel krepsinio_Lyga)
        {
            try
            {
                bool exist = krepsinio_LygaRepository.findDublicates(krepsinio_Lyga.LygosPavadinimas);
                if (exist)
                {
                    ViewBag.klaida = "Komanda su tokiu pavadinimu jau egzistuoja";
                    PopulateSelections(krepsinio_Lyga);
                    return View(krepsinio_Lyga);
                }

                if (ModelState.IsValid)
                {
                    krepsinio_LygaRepository.addLyga(krepsinio_Lyga);
                    if (krepsinio_Lyga.PrezidentoKadencijos != null)
                    {
                        int lygosID = krepsinio_LygaRepository.getIDbyName(krepsinio_Lyga.LygosPavadinimas);
                        
                        foreach (var kadencijosInfo in krepsinio_Lyga.PrezidentoKadencijos)
                        {
                            if (kadencijosInfo.KadencijosPradzia < kadencijosInfo.KadencijosPabaiga)
                            {
                                prezidentasRepository.addPrezidentoKadencija(lygosID, kadencijosInfo);
                            }

                            else
                            {
                                ViewBag.klaida = "Kadencijos pabaiga turi būti vėliau nei pradžia";
                                PopulateSelections(krepsinio_Lyga);
                                return View(krepsinio_Lyga);
                            }
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(krepsinio_Lyga);
                return View(krepsinio_Lyga);
            }
        }

        public ActionResult Edit(int id)
        {
            KrepsinioLygaEditViewModel krepsinioLygaEditViewModel = new KrepsinioLygaEditViewModel();
            krepsinioLygaEditViewModel = krepsinio_LygaRepository.getLyga(id);
            krepsinioLygaEditViewModel.PrezidentoKadencijos = prezidentasRepository.getKadencijos(id);
            PopulateSelections(krepsinioLygaEditViewModel);
            return View(krepsinioLygaEditViewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id,KrepsinioLygaEditViewModel krepsinio_Lyga)
        {
            try
            {
                bool exist = krepsinio_LygaRepository.findDbulicatesForEdit(krepsinio_Lyga.LygosPavadinimas, id);
                if (exist)
                {
                    ViewBag.klaida = "Komanda su tokiu pavadinimu jau egzistuoja";
                    PopulateSelections(krepsinio_Lyga);
                    return View(krepsinio_Lyga);
                }


                krepsinio_LygaRepository.updateLyga(id,krepsinio_Lyga);
                
                if(krepsinio_Lyga.PrezidentoKadencijos != null)
                {
                    prezidentasRepository.deleteKadencija(id);
                    foreach (var prezidentas in krepsinio_Lyga.PrezidentoKadencijos)
                    {
                        if (prezidentas.KadencijosPradzia < prezidentas.KadencijosPabaiga)
                        {
                            prezidentas.fk_komanda = id;
                            prezidentasRepository.addPrezidentoKadencija(id,prezidentas);
                        }

                        else
                        {
                            ViewBag.klaida = "Kadencijos pabaiga turi būti vėliau nei pradžia";
                            PopulateSelections(krepsinio_Lyga);
                            return View(krepsinio_Lyga);
                        }
                    }
                }
              
                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(krepsinio_Lyga);
                return View(krepsinio_Lyga);
            }
        }

        public ActionResult Delete(int id)
        {
            KrepsinioLygaEditViewModel krepsinioLygaEditViewModel = new KrepsinioLygaEditViewModel();
            krepsinioLygaEditViewModel = krepsinio_LygaRepository.getLyga(id);
            krepsinioLygaEditViewModel.PrezidentoKadencijos = prezidentasRepository.getKadencijos(id);
            return View(krepsinioLygaEditViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                KrepsinioLygaEditViewModel krepsinioLygaEditViewModel = new KrepsinioLygaEditViewModel();
                krepsinioLygaEditViewModel = krepsinio_LygaRepository.getLyga(id);
                krepsinioLygaEditViewModel.PrezidentoKadencijos = prezidentasRepository.getKadencijos(id);
                prezidentasRepository.deleteKadencija(id);
                krepsinio_LygaRepository.deleteLyga(id);
                return RedirectToAction("Index");
            }

            catch
            {
                return View();
            }
        }

        public void PopulateSelections(KrepsinioLygaEditViewModel krepsinioLyga)
        {
            var imones = ImoneRepository.getImones();
            var prezidentai = prezidentasRepository.getPrezidentai();
            List<SelectListItem> selectListImones = new List<SelectListItem>();
            List<SelectListItem> selectListPrezidentai = new List<SelectListItem>();

            foreach (var imone in imones)
            {
                selectListImones.Add(new SelectListItem() { Value = Convert.ToString(imone.id), Text = imone.Pavadinimas + " - " + imone.GaminamaProdukcija});
            }

            krepsinioLyga.RemejaiList = selectListImones;
          
            foreach (var prezidentas in prezidentai)
            {
                selectListPrezidentai.Add(new SelectListItem() { Value = Convert.ToString(prezidentas.id), Text = prezidentas.Vardas + " " + prezidentas.Pavarde});
            }

            krepsinioLyga.PrezidentaiList = selectListPrezidentai;
        }
    }
}