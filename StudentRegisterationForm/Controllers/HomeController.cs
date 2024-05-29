using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Configuration;
using StudentRegisterationForm.Models;
using System.Diagnostics;
using System.Drawing.Printing;

namespace StudentRegisterationForm.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentDBContext dBContext;

        public HomeController(StudentDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public IActionResult Index()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(StudentModel s)
        {

            if (ModelState.IsValid)
            {
                await dBContext.AddAsync(s);
                TempData["inserted"] = "Inserted";
                await dBContext.SaveChangesAsync();

                return RedirectToAction("Details");
            }


            return View(s);

        }

       
        public async Task<IActionResult> SuccessView(int? id, StudentModel s)
        {
            var stdData = await dBContext.StudentsDataTab.FirstOrDefaultAsync(x => x.Id == id);

            if (stdData != null)
            {
                return View(stdData);
            }
            else
            {
                return View(s);   
            }


        }

        public async Task<IActionResult> Edit(int? Id)
        {
            var edit = await dBContext.StudentsDataTab.FirstOrDefaultAsync(x => x.Id == Id);
            if (edit == null)
            {
                return NotFound();
            }
            else
            {
               
                return View(edit);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StudentModel a)
        {

            if (ModelState.IsValid)
            {
                dBContext.StudentsDataTab.Update(a);
                await dBContext.SaveChangesAsync();
                TempData["edit"] = "Edited";
                return RedirectToAction("Details", "Home");
            }
            else
            {

                return View(a);
            }

        }

        public async Task<IActionResult> Delete(int? id)
        {
            var confirm_data = await dBContext.StudentsDataTab.FirstOrDefaultAsync(x => x.Id == id);
            if (confirm_data == null)
            {
                return NotFound();
            }
            else
            {
                return View(confirm_data);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm_Delete(int? id)
        {

            var del_data = await dBContext.StudentsDataTab.FirstOrDefaultAsync(x => x.Id == id);
            if (del_data == null) { return NotFound(); }
            else
            {

                dBContext.StudentsDataTab.Remove(del_data);
                dBContext.SaveChanges();
                TempData["deleted"] = "Deleted";
                return RedirectToAction("Details");

            }
        }


        [ActionName("Details")]
        public async Task<IActionResult> Details()
        {
            var std_data = await dBContext.StudentsDataTab.ToListAsync();
            return View(std_data);
        }
    
        

        private FetchingModel FetchLogic() 
            {
            TempData.Keep();
            //created a model for dropdown list and created an instance of that model here to access it 
            FetchingModel ft = new FetchingModel();

            //accesssing List Property of that model and for appending/populating data created a instance
            ft.Studs = new List<SelectListItem>();

            //data source converted into list
            var data = dBContext.StudentsDataTab.ToList();

            //checking if data is not empty and creating default view with empty value
            if (data != null)
            {
                ft.Studs.Add(new SelectListItem
                {
                    Value = "",
                    Text = "Select Name"
                });
            }

            //for SelectListItem Value and Text are property that sets the value in <option>
            foreach (var item in data)
            {
                if (data != null)
                {
                    ft.Studs.Add(new SelectListItem
                    {
                        Value = item.Id.ToString() ,
                        Text = item.Name
                    });
                }
            }

            //passing the object to view 
            return ft;

        }

        public IActionResult fetch()
        {
           FetchingModel f = FetchLogic();
          
            return View(f);
            
        }

        [HttpPost]
        public async Task<IActionResult> fetch(FetchingModel f)
        {
           FetchingModel m= FetchLogic();
            var stdData = await dBContext.StudentsDataTab.FirstOrDefaultAsync(x => x.Id == f.ID);

            if (stdData != null)
            {
                TempData["dataName"] = stdData.Name;
                TempData["dataEmail"] = stdData.Email;
                TempData["dataAge"] = stdData.Age;
                TempData["dataPhone"] = stdData.Phone_Number;
            }
            else
            {
                ViewBag.Message = "Data Not Found";
            }
            return View(m);
        }

    

   

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
