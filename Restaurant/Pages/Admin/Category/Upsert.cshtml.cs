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
    }
}
