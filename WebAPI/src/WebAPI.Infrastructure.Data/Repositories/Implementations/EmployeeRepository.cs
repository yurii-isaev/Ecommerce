﻿using System;
using System.Collections;
using System.IO;
using System.Linq;
using WebAPI.Domain.Core.Entities;
using WebAPI.Domain.Core.Interfaces;
using WebAPI.Infrastructure.Data.Helpers;
using WebAPI.Infrastructure.Data.Persistence.Context;

namespace WebAPI.Infrastructure.Data.Repositories.Implementations
{
    public class EmployeeRepository : ICrudRepository<Employee>
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable Read()
        {
            return _context.Employees.OrderBy(x => x.EmployeeId);
        }
        
        public IEnumerable ReadAll()
        {
            return _context.Departments.OrderBy(x => x.DepartmentId);
        }
        
        public Employee Create(Employee model)
        {
            _context.Employees.Add(model);
            _context.SaveChanges();
            return model;
        }
        
        public Employee Update(Employee model)
        {
            _context.Employees.Update(model);
            _context.SaveChanges();
            return model;
        }
        
        public void Delete(int id)
        {
            var model = _context.Employees.FirstOrDefault(x => x.EmployeeId == id);
            _context.Employees.Remove(model ?? throw new InvalidOperationException());
            _context.SaveChanges();
            
            var path = PathTypes.StoragePath + model.PhotoFileName;
            if (File.Exists(path)) File.Delete(path);
        }

        public string GetFileName(int id)
        {
            return _context.Employees.Find(id).PhotoFileName;
        }
    }
}