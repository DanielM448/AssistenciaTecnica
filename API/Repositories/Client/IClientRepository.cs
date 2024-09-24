using API.Repositories.Generic;
using Models;

namespace API.Repositories.Client
{
    public interface IClientRepository : IRepository<ClientModel>
    {
        ClientModel FindByEmail(string email);
    }
}
