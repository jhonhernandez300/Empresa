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
using Microsoft.AspNetCore.JsonPatch;

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
        public IActionResult GetAllClients()
        {
            try
            {
                var clients = _context.Clients.ToList();
                return Ok(clients);
            }
            catch (Exception e)
            {
                Console.Write("Error info:" + e.Message);
                return BadRequest("Error interno del servidor");
            }
        }

        [HttpGet("GetClientById/{id}", Name = "clientFounded")]
        public IActionResult GetClientById(int id)
        {
            try
            {
                var client = _context.Clients.FirstOrDefault(x => x.Id == id);

                if (client == null)
                {
                    return NotFound();
                }
                return Ok(client);
            }
            catch (Exception e)
            {
                Console.Write("Error info:" + e.Message);
                return BadRequest("Error interno del servidor");
            }
        }
        
        public string AlreadyExistsIdentityDocument(int identityDocument)
        {
            try
            {                
                if (_context.Clients.FirstOrDefault(x => x.IdentityDocument == identityDocument) != null)
                {
                    return "yes";
                }
                else {
                    return "no";
                }
                
            }
            catch (Exception e)
            {
                Console.Write("Error info:" + e.Message);
                return "Error interno del servidor";
            }
        }

        // POST: api/Client/SaveClient
        [HttpPost("SaveClient")]
        public async Task<ActionResult<Client>> SavePost([FromBody] Client client)
        {
            if (client == null)
            {
                return NotFound();
            }

            if (AlreadyExistsIdentityDocument(client.IdentityDocument) == "yes")
            {                
                return BadRequest("Ese documento de identificación ya existe");
            }

            try
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                return Ok(client);
            }
            catch (Exception e)
            {
                Console.Write("Error info:" + e.Message);
                return BadRequest("Error interno del servidor");
            }
        }

        // POST: api/Client/UpdateClient
        [HttpPatch("UpdateClient")]
        public async Task<ActionResult<Client>> UpdateClient([FromBody] Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return Ok(client);
        }

        [HttpPatch("UpdateClient3/{id}")]
        public IActionResult Patch(int id, [FromBody] Client clientPatch)
        {
            if (clientPatch != null)
            {
                var client = _context.Clients.FirstOrDefault(x => x.Id == id);

                if (client != null)
                {
                    client.IdentityDocument = clientPatch.IdentityDocument;
                    _context.SaveChanges();
                    //return Ok(clientPatch);
                    return Ok(client);
                }
            }
            return BadRequest();
        }

        [HttpPatch("UpdateClient2/{id}")]
        public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Client> clientPatch)
        {
            if (clientPatch != null)
            {
                var client = _context.Clients.FirstOrDefault(x => x.Id == id);

                if (client != null)
                {
                    clientPatch.ApplyTo(client);
                    return Ok(clientPatch);
                }
            }
            return BadRequest();
        }

        [HttpPut("UpdateClient/{id}")]
        public IActionResult UpdateClient(int id, [FromBody] Client newClient)
        {
            if (newClient != null)
            {
                try
                {
                    var oldClient = _context.Clients.FirstOrDefault(x => x.Id == id);

                    if (oldClient != null)
                    {
                        _context.Clients.Update(newClient);
                        _context.SaveChanges();
                        return Ok(newClient);
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Error info:" + e.Message);
                }
            }
            return BadRequest();
        }

        [HttpPut("DeleteClient/{id}")]
        public IActionResult DeleteClient(int id)
        {
            try
            {
                var client = _context.Clients.FirstOrDefault(x => x.Id == id);

                if (client != null)
                {
                    client.Active = false;
                    _context.Clients.Update(client);
                    _context.SaveChanges();
                    return Ok(client);

                }
                return BadRequest();
            }
            catch (Exception e)
            {
                Console.Write("Error info:" + e.Message);
                return BadRequest("Error interno del servidor");
            }
        }

    }
}
