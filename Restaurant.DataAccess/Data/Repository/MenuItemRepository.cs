using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Restaurant.DataAccess.Data.Repository.IRepository;
using Restaurant.Models;

namespace Restaurant.DataAccess.Data.Repository
{
   public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
   {
       private ApplicationDbContext _db;
        public MenuItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MenuItem menuItem)
        {
            var menuItemFromDb = _db.MenuItem.FirstOrDefault(m => m.Id == menuItem.Id);

            menuItemFromDb.Name = menuItem.Name;
            menuItemFromDb.CategoryId = menuItem.CategoryId;
            menuItemFromDb.Description = menuItem.Description;
            menuItemFromDb.FoodTypeId = menuItem.FoodTypeId;
            menuItemFromDb.Price = menuItem.Price;
            if (menuItem.Image != null) //Update the image, only if they uploaded something, if not stays how it was
            {
                menuItemFromDb.Image = menuItem.Image;
            }

            _db.SaveChanges();//Save changes

        }
    }
}
