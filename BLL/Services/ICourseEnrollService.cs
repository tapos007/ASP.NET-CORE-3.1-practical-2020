using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Request;
using DLL.Model;
using DLL.UnitOfWork;
using Utility.Exceptions;
using Utility.Helpers;

namespace BLL.Services
{
    public interface ICourseEnrollService
    {
        Task<IEnumerable<CourseStudent>> FindAllAsync();
        Task<SuccessResponse> CreateAsync(CourseEnrollCreateRequest request);

    }

    public class CourseEnrollService : ICourseEnrollService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseEnrollService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CourseStudent>> FindAllAsync()
        {
            var courseEnroll = await _unitOfWork.CourseEnrollRepository.GetListAsynce();
            if (courseEnroll == null)
                throw new MyAppException("The course enroll list is not found!");
            return courseEnroll;
        }

        

        public async Task<SuccessResponse> CreateAsync(CourseEnrollCreateRequest request)
        {
            var courseEnroll = new CourseStudent()
            {
                CourseId = request.CourseId,
                StudentId = request.StudentId
            };

            var alreadyExists = await _unitOfWork.CourseEnrollRepository.GetAAsynce(x =>
                x.CourseId == request.CourseId
                && x.StudentId == request.StudentId);

            if (alreadyExists != null)
            {
                throw new MyAppException("hello student you are already exists in this course.");
            }
            await _unitOfWork.CourseEnrollRepository.CreateAsync(courseEnroll);
            if (await _unitOfWork.ApplicationSaveChangesAsync())
            {
                return new SuccessResponse()
                {
                    StatusCode = 200,
                    Message = "The course enrolls has been successfully done."
                };
            }

            throw new MyAppException("Something went wrong!");
        }
    }
}