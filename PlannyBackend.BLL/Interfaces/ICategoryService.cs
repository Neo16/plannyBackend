using PlannyBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlannyBackend.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task<List<MainCategory>> GetMainCategories();
        Task<List<Category>> GetSubCategories();
    }
}
