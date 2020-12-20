using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Restaurant.DataAccess.Data.Repository.IRepository;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new {data = _unitOfWork.Category.GetAll()});
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            //retrieve object from database
            var objectFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (objectFromDb == null) //if the object is null, say there is a error in deletion
            {
                return Json((new {success = false, message = "Error while deleting"}));

            }
            _unitOfWork.Category.Remove(objectFromDb); //remove it from Db
            _unitOfWork.Save(); //save the changes on the DB
            return Json(new {success = true, message = "Delete Successful"}); //ANd then a simple message alerting the deletion was successful
        }
        
    }
}
