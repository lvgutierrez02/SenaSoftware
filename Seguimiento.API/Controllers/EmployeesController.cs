﻿using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Seguimiento.API.Controllers
{
    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public EmployeesController(IRepositoryManager repository, ILoggerManager logger,
        IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetEmployeesForCompany(Guid companyId) //tenemos el parámetro parámetro companyId en nuestra acción y este parámetro será mapeado
                                                                    //desde la ruta principal.Por esa razón, no lo colocamos en el atributo [HttpGet]
        {
            var company = _repository.Company.GetCompany(companyId, trackChanges: false);
            if (company == null)
            {
                _logger.LogInfo($"Company with id: {companyId} doesn't exist in the database.");
            return NotFound();
            }
            var employeesFromDb = _repository.Employee.GetEmployees(companyId,
           trackChanges: false);
            return Ok(employeesFromDb);
        }

    }

}
