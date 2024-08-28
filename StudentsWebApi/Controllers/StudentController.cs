using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsWebApi.Models;
using StudentsWebApi.Service.StudentService;

namespace StudentsWebApi.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentInterface _studentInterface;

        public StudentController(IStudentInterface studentInterface)
        {
            _studentInterface = studentInterface;
        }

        /// <summary>
        ///     Obtém a lista de estudantes
        /// </summary>
        /// <returns>Lista de Estudantes</returns>
        /// <response code="200">Sucess</response>
        //[Authorize]
        [HttpGet("/api/students")]
        //[Route("/api/students")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ServiceResponse<List<StudentModel>>>> GetEstudantesAsync()
        {
            return Ok(await _studentInterface.GetEstudantesAsync());
        }

        /// <summary>
        ///     Obter estudante Id
        /// </summary>
        /// <param name="id">Identificador do estudante</param>
        /// <response code="200">Sucess</response>
        //[Authorize]
        [HttpGet("/api/students/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ServiceResponse<StudentModel>>> GetEstudantesByIdAsync(int id)
        {
            ServiceResponse<StudentModel> serviceResponse = await _studentInterface.GetEstudantesByIdAsync(id);

            return Ok(serviceResponse);
        }

        /// <summary>
        ///     Cadastrar um novo estudante
        /// </summary>
        /// <param name="devEvent">Dados do estudante</param>
        /// <returns>Estudante criado</returns>
        /// <response code="201">Sucess</response>
        //[Authorize]
        [HttpPost("/api/students")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ServiceResponse<List<StudentModel>>>> CreateEstudanteAsync(StudentModel novoEstudante)
        {
            return Ok(await _studentInterface.CreateEstudanteAsync(novoEstudante));
        }

        /// <summary>
        ///     Atualiza estudante
        /// </summary>
        /// <param name="id">Identificador do estudante</param>
        /// <response code="204">Sucess</response>
        /// <response code="404">Not Found</response>
        //[Authorize]
        [HttpPut("/api/students/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResponse<List<StudentModel>>>> UpdateEstudanteAsync(StudentModel estudante)
        {
            ServiceResponse<List<StudentModel>> serviceResponse = await _studentInterface.UpdateEstudanteAsync(estudante);

            return Ok(serviceResponse);
        }

        /// <summary>
        ///     Deletar estudante Id
        /// </summary>
        /// <param name="id">Identificador do estudante</param>
        /// <response code="204">Sucess</response>
        /// <response code="404">Not Found</response>
        //[Authorize]
        [HttpDelete("/api/students/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ServiceResponse<List<StudentModel>>>> DeleteEstudanteAsync(int id)
        {
            ServiceResponse<List<StudentModel>> serviceResponse = await _studentInterface.DeleteEstudanteAsync(id);

            return Ok(serviceResponse);
        }
    }
}
