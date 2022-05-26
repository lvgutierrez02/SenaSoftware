using AutoMapper;
using Contracts;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Seguimiento.API.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CompaniesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository; _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetCompanies()
        {
            var companies = _repository.Company.GetAllCompanies(trackChanges: false);
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            //throw new Exception("Exception");
            return Ok(companiesDto);
           
        }

        [HttpGet("{id}", Name = "CompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _repository.Company.GetCompany(id, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
                return NotFound(); //es para el resultado NotFound (código de estado 404).
            }
            else
            {
                var companyDto = _mapper.Map<CompanyDto>(company);
                return Ok(companyDto); //es para el buen resultado (código de estado 200)
            }
        }
         

        [HttpPost]//restringe a las peticiones POST
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company) //el parámetro que proviene del cliente en el cuerpo de la petición [FromBody]
        {
            if (company == null)
            {
                _logger.LogError("CompanyForCreationDto object sent from client is null.");

                
            return BadRequest("CompanyForCreationDto object is null");
            }
            var companyEntity = _mapper.Map<Company>(company); //mapeamos la empresa para su creación a la entidad empresa
            _repository.Company.CreateCompany(companyEntity);//lamamos al método del repositorio para la creación
            _repository.Save();//guardar la entidad en la base de datos
            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity); //asignamos la entidad empresa al objeto DTO empresa para devolverlo al cliente

            //devolverá un código de estado 201, que significa Creado. Rellenará el cuerpo de la respuesta con el nuevo objeto así como el atributo Location dentro de la cabecera
            //de la respuesta con la dirección para recuperar esa empresa.Tenemos que proporcionar el nombre de la acción, donde podemos recuperar la entidad creada.
            return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id },companyToReturn); //
        }



    }
}
