using System;
using System.Web.Mvc;
using _2_Laboras.ViewModels;
using _2_Laboras.Repos;
using System.Collections.Generic;
using System.Linq;

namespace _2_Laboras.Controllers
{
    public class Krepsinio_komandaController: Controller
    {
        Krepsinio_komandaRepository komandaRepository = new Krepsinio_komandaRepository();
        DarbuotojasRepository darbuotojasRepository = new DarbuotojasRepository();
        public ActionResult Index()
        {
            return View(komandaRepository.getKrepsinio_Komanda());
        }

        public ActionResult Create()
        {
            KrepsinioKomandaEditViewModel krepsinioKomanda = new KrepsinioKomandaEditViewModel();
            return View(krepsinioKomanda);
        }

        [HttpPost]
        public ActionResult Create(KrepsinioKomandaEditViewModel komanda)
        {
            try
            {
                bool exist = komandaRepository.findDublicates(komanda.KrepsinioKomanda.Pavadinimas);
                if (exist)
                {
                    ViewBag.klaida = "Komanda su tokiu pavadinimu jau egzistuoja";
                    return View(komanda);
                }


                int komanda_id = komandaRepository.addKrepsinio_Komanda(komanda.KrepsinioKomanda);
                if (komanda_id < 0)
                {
                    ViewBag.failed = "Nepavyko pridėti komandos";
                    return View(komanda);
                }

                if(komanda.Darbuotojai != null)
                {
                    foreach(var darbuotojas in komanda.Darbuotojai)
                    {
                        darbuotojas.fk_krepsinioKomanda = komanda_id;
                        darbuotojasRepository.addDarbuotojas(darbuotojas);
                    }
                }

                return RedirectToAction("Index");
            }

            catch
            {
                ViewBag.klaida = "Visi darbuotojo duomenys yra reikalingi";
                return View(komanda);
            }
        }

        public ActionResult Edit(int id)
        {
            KrepsinioKomandaEditViewModel krepsinioKomandaEditViewModel = new KrepsinioKomandaEditViewModel();
            krepsinioKomandaEditViewModel.KrepsinioKomanda = komandaRepository.getKrepsinio_Komanda(id);
            krepsinioKomandaEditViewModel.Darbuotojai = darbuotojasRepository.getDarbuotojai(id);
            return View(krepsinioKomandaEditViewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, KrepsinioKomandaEditViewModel komanda)
        {
            try
            {
                bool exist = komandaRepository.findDbulicatesForEdit(komanda.KrepsinioKomanda.Pavadinimas, id);
                if (exist)
                {
                    ViewBag.klaida = "Komanda su tokiu pavadinimu jau egzistuoja";
                    return View(komanda);
                }

                komandaRepository.updateKrepsinio_Komanda(id,komanda.KrepsinioKomanda);

                if(komanda.Darbuotojai != null)
                {
                    darbuotojasRepository.deleteDarbuotojas(id);
                    foreach (var darbuotojas in komanda.Darbuotojai)
                    {
                        darbuotojas.fk_krepsinioKomanda = id;
                        darbuotojasRepository.addDarbuotojas(darbuotojas);
                    }
                } 

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.klaida = "Visi darbuotojo duomenys yra reikalingi";
                return View(komanda);
            }
        }

        public ActionResult Delete(int id)
        {
            KrepsinioKomandaEditViewModel krepsinioKomandaEditViewModel = new KrepsinioKomandaEditViewModel();
            krepsinioKomandaEditViewModel.KrepsinioKomanda = komandaRepository.getKrepsinio_Komanda(id);
            krepsinioKomandaEditViewModel.Darbuotojai = darbuotojasRepository.getDarbuotojai(id);
            return View(krepsinioKomandaEditViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                KrepsinioKomandaEditViewModel krepsinioKomandaEditViewModel = new KrepsinioKomandaEditViewModel();
                krepsinioKomandaEditViewModel.KrepsinioKomanda = komandaRepository.getKrepsinio_Komanda(id);
                krepsinioKomandaEditViewModel.Darbuotojai = darbuotojasRepository.getDarbuotojai(id);
                darbuotojasRepository.deleteDarbuotojas(id);
                komandaRepository.deleteKrepsinio_Komanda(id);
                return RedirectToAction("Index");
            }

            catch(Exception e)
            {
                ViewBag.klaida = e;
                return View();
            }
        }

    }
}