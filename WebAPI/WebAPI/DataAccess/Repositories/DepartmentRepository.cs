﻿using System;
using System.Collections;
using System.Linq;
using WebAPI.DataAccess.Persistence;
using WebAPI.Domain.Entities;
using WebAPI.UseCases.Services;

namespace WebAPI.DataAccess.Repositories
{
    public class DepartmentRepository : ICrudRepository<Department>
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext ctx) => _context = ctx;

        public IEnumerable Read()
        {
            return _context.Departments.OrderBy(d => d.Id);
        }

        public Department Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable ReadAll()
        {
            throw new NotImplementedException();
        }

        public Department Create(Department model)
        {
            _context.Departments.Add(model);
            _context.SaveChanges();
            return model;
        }

        public Department Update(Department model)
        {
            _context.Departments.Update(model);
            _context.SaveChanges();
            return model;
        }

        public void Delete(int id)
        {
            var model = _context.Departments.FirstOrDefault(d => d.Id == id);
            _context.Departments.Remove(model ?? throw new InvalidOperationException());
            _context.SaveChanges();
        }

        public string GetFileName(int id)
        {
            throw new NotImplementedException();
        }
    }
}