﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using data = FrontEnd.API.Models;

namespace FrontEnd.API.Controllers
{
    public class UsuarioTiendasController : Controller
    {
        string baseurl = "http://localhost:61265/";



        // GET: UsuarioTienda
        public async Task<IActionResult> Index()
        {
            List<data.UsuarioTienda> aux = new List<data.UsuarioTienda>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.GetAsync("api/UsuarioTienda");

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.UsuarioTienda>>(auxres);
                }
            }
            return View(aux);
        }

        // GET: UsuarioTienda/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioTienda = GetById(id);


            if (usuarioTienda == null)
            {
                return NotFound();
            }

            return View(usuarioTienda);
        }

        // GET: UsuarioTienda/Create
        public IActionResult Create()
        {
            ViewData["IdTienda"] = new SelectList(getAllTiendas(), "IdTienda", "DescripcionTienda");
            ViewData["IdUsuario"] = new SelectList(getAllAspNetUsers(), "Id", "Id");
            return View();
        }

        //POST: UsuarioTienda/Create
        //To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuarioTienda,IdUsuario,IdTienda")] data.UsuarioTienda usuarioTienda)
        {
            if (ModelState.IsValid)
            {
                using (var cl = new HttpClient())
                {
                    cl.BaseAddress = new Uri(baseurl);
                    var content = JsonConvert.SerializeObject(usuarioTienda);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                    var postTask = cl.PostAsync("api/UsuarioTienda", byteContent).Result;

                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }


            ViewData["IdTienda"] = new SelectList(getAllTiendas(), "IdTienda", "DescripcionTienda", usuarioTienda.IdTienda);
            ViewData["IdUsuario"] = new SelectList(getAllAspNetUsers(), "Id", "Id", usuarioTienda.IdUsuario);

            return View(usuarioTienda);
        }

        // GET: UsuarioTienda/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var usuarioTienda = GetById(id);
            if (usuarioTienda == null)
            {
                return NotFound();
            }


            ViewData["IdTienda"] = new SelectList(getAllTiendas(), "IdTienda", "DescripcionTienda", usuarioTienda.IdTienda);
            ViewData["IdUsuario"] = new SelectList(getAllAspNetUsers(), "Id", "Id", usuarioTienda.IdUsuario);
            return View(usuarioTienda);
        }

        //// POST: UsuarioTienda/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuarioTienda,IdUsuario,IdTienda")] data.UsuarioTienda usuarioTienda)
        {
            if (id != usuarioTienda.IdUsuarioTienda)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    using (var cl = new HttpClient())
                    {
                        cl.BaseAddress = new Uri(baseurl);
                        var content = JsonConvert.SerializeObject(usuarioTienda);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                        var byteContent = new ByteArrayContent(buffer);
                        byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        var postTask = cl.PutAsync("api/UsuarioTienda/" + id, byteContent).Result;

                        if (postTask.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
                catch (Exception)
                {
                    var aux2 = GetById(id);
                    if (aux2 == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdTienda"] = new SelectList(getAllTiendas(), "IdTienda", "DescripcionTienda", usuarioTienda.IdTienda);
            ViewData["IdUsuario"] = new SelectList(getAllAspNetUsers(), "Id", "Id", usuarioTienda.IdUsuario);

            return View(usuarioTienda);
        }

        //// GET: UsuarioTienda/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarioTienda = GetById(id);
            if (usuarioTienda == null)
            {
                return NotFound();
            }

            return View(usuarioTienda);
        }

        //// POST: UsuarioTienda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await cl.DeleteAsync("api/UsuarioTienda/" + id);

                if (res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction(nameof(Index));
        }


        private bool UsuarioTiendaExists(int id)
        {
            return (GetById(id) != null);
        }
        private data.UsuarioTienda GetById(int? id)
        {
            data.UsuarioTienda aux = new data.UsuarioTienda();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/UsuarioTienda/" + id).Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<data.UsuarioTienda>(auxres);
                }
            }
            return aux;
        }

        private List<data.Tiendas> getAllTiendas()
        {

            List<data.Tiendas> aux = new List<data.Tiendas>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/Tiendas").Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.Tiendas>>(auxres);
                }
            }
            return aux;
        }
        private List<data.AspNetUsers> getAllAspNetUsers()
        {

            List<data.AspNetUsers> aux = new List<data.AspNetUsers>();
            using (var cl = new HttpClient())
            {
                cl.BaseAddress = new Uri(baseurl);
                cl.DefaultRequestHeaders.Clear();
                cl.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = cl.GetAsync("api/AspNetUsers").Result;

                if (res.IsSuccessStatusCode)
                {
                    var auxres = res.Content.ReadAsStringAsync().Result;
                    aux = JsonConvert.DeserializeObject<List<data.AspNetUsers>>(auxres);
                }
            }
            return aux;
        }

    }
}
