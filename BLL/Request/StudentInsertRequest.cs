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
    }
    public class StudentUpdateRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string RollNo { get; set; }
    }

    public class StudentInsertRequestValidator : AbstractValidator<StudentInsertRequest>
    {
        private readonly IServiceProvider _serviceProvider;

        public StudentInsertRequestValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RuleFor((x => x.Name)).NotNull().NotEmpty()
                .MinimumLength(3);
            RuleFor((x => x.Email)).NotNull().NotEmpty().EmailAddress()
                .MustAsync(EmailExists).WithMessage("Email already exists");
            RuleFor((x => x.RollNo)).NotNull().NotEmpty()
               .MinimumLength(3).MustAsync(RollNoExists).WithMessage("RollNo already exists");
        }

        private async Task<bool> EmailExists(string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return true;
            }

            var studentService = _serviceProvider.GetRequiredService<IStudentService>();

            return !await studentService.IsEmailExists(email);
        }

        private async Task<bool> RollNoExists(string rollNo, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(rollNo))
            {
                return true;
            }
            var studentService = _serviceProvider.GetRequiredService<IStudentService>();

            return !await studentService.IsRollNoExits (rollNo);
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

            return await studentService.IsRollNoExits(rollNo);
        }

    }
}
