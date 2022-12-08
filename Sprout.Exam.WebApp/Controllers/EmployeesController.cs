using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Repository;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Factory;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _employeeRepository.GetAllActiveEmployeesAsync();
            var result = response.Select(x => EmployeeModelMapper(x));

            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (response == null) return NotFound();

            var result = EmployeeModelMapper(response);

            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            var result = await _employeeRepository.CreateEmployeeAsync(input);

            return Created($"/api/employees/{result.Id}", result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(input.Id);

            if (employee == null) return NotFound();

            var result = await _employeeRepository.UpdateEmployeeAsync(input);

            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee == null) return NotFound();

            var result = await _employeeRepository.DeleteEmployeeAsync(id);

            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(SalaryInput input)
        {
            var result = await _employeeRepository.GetEmployeeByIdAsync(input.Id);
            if (result == null) return NotFound();

            var factory = new SalaryFactory((EmployeeType) result.EmployeeTypeId);
            var salary = factory.CalculateSalary(input);

            if (salary == null) return NotFound();

            return Ok(salary);
        }

        private EmployeeModel EmployeeModelMapper(Employee employee)
        {
            return new EmployeeModel()
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Birthdate = employee.Birthdate.ToString("yyyy-MM-dd"),
                Tin = employee.Tin,
                TypeId = employee.EmployeeTypeId,
                IsDeleted = employee.IsDeleted
            };
        }
    }
}
