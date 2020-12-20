using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data.Repository.IRepository;

namespace Restaurant.Pages.Admin.Category
{
    public class UpsertModel : PageModel
    {
        private IUnitOfWork _unitOfWork;
        [BindProperty]
        public Models.Category CategoryObject { get; set; }
        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult OnGet(int? id) //?means the id can be null
        {
            //since the javascript routes an ID, and a create new doesnt route a id we can
            //simply test if ID is null it means we are creating. if its not null it means we are editing
            CategoryObject = new Models.Category();
            if (id != null)//gets the respective object from the database via ID
            {
                CategoryObject = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
                if (CategoryObject == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (CategoryObject.Id == 0)//As explained on the view, if the ID is 0 it means its creating so its call the Add to database
            {
                _unitOfWork.Category.Add(CategoryObject);
            }
            else
            {
                _unitOfWork.Category.Update(CategoryObject);//If its not 0 it means its editing because it has its ID. so it calls for the update method
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
