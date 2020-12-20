using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.DataAccess.Data.Repository.IRepository;
using Restaurant.Models;

namespace Restaurant.DataAccess.Data.Repository
{
    public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
    {
        private ApplicationDbContext _db;

        public FoodTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetFoodTypeListForDropdown()
        {
            return _db.FoodType.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(FoodType foodType)
        {
            var objectFromDb = _db.FoodType.FirstOrDefault(s => s.Id == foodType.Id);
            objectFromDb.Name = foodType.Name;
            _db.SaveChanges();
        }
    }
}
