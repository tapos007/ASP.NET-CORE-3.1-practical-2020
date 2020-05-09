using System;
using System.Threading.Tasks;
using DLL.DbContext;
using DLL.Repository;

namespace DLL.UnitOfWork
{
    public interface IUnitOfWork
    {
        // all repository need to added here
        
        IDepartmentRepository DepartmentRepository { get; }
        IStudentRepository StudentRepository { get; }
        ICustomerBalanceRepository CustomerBalanceRepository { get; }
        IOrderRepository OrderRepository{ get; }
        ICourseRepository CourseRepository { get; }
        
        ICourseEnrollRepository CourseEnrollRepository { get; }
        // all repository need to added here end
        
        Task<bool> ApplicationSaveChangesAsync();
        void Dispose();
    }

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed = false;

        private IDepartmentRepository _departmentRepository;
        private IStudentRepository _studentRepository;
        private ICustomerBalanceRepository _customerBalanceRepository;
        private IOrderRepository _orderRepository;
        private ICourseRepository _courseRepository;
        private ICourseEnrollRepository _courseEnrollRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IDepartmentRepository DepartmentRepository =>
            _departmentRepository ??= new DepartmentRepository(_context);
        
        public IStudentRepository StudentRepository =>
            _studentRepository ??= new StudentRepository(_context);
        
        public ICustomerBalanceRepository CustomerBalanceRepository =>
            _customerBalanceRepository ??= new CustomerBalanceRepository(_context);
        
        
        public IOrderRepository OrderRepository =>
            _orderRepository ??= new OrderRepository(_context);
        
        
        public ICourseRepository CourseRepository =>
            _courseRepository ??= new CourseRepository(_context);
        
        
        public ICourseEnrollRepository CourseEnrollRepository =>
            _courseEnrollRepository ??= new CourseEnrollRepository(_context);
        
        

        public async Task<bool> ApplicationSaveChangesAsync()
        {
            // if (await _context.SaveChangesAsync() > 0)
            // {
            //     return true;
            // }
            //
            // return false;
          return  await _context.SaveChangesAsync() > 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }

            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}