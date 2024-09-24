using API.Data.Converter.Contract;
using API.Data.VO;
using Models;

namespace API.Data.Converter.Implementations
{
    public class PartConverter : IParser<PartVO, PartModel>, IParser<PartModel, PartVO>
    {
        public PartModel Parse(PartVO origin)
        {
            if (origin == null) return null;
            return new PartModel
            {
                Id = origin.Id,
                Name = origin.Name,
                Quantity = origin.Quantity,
                Price = origin.Price,
                OsId = origin.OsId,
            };
        }
        public PartVO Parse(PartModel origin)
        {
            if (origin == null) return null;
            return new PartVO
            {
                Id = origin.Id,
                Name = origin.Name,
                Quantity = origin.Quantity,
                Price = origin.Price,
                OsId = origin.OsId,
            };
        }

        public List<PartModel> Parse(List<PartVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
        public List<PartVO> Parse(List<PartModel> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
