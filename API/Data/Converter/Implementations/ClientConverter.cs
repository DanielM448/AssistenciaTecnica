using API.Data.Converter.Contract;
using API.Data.VO;
using Models;

namespace API.Data.Converter.Implementations
{
    public class ClientConverter : IParser<ClientVO, ClientModel>, IParser<ClientModel, ClientVO>
    {
        private readonly EnderecoConverter _enderecoConverter;
        private readonly ServiceOrderConverter _serviceOrderConverter;
        public ClientConverter()
        {
            _enderecoConverter = new EnderecoConverter();
            _serviceOrderConverter = new ServiceOrderConverter();
        }
        public ClientModel Parse(ClientVO origin)
        {
            if (origin == null) return null;
            return new ClientModel
            {
                Id = origin.Id,
                Name = origin.Name,
                CellNumber = origin.CellNumber,
                CellNumberAlternative = origin.CellNumberAlternative,
                Email = origin.Email,
                Enderecos = _enderecoConverter.Parse(origin.Enderecos),
                ServiceOrders = _serviceOrderConverter.Parse(origin.ServiceOrders),
            };
        }
        public ClientVO Parse(ClientModel origin)
        {
            if (origin == null) return null;
            return new ClientVO
            {
                Id = origin.Id,
                Name = origin.Name,
                CellNumber = origin.CellNumber,
                CellNumberAlternative = origin.CellNumberAlternative,
                Email = origin.Email,
                Enderecos = _enderecoConverter.Parse(origin.Enderecos),
                ServiceOrders = _serviceOrderConverter.Parse(origin.ServiceOrders),
            };
        }
        public List<ClientModel> Parse(List<ClientVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
        public List<ClientVO> Parse(List<ClientModel> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
