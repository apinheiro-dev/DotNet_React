using StudentsWebApi.Models;
using StudentsWebApi.DataContext;
using Microsoft.EntityFrameworkCore;

namespace StudentsWebApi.Service.StudentService
{
    public class StudentService: IStudentInterface
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<StudentModel>>> GetEstudantesAsync()
        {
            ServiceResponse<List<StudentModel>> serviceResponse = new ServiceResponse<List<StudentModel>>();

            try
            {
                serviceResponse.Dados = _context.Students.ToList();

                if(serviceResponse.Dados.Count == 0)
                {
                    serviceResponse.Mensagem = "Nenhum dado encontrado!";
                }
            }
            catch (Exception ex) 
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<StudentModel>> GetEstudantesByIdAsync(int id)
        {
            ServiceResponse<StudentModel> serviceResponse = new ServiceResponse<StudentModel>();

            try
            {
                StudentModel student = _context.Students.FirstOrDefault(x => x.Id == id);

                if(student == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Estudante não localizado!";
                    serviceResponse.Sucesso = false;
                }

                serviceResponse.Dados = student;
            }
            catch(Exception ex) 
            { 
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<StudentModel>>> CreateEstudanteAsync(StudentModel novoEstudante)
        {
            ServiceResponse<List<StudentModel>> serviceResponse = new ServiceResponse<List<StudentModel>>();

            try
            {

                if (novoEstudante == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Informar dados!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                _context.Add(novoEstudante);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Students.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<StudentModel>>> UpdateEstudanteAsync(StudentModel estudante)
        {
            ServiceResponse<List<StudentModel>> serviceResponse = new ServiceResponse<List<StudentModel>>();

            try
            {
                StudentModel student = _context.Students.AsNoTracking().ToList().FirstOrDefault(x => x.Id == estudante.Id);

                if (student == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Estudante não localizado!";
                    serviceResponse.Sucesso = false;
                }

                
                student.Nome = estudante.Nome;
                student.Idade = estudante.Idade;
                student.Serie = estudante.Serie;
                student.NotaMedia = estudante.NotaMedia;
                student.Endereco = estudante.Endereco;
                student.NomePai = estudante.NomePai;
                student.NomeMae = estudante.NomeMae;
                student.DataNascimento = estudante.DataNascimento;

                _context.Students.Update(estudante);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Students.ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<StudentModel>>> DeleteEstudanteAsync(int id)
        {
            ServiceResponse<List<StudentModel>> serviceResponse = new ServiceResponse<List<StudentModel>>();

            try
            {
                StudentModel student = _context.Students.FirstOrDefault(x => x.Id == id);

                if (student == null)
                {
                    serviceResponse.Dados = null;
                    serviceResponse.Mensagem = "Estudante não localizado!";
                    serviceResponse.Sucesso = false;

                    return serviceResponse;
                }

                _context.Students.Remove(student);
                await _context.SaveChangesAsync();

                serviceResponse.Dados = _context.Students.ToList();

            }
            catch (Exception ex)
            {
                serviceResponse.Mensagem = ex.Message;
                serviceResponse.Sucesso = false;
            }

            return serviceResponse;
        }
    }
}
