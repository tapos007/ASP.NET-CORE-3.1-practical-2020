using BLL.Request;
using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public class BLLDependency
    {
        public static void ALLDependency(IServiceCollection services)
        {

            services.AddTransient<IDepartmentService,DepartmentService>();
            services.AddTransient<IStudentService, StudentService>();

            AllValidationDependency(services);
            
        }

        private static void AllValidationDependency(IServiceCollection services)
        {
            services.AddTransient<IValidator<DepartInsertRequest>, DepartInsertRequestValidator>();
            services.AddTransient<IValidator<StudentInsertRequest>, StudentInsertRequestValidator>();
        }
    }
}