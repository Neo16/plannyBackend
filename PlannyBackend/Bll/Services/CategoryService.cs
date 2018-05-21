﻿using PlannyBackend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlannyBackend.Models;
using PlannyBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace PlannyBackend.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetMainCategories()
        {
            return await _context.Categories
                .Where( e => e.ParentCategoryId == null)
                .ToListAsync();
        }

        public async Task<List<Category>> GetSubCategories()
        {
            return await _context.Categories
                .Where(e => e.ParentCategoryId != null)
                .ToListAsync();
        }
    }
}
