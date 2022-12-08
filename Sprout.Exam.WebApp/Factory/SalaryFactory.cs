using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sprout.Exam.Common.Enums;

namespace Sprout.Exam.WebApp.Factory
{
    public class SalaryFactory
    {
        private readonly EmployeeType _typeId;
        public SalaryFactory(EmployeeType typeId)
        {
            _typeId = typeId;
        }

        public decimal? CalculateSalary(SalaryInput input)
        {
            return _typeId switch
            {
                EmployeeType.Regular =>
                    CalculateRegular(input.AbsentDays),
                EmployeeType.Contractual =>
                    CalculateContractual(input.WorkedDays),
                _ => null,
            };
        }
        private decimal CalculateRegular(decimal absentDays)
        {
            decimal result = 20000 - (absentDays * ((decimal) 20000 / 22)) - (20000 * (decimal) 0.12);
            return Math.Round(result, 2);
        }

        private decimal CalculateContractual(decimal workedDays)
        {
            decimal result = 500 * workedDays;
            return Math.Round(result, 2);
        }
    }

    public class SalaryInput
    {
        public int Id { get; set; }
        public decimal AbsentDays { get; set; }
        public decimal WorkedDays { get; set; }
    }
}
