using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteMessage.Models;
using System.ComponentModel.DataAnnotations;

namespace TesteMessage.Controllers
{
    public class DadosController : Controller
    {
        private readonly Contexto _contexto;

        public DadosController(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Dados.ToListAsync());
        }

        [HttpGet]
        public IActionResult CriarDado()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarDado(Dados dado)
        {
            if (ModelState.IsValid)
            {
                EmailAddressAttribute email = new EmailAddressAttribute();

                if (email.IsValid(dado.email))
                {
                    _contexto.Add(dado);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Alerta = "Formato do e-mail incorreto!";
                    return View(dado);
                }
            }
            else return View(dado);
        }

        [HttpGet]
        public IActionResult AtualizarDado(int id)
        {
            Dados dado = _contexto.Dados.Find(id);
            return View(dado);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarDado(int id, Dados dado)
        {
            if (ModelState.IsValid)
            {
                EmailAddressAttribute email = new EmailAddressAttribute();

                if (email.IsValid(dado.email))
                {
                    _contexto.Update(dado);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Alerta = "Formato do e-mail incorreto!";
                    return View(dado);
                }                   
            }
            else return View(dado);
        }                
    }
}