using API.Db.Context;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Enums;

namespace API.Repositories.ServiceOrder
{
    public class ServiceOrderRepository : IServiceOrderRepository
    {
        private readonly MySQLContext _context;

        public ServiceOrderRepository(MySQLContext context)
        {
            _context = context;
        }

        public ServiceOrderModel Create(ServiceOrderModel serviceOrder)
        {
            try
            {
                _context.ServiceOrders.Add(serviceOrder);
                _context.SaveChanges();
                return serviceOrder;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ServiceOrderModel> FindAll()
        {
            return _context.ServiceOrders.Include(sc => sc.Client).Include(se => se.Equipment).Include(sp => sp.Parts).ToList();
        }

        public ServiceOrderModel FindById(int id)
        {
            return _context.ServiceOrders.Include(sc => sc.Client).Include(se => se.Equipment).Include(sp => sp.Parts).FirstOrDefault(so => so.Id == id);
        }

        public List<ServiceOrderModel> FindByStatus(ServiceOrderStatus status)
        {
            return _context.ServiceOrders.Include(sc => sc.Client).Include(se => se.Equipment).Include(sp => sp.Parts).Where(so => so.Status == status).ToList();
        }

        public ServiceOrderModel Update(ServiceOrderModel serviceOrder)
        {            
            var result = _context.ServiceOrders.FirstOrDefault(so => so.Id == serviceOrder.Id);
            if (result != null)
            {
                var equipment = FindEquipmentById(serviceOrder.EquipmentId);
                if (equipment == null) return null;

                var client = FindClientById(serviceOrder.ClientId);
                if (client == null) return null;

                serviceOrder.Client = client;
                serviceOrder.Equipment = equipment;

                try
                {
                    _context.Entry(result).CurrentValues.SetValues(serviceOrder);
                    _context.SaveChanges();
                    return serviceOrder;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                return null;
            }
        }

        public bool Exists(int id)
        {
            return _context.ServiceOrders.Any(so => so.Id == id);
        }

        private EquipmentModel FindEquipmentById(int id)
        {
            return _context.Equipments.FirstOrDefault(e => e.Id == id);
        }

        private ClientModel FindClientById(int id)
        {
            return _context.Client.FirstOrDefault(c => c.Id == id);
        }

        
    }
}
