﻿using System;
using System.Threading;
using System.Threading.Tasks;
using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Request
{
    public class DepartInsertRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
    
    public class DepartInsertRequestValidator : AbstractValidator<DepartInsertRequest> {
        
        private readonly IServiceProvider _serviceProvider;

        public DepartInsertRequestValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RuleFor((x => x.Name)).NotNull().NotEmpty()
                .MinimumLength(4).MustAsync(NameExists).WithMessage("name already exists");
            RuleFor((x => x.Code)).NotNull().NotEmpty().MinimumLength(3)
                .MaximumLength(12).MustAsync(CodeExists).WithMessage("code already exists");
        }

        private async Task<bool> CodeExists(string code, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return true;
            }

            var departmentService = _serviceProvider.GetRequiredService<IDepartmentService>();

            return ! await departmentService.IsCodeExits(code);
        }

        private async Task<bool> NameExists(string name, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return true;
            }
            var departmentService = _serviceProvider.GetRequiredService<IDepartmentService>();

            return ! await departmentService.IsNameExists(name);
        }
    }
}