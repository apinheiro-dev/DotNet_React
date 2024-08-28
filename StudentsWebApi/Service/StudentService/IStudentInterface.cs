using StudentsWebApi.Models;

namespace StudentsWebApi.Service.StudentService
{
    public interface IStudentInterface
    {
        Task<ServiceResponse<List<StudentModel>>> GetEstudantesAsync();
        Task<ServiceResponse<StudentModel>> GetEstudantesByIdAsync(int id);
        Task<ServiceResponse<List<StudentModel>>> CreateEstudanteAsync(StudentModel novoEstudante);
        Task<ServiceResponse<List<StudentModel>>> UpdateEstudanteAsync(StudentModel estudante);
        Task<ServiceResponse<List<StudentModel>>> DeleteEstudanteAsync(int id);
    }
}
