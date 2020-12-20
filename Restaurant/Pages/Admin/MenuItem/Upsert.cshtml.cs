using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data.Repository.IRepository;
using Restaurant.Models.ViewModels;

namespace Restaurant.Pages.Admin.MenuItem
{
    public class UpsertModel : PageModel
    {
        private IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _webHostEnvironment;
        [BindProperty]
        public MenuItemVM MenuItemObj { get; set; }
        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult OnGet(int? id) //?means the id can be null
        {
            //since the javascript routes an ID, and a create new doesnt route a id we can
            //simply test if ID is null it means we are creating. if its not null it means we are editing

            MenuItemObj = new MenuItemVM//Populate the viewmodel with the several existing properties
            {
                CategoryList = _unitOfWork.Category.GetCategoryListForDropdown(),
                FoodTypeList = _unitOfWork.FoodType.GetFoodTypeListForDropdown(),
                MenuItem = new Models.MenuItem() //initialize to prevent null
            };
            if (id != null)//gets the respective object from the database via ID
            {
                MenuItemObj.MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id);
                if (MenuItemObj == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            string webRootPath = _webHostEnvironment.WebRootPath; //Get the root path for www
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (MenuItemObj.MenuItem.Id == 0)//As explained on the view, if the ID is 0 it means its creating so its call the Add to database
            {
                string fileName = Guid.NewGuid().ToString(); //convert filename to guid
                var uploads = Path.Combine(webRootPath, @"images\menuItems");
                var extension = Path.GetExtension(files[0].FileName);
                //mega complicated way to upload the pictures to the server
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                MenuItemObj.MenuItem.Image = @"\images\menuItems\" + fileName + extension;
                _unitOfWork.MenuItem.Add(MenuItemObj.MenuItem);
            }
            else
            {
                //Edit an existing item
                //mega complicated way to upload the pictures to the server
                //ALso a mega super complicated way to edit existing pictures
                var objFromDb = _unitOfWork.MenuItem.Get(MenuItemObj.MenuItem.Id);
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString(); //convert filename to guid
                    var uploads = Path.Combine(webRootPath, @"images\menuItems");
                    var extension = Path.GetExtension(files[0].FileName);


                    var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    MenuItemObj.MenuItem.Image = @"\images\menuItems\" + fileName + extension;
                    

                }
                else//In case the picture wasnt changed
                {
                    MenuItemObj.MenuItem.Image = objFromDb.Image;
                }

                _unitOfWork.MenuItem.Update(MenuItemObj.MenuItem);//If its not 0 it means its editing because it has its ID. so it calls for the update method
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
