using API.Data.Converter.Contract;
using API.Data.VO;
using Models;

namespace API.Data.Converter.Implementations
{
    public class EnderecoConverter : IParser<EnderecoVO, EnderecoModel>, IParser<EnderecoModel, EnderecoVO>
    {
        private readonly ClientConverter _clientConverter;
        public EnderecoConverter()
        {
            _clientConverter = new ClientConverter();
        }
        public EnderecoModel Parse(EnderecoVO origin)
        {
            if (origin == null) return null;
            return new EnderecoModel
            {
                Id = origin.Id,
                Street = origin.Street,
                Number = origin.Number,
                Complement = origin.Complement,
                Neighborhood = origin.Neighborhood,
                City = origin.City,               
                State = origin.State,
                ZipCode = origin.ZipCode,
                ClientId = origin.ClientId,
                Client = _clientConverter.Parse(origin.Client),
            };
        }
        public EnderecoVO Parse(EnderecoModel origin)
        {
            if (origin == null) return null;
            return new EnderecoVO
            {
                Id = origin.Id,
                Street = origin.Street,
                Number = origin.Number,
                Complement = origin.Complement,
                Neighborhood = origin.Neighborhood,
                City = origin.City,
                State = origin.State,
                ZipCode = origin.ZipCode,
                ClientId = origin.ClientId,
                Client = _clientConverter.Parse(origin.Client),
            };
        }
        public List<EnderecoModel> Parse(List<EnderecoVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }       
        public List<EnderecoVO> Parse(List<EnderecoModel> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
