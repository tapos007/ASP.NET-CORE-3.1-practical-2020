using System;
using System.Threading;
using System.Threading.Tasks;
using BLL.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Request
{
    public class CourseEnrollCreateRequest
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
    }

    public class CourseEnrollCreateValidator : AbstractValidator<CourseEnrollCreateRequest>
    {
        private readonly IServiceProvider _serviceProvider;

        public CourseEnrollCreateValidator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            RuleFor(x => x.CourseId).NotNull().NotEmpty()
                .MustAsync(CourseIdExists).WithMessage("The given course Id doesn't exist in our system.");
            RuleFor(x => x.StudentId).NotNull().NotEmpty()
                .MustAsync(StudentIdExists).WithMessage("The given student Id doesn't exist in our system.");
        }

        private async Task<bool> CourseIdExists(int courseId, CancellationToken token)
        {
            if (courseId == 0)
            {
                return true;
            }
            var courseEnrollService = _serviceProvider.GetRequiredService<ICourseService>();
            return await courseEnrollService.IsIdExits(courseId);
        }

        private async Task<bool> StudentIdExists(int studentId, CancellationToken token)
        {
            if (studentId == 0)
            {
                return true;
            }
            var courseEnrollService = _serviceProvider.GetRequiredService<IStudentService>();
            return await courseEnrollService.IsIdExit(studentId);
        }
    }
}