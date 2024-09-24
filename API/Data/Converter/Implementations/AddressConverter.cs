using API.Data.Converter.Contract;
using API.Data.VO;
using Models;

namespace API.Data.Converter.Implementations
{
    public class AddressConverter : IParser<AddressVO, AddressModel>, IParser<AddressModel, AddressVO>
    {
        public AddressModel Parse(AddressVO origin)
        {
            if (origin == null) return null;
            return new AddressModel
            {
                Id = origin.Id,
                Street = origin.Street,
                Number = origin.Number,
                Complement = origin.Complement,
                Neighborhood = origin.Neighborhood,
                City = origin.City,               
                State = origin.State,
                ZipCode = origin.ZipCode,
            };
        }
        public AddressVO Parse(AddressModel origin)
        {
            if (origin == null) return null;
            return new AddressVO
            {
                Id = origin.Id,
                Street = origin.Street,
                Number = origin.Number,
                Complement = origin.Complement,
                Neighborhood = origin.Neighborhood,
                City = origin.City,
                State = origin.State,
                ZipCode = origin.ZipCode,
            };
        }
        public List<AddressModel> Parse(List<AddressVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }       
        public List<AddressVO> Parse(List<AddressModel> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
