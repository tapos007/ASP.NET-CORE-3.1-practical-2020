using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DLL.DbContext;
using DLL.Model;
using DLL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repository
{
    public interface IOrderRepository:IRepositoryBase<Order>
    {

    }

    public class OrderRepository: RepositoryBase<Order>,IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context):base(context)
        {
                
        }
    }
}