﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BakeryShop.Shared;

namespace BakeryShop.Api.Models
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeById(int employeeId);
        Employee AddEmployee(Employee employee);
        Employee UpdateEmployee(Employee employee);
        void DeleteEmployee(int employeeId);
    }
}
