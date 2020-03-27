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
            services.AddTransient<IValidator<DepartInsertRequest>, DepartInsertRequestValidator>();
        }
    }
}