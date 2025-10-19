using AutoMapper;
using StudentPortal.Contracts.Requests;
using StudentPortal.Contracts.Responses;
using StudentPortal.Entities;

namespace StudentPortal.Mappings
{
    public class StudentProfile : Profile
    {
        public StudentProfile() 
        {
            CreateMap<Student, StudentResponse>();
            CreateMap<StudentCreateRequest, Student>();
            CreateMap<StudentUpdateRequest, Student>();
        }
    }
}
