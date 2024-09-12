using API.Data.Converter.Implementations;
using API.Data.VO;
using API.Repositories.Generic;
using Models;

namespace API.Business.Implementations
{
    public class RoleBusinessImplementations : IRoleBusiness
    {
        private readonly IRepository<RoleModel> _repository;
        private readonly RoleConverter _converter;
        public RoleBusinessImplementations(IRepository<RoleModel> repository)
        {
            _repository = repository;
            _converter = new RoleConverter();
        }

        public List<RoleVO> FindAll()
        {
            return _converter.Parse(_repository.FindAll());
        }

        public RoleVO FindByID(int id)
        {
            return _converter.Parse(_repository.FindByID(id));
        }
    }
}
