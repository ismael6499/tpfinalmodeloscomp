using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using WebAppGUI.DataMapper;
using WebAppGUI.Modelos;

namespace WebAppGUI.Controllers
{
    public class CintaController : Controller
    {
        private CintaMapper mapper;

        public CintaController()
        {
            mapper = new CintaMapper();
        }

        public ActionResult Index()
        {
            List<Cinta> listado = mapper.Listar();

            return View(listado);
        }

        public ActionResult Details(int id)
        {
            var entidad = mapper.Get(id);
            return View(entidad);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cinta cinta)
        {
            try
            {
                mapper.Agregar(cinta);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                ModelState.AddModelError("",e.Message);

                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(Cinta cinta)
        {
            try
            {
                mapper.Actualizar(cinta);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                ModelState.AddModelError("",e.Message);

                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            var entidad = mapper.Get(id);
            return View(entidad);
        }

        [HttpPost]
        public ActionResult Delete(Cinta cinta)
        {
            try
            {
                mapper.Eliminar(cinta);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception e)
            {
                ModelState.AddModelError("",e.Message);

                return View();
            }
        }
    }
}