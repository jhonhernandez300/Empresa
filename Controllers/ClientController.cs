using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empresa.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Empresa.Controllers
{
    [Route("api/Client")]
    [EnableCors("CorsPolicy")]
    public class ClientController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllClients")]
        public IEnumerable<Client> GetAllClients()
        {
            return _context.Clients.ToList();
        }

        [HttpGet("{id}", Name = "clientFounded")]
        public IActionResult GetById(int id)
        {
            var client = _context.Clients.FirstOrDefault(x => x.Id == id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // POST: api/PostServicio        
        //[HttpPost("PostClient")]
        [HttpPost]
        //public IActionResult Post([FromBody] Client client)
        public async Task<ActionResult<Client>> Post([FromBody] Client client)
        {
            //if (ModelState.IsValid)
            //{
                //Client theClient = JsonConvert.DeserializeObject<Client>(jsonOutPut);                                
                

                _context.Clients.Add(client);
            //_context.SaveChanges();
            await _context.SaveChangesAsync();
            //return new CreatedAtRouteResult("clientCreated", new { id = client.Id }, client);

            //}
            //return BadRequest(ModelState);
            return client;
        }

        //public async Task<ActionResult<Servicio>> IngresarServicioALaBaseDeDatos(Servicio servicio)
        //{
        //    _context.Servicio.Add(servicio);
        //    await _context.SaveChangesAsync();

        //    return servicio;
        //}


        //// GET: api/GetServicio/5        
        //[HttpGet("GetServicio/{tecnico}/{semanaDelAno}")]
        //public async Task<IActionResult> GetServicio(string tecnico, int semanaDelAno)
        //{
        //    var servicio = await _context.Servicio.Where(i => i.tecnico == tecnico && i.semanaDelAno == semanaDelAno).ToListAsync();

        //    if (servicio.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(servicio);
        //}

        


    }
}
