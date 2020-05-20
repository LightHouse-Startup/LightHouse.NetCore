using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Restful.Api.Entities;

namespace Restful.Api.Services
{
    public interface ICompanyService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId);

        Task<Employee> GetEmployeesAsync(Guid companyId, Guid employeeId);

        void AddEmployee(Guid companyId, Employee employee);

        void UpdateEmployee(Employee employee);

        void DeleteEmployee(Employee employee);

        Task<IEnumerable<Company>> GetCompaniesAsync();

        Task<Company> GetCompanyAsync(Guid companyId);

        Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds);

        void AddCompany(Company company);

        void DeleteCompany(Company company);

        void UpdateCompany(Company company);

        Task<bool> CompanyExistsAsync(Guid companyId);

        Task<bool> SaveAsync();
    }
}