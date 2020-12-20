using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.DataAccess.Data.Repository.IRepository;
using Restaurant.Models;

namespace Restaurant.DataAccess.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        //Dropdown menu
        public IEnumerable<SelectListItem> GetCategoryListForDropdown()
        {
            return _db.Category.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Category category)
        {
            var objectFromDb = _db.Category.FirstOrDefault(s => s.Id == category.Id);

            objectFromDb.Name = category.Name;
            objectFromDb.DisplayOrder = category.DisplayOrder;

            _db.SaveChanges();
        }
    }
}
