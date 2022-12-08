using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Models;

namespace Sprout.Exam.WebApp.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employee.ToListAsync();
        }

        public async Task<List<Employee>> GetAllActiveEmployeesAsync()
        {
            return await _context.Employee.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return await _context.Employee.Where(x => x.Id == employeeId).FirstOrDefaultAsync();
        }

        public async Task<Employee> CreateEmployeeAsync(CreateEmployeeDto employeeModel)
        {
            var employee = new Employee()
            {
                FullName = employeeModel.FullName,
                Birthdate = employeeModel.Birthdate,
                Tin = employeeModel.Tin,
                EmployeeTypeId = employeeModel.TypeId
            };

            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee> UpdateEmployeeAsync(EditEmployeeDto employeeModel)
        {
            _context.ChangeTracker.Clear();

            var employee = new Employee() { 
                Id = employeeModel.Id,
                FullName = employeeModel.FullName,
                Birthdate = employeeModel.Birthdate,
                Tin = employeeModel.Tin,
                EmployeeTypeId = employeeModel.TypeId
            };

            _context.Employee.Update(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<int> DeleteEmployeeAsync(int employeeId)
        {
            _context.ChangeTracker.Clear();

            var employee = new Employee() { Id = employeeId, IsDeleted = true };

            _context.Employee.Attach(employee).Property(x => x.IsDeleted).IsModified = true;
            await _context.SaveChangesAsync();

            return employeeId;
        }
    }
}
