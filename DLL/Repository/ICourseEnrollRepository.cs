using DLL.DbContext;
using DLL.Model;
using DLL.UnitOfWork;

namespace DLL.Repository
{
    public interface ICourseEnrollRepository : IRepositoryBase<CourseStudent>
    {
    }

    public class CourseEnrollRepository : RepositoryBase<CourseStudent>, ICourseEnrollRepository
    {
        public CourseEnrollRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}