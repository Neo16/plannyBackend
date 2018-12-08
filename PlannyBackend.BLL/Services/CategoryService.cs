using PlannyBackend.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlannyBackend.Model;
using Microsoft.EntityFrameworkCore;
using PlannyBackend.DAL;

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

        public async Task<List<MainCategory>> GetMainCategories()
        {
            return await _context.MainCategories              
                .ToListAsync();
        }

        public async Task<List<Category>> GetSubCategories()
        {
            return await _context.Categories             
                .ToListAsync();
        }
    }
}
