using JWTAUthentication.Interface;
using JWTAUthentication.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTAUthentication.Repository;


public class EmployeeRepository: IEmployees
{

    // readonly DatabaseContext _databaseContext=new();
    readonly DatabaseContext _databaseContext;

    public EmployeeRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public List<Employee> GetEmployeeDetails()
    {
        try
        {
            return _databaseContext.Employees.ToList();
        }
        catch
        {
            throw;
        }
    }

    public Employee GetEmployeeDetails(int id)
    {
        try
        {
            Employee? employee = _databaseContext.Employees.Find(id);
            if (employee != null)
            {
                return employee;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }
        catch
        {
            throw;
        }
    }

        public void AddEmployee(Employee employee)
        {
            try
            {
                _databaseContext.Employees.Add(employee);
                _databaseContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                _databaseContext.Entry(employee).State = EntityState.Modified;
                _databaseContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Employee DeleteEmployee(int id)
        {
            try
            {
                Employee? employee = _databaseContext.Employees.Find(id);

                if (employee != null)
                {
                    _databaseContext.Employees.Remove(employee);
                    _databaseContext.SaveChanges();
                    return employee;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool CheckEmployee(int id)
        {
            return _databaseContext.Employees.Any(e => e.EmployeeID == id);
        }
    }

    




