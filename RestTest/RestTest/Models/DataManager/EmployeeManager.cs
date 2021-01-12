using System;
using System.Linq;
using System.Collections.Generic;
using RestTest.Repository;
using RestTest.Models;



namespace RestTest.Models.DataManager
{
    public class EmployeeManager : IDataRepository<Employee>
    {
        readonly EmployeeContext _employeeContext;

        public EmployeeManager(EmployeeContext context)
        {
            _employeeContext = context;
        }

        public void Add(Employee entity)
        {
            _employeeContext.Employees.Add(entity);
            _employeeContext.SaveChanges();
        }

        public void Delete(Employee employee)
        {
            _employeeContext.Employees.Remove(employee);
            _employeeContext.SaveChanges();
        }

        public Employee Get(long id) {
           return _employeeContext.Employees
                .FirstOrDefault(e => e.ID == id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeContext.Employees.ToList();
        }

        public void Update(Employee employee, Employee entity)
        {
            
            employee.Name = entity.Name;
            employee.Address = entity.Address;
            employee.PhoneNumber = entity.PhoneNumber;
           

            _employeeContext.SaveChanges();
        }
    }
}
