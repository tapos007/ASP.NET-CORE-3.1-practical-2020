using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Request
{
    public class StudentInsertRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string RollNo { get; set; }
        public int DepartmentId { get; set; }
    }

    public class StudentInsertRequestValidator : AbstractValidator<StudentInsertRequest>
    {
        private readonly IServiceProvider _serviceProvider;

        public StudentInsertRequestValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RuleFor(x=>x.RollNo).NotEmpty().NotNull().MustAsync(RollExits).WithMessage("Roll Already exits.");
            RuleFor(x => x.Name).NotEmpty().NotNull().MustAsync(NameExits).WithMessage("Name Already exits.");
            RuleFor(x => x.DepartmentId).NotNull().NotEmpty().MustAsync(DepartmentIdExists).WithMessage("Department ID not found");;
        }

        private async Task<bool> NameExits(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            var studentService = _serviceProvider.GetRequiredService<IStudentService>();
            return await studentService.IsNameExit(name);

        }

        private async Task<bool> RollExits(string roll, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(roll))
            {
                return false;
            }
            var studentService = _serviceProvider.GetRequiredService<IStudentService>();
            return await studentService.IsRollExit(roll);
        }
        
        private async Task<bool> DepartmentIdExists(int id, CancellationToken cancellationToken)
        {
            if (id==0)
            {
                return true;
            }

            var departmentService = _serviceProvider.GetRequiredService<IDepartmentService>();

            return ! await departmentService.IsIdExits(id);
        }


    }
    public class StudentUpdateRequestValidator : AbstractValidator<StudentUpdateRequest>
    {
        private readonly IServiceProvider _serviceProvider;

        public StudentUpdateRequestValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;           
            RuleFor((x => x.RollNo)).NotNull().NotEmpty()
               .MustAsync(RollNoExists).WithMessage("RollNo not exists");
            RuleFor((x => x.Name)).NotNull().NotEmpty();
            RuleFor((x => x.Email)).NotNull().NotEmpty().EmailAddress();
                //.Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$").WithMessage(("Invalid Email"));
        }
        private async Task<bool> RollNoExists(string rollNo, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(rollNo))
            {
                return true;
            }
            var studentService = _serviceProvider.GetRequiredService<IStudentService>();

            return await studentService.IsRollExit(rollNo);
        }

    }
}
