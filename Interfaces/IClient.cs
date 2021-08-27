using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Empresa.Models;

namespace Empresa.Interfaces
{
    interface IClient
    {
        void Update(Client client);
        Task<IEnumerable<Client>> GetClientsAsync();
        Task<Client> GetClientByIdAsync(int id);
        Task<Client> GetClientByUsernameAsync(string username);        
        
    }
}
