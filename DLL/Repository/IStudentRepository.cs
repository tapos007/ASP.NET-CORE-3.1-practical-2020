using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DLL.DbContext;
using DLL.Model;
using DLL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace DLL.Repository
{
    public interface IStudentRepository:IRepositoryBase<Student>
    {

    }

    public class StudentRepository: RepositoryBase<Student>,IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context):base(context)
        {
                
        }
    }
}