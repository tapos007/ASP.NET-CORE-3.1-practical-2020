using DLL.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DLL
{
    public class DLLDependency
    {
        public static void ALLDependency(IServiceCollection services)
        {
            services.AddTransient<IStudentRepository,StudentRepository>();
            services.AddTransient<IDepartmentRepository,DepartmentRepository>();
        }
    }
}