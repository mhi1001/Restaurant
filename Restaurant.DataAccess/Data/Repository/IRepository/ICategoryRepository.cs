using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Models;

namespace Restaurant.DataAccess.Data.Repository.IRepository
{
   public interface ICategoryRepository : IRepository<Category>
   {
       IEnumerable<SelectListItem> GetCategoryListForDropdown();

       void Update(Category category);
   }
}
