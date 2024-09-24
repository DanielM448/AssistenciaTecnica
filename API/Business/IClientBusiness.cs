using API.Data.VO;

namespace API.Business
{
    public interface IClientBusiness
    {
        ClientVO Create(ClientVO client);
        List<ClientVO> FindAll();
        ClientVO Update(ClientVO client);
        ClientVO FindByEmail(string email);
        ClientVO AddAddress(ClientVO client, AddressVO address);
        ClientVO RemoveAddress(ClientVO client, int addressID);
        ClientVO UpdateAddress(ClientVO client, AddressVO address);

    }
}
