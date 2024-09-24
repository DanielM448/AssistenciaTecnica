using API.Db.Context;
using API.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using Models;

namespace API.Repositories.Client
{
    public class ClientRepository : GenericRepository<ClientModel>, IClientRepository
    {
        public ClientRepository(MySQLContext context) : base(context) { }

        public ClientModel FindByEmail(string email)
        {
            return _context.Client.Include(cr => cr.Addresses).Include(cs => cs.ServiceOrders).FirstOrDefault(c => c.Email == email);
        }

        public List<ClientModel> FindAll()
        {
            return _context.Client.Include(cr => cr.Addresses).Include(cs => cs.ServiceOrders).ToList();
        }
    }
}
