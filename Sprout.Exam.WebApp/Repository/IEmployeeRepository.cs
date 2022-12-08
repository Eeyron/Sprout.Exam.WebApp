using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Models;

namespace Sprout.Exam.WebApp.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<List<Employee>> GetAllActiveEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int employeeId);
        Task<Employee> CreateEmployeeAsync(CreateEmployeeDto employeeModel);
        Task<Employee> UpdateEmployeeAsync(EditEmployeeDto employeeModel);
        Task<int> DeleteEmployeeAsync(int employeeId);
    }
}
