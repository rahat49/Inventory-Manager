using dotnetsorting.Tools;
using Inventory_Manager.Models;
using InventoryManger.Data;
using InventoryManger.Interfaces;
using InventoryManger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace InventoryManger.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _henv;

        private readonly IProduct _Repo;
        private readonly IUnit _unitRepo;

        private readonly ICategory _catRepo;
        private readonly IBrand _brRepo;
        private readonly IProductGroup _pgRepo;
        private readonly IProductProfile _ppRepo;

        public ProductController(IProduct Repo, IUnit unitRepo, ICategory catRepo, IBrand brRepo,IProductGroup pgRepo, IProductProfile ppRepo, IWebHostEnvironment henv) //here the repository will be pass be passed by the dependancy injection
        {
            _Repo = Repo;
            _unitRepo = unitRepo;
            _catRepo = catRepo;
            _brRepo = brRepo;
            _pgRepo = pgRepo;
            _ppRepo = ppRepo;
            _henv = henv;
        }

        public IActionResult Index(string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 5)
        {
            SortModel sortModel = new SortModel();
            sortModel.AddColumn("Code");
            sortModel.AddColumn("name");
            sortModel.AddColumn("description");
            sortModel.AddColumn("Cost");
            sortModel.AddColumn("Price");
            sortModel.AddColumn("Unit");
            sortModel.ApplySort(sortExpression);
            ViewData["sortModel"] = sortModel;
            ViewBag.SearchText = SearchText;

            PaginatedList<Product> items = _Repo.GetItems(sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

            //int totRecs=((PaginatedList<Unit>)units).TotalRecord;

            var pager = new PagerModel(items.TotalRecord, pg, pageSize);
            pager.SortExpression = sortExpression;
            this.ViewBag.Pager = pager;

            TempData["CurrentPage"] = pg;
            return View(items);
        }

        public IActionResult Create()
        {
            Product items = new Product();
            ViewBag.Units = GetUnits();
            ViewBag.Brands = GetBrands();
            ViewBag.Category = GetCategories();
            ViewBag.ProductProfile = GetProductProfile();
            ViewBag.ProductGroup = GetProductGroup();
            TempData.Keep();
            return View(items);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product items)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (items.Description.Length < 4 || items.Description == null)
                    errMessage = "Description Must be atleast 4 Characters";

                if (_Repo.IsItemCodeExist(items.Code) == true)
                    errMessage = errMessage + " " + items.Code + "Exists Already";

                if (_Repo.IsItemExist(items.Name) == true)
                    errMessage = errMessage + " " + items.Name + " " + "Exists Already";

                if (errMessage == "")
                {
                    string uniqufilename = GetUploadFileName(items);
                    items.PhotoUrl = uniqufilename;

                    items = _Repo.Create(items);
                    bolret = true;
                }
            }
            catch (Exception ex)
            {
                errMessage = errMessage + " " + ex.Message;
            }
            if (bolret == false)
            {
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(items);
            }
            else
            {

            }
            TempData["SuccessMessage"] = items.Name + " " + "Created Successfully";
            return RedirectToAction(nameof(Index));
        }
        //edit 
        public IActionResult Edit(string id)
        {
            Product items = _Repo.GetItem(id);
            ViewBag.Units = GetUnits();
            ViewBag.Brands = GetBrands();
            ViewBag.Category = GetCategories();
            ViewBag.ProductProfile = GetProductProfile();
            ViewBag.ProductGroup = GetProductGroup();
            TempData.Keep();
            return View(items);
        }
        [HttpPost]
        public IActionResult Edit(Product items)
        {
            bool bolret = false;
            string errMessage = "";
            try
            {
                if (items.Description.Length < 4 || items.Description == null)

                    errMessage = "Description Must be atleast 4 Character";

                if (_Repo.IsItemCodeExist(items.Code) == true)
                    errMessage = errMessage + " " + items.Code + "Exists Already";


                if (_Repo.IsItemExist(items.Name) == true)
                    errMessage = errMessage + " " + items.Name + "Exists Already";

                if(items.ProductPhote!=null)
                {
                    //photo save
                    string uniqufilename = GetUploadFileName(items);
                    items.PhotoUrl = uniqufilename;
                }

                if (errMessage == "")
                {

                    items = _Repo.Edit(items);
                    TempData["SuccessMessage"] = items.Name + "Updated Successfully";
                    bolret = true;
                }

            }
            catch (Exception ex)
            {
                errMessage = ex.Message;
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];

            if (bolret == false)
            {
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);

                PopulateViewbags();

                return View(items);
                
            }
            else

                TempData["SuccessMessage"] = items.Name + " " + "Updated Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });
        }
        public IActionResult Details(string id)
        {

            Product items = _Repo.GetItem(id);
            ViewBag.Units = GetUnits();
            ViewBag.Brands = GetBrands();
            ViewBag.Category = GetCategories();
            ViewBag.ProductProfile = GetProductProfile();
            ViewBag.ProductGroup = GetProductGroup();
            return View(items);
        }
        //delete
        public IActionResult Delete(string id)
        {
                Product items = _Repo.GetItem(id);
                TempData.Keep();
                return View(items);
        }
        [HttpPost]
        public IActionResult Delete(Product items)
        {
            try
            {
                items = _Repo.Delete(items);
            }
            catch (Exception ex)
            {
                string errMessage = ex.Message;
                if(ex.InnerException!=null)
                    errMessage= ex.InnerException.Message;
                TempData["ErrorMessage"] = errMessage;
                ModelState.AddModelError("", errMessage);
                return View(items);
            }
            int currentPage = 1;
            if (TempData["CurrentPage"] != null)
                currentPage = (int)TempData["CurrentPage"];
            TempData["SuccessMessage"] = items.Name + " " + "Deleted Successfully";
            return RedirectToAction(nameof(Index), new { pg = currentPage });


        }
        private void PopulateViewbags()
        {
             ViewBag.Category = GetCategories();
             ViewBag.Units = GetUnits();
             ViewBag.Brands = GetBrands();
             ViewBag.ProductGroup = GetProductGroup();
             ViewBag.ProductProfile = GetProductProfile();
            
            
        }
        private List<SelectListItem>GetUnits()
        {
            var units = new List<SelectListItem>();
            PaginatedList<Unit> units1 = _unitRepo.GetItems("Name", SortOrder.Ascending,"",1,1000);
            units = units1.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name

            }).ToList();
            var defitem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Unit----"
            };
            units.Insert(0, defitem);
            return units;
          
        }
        private List<SelectListItem> GetBrands()
        {
            var lstItem = new List<SelectListItem>();
            PaginatedList<Brand> lstItem1 = _brRepo.GetItems("Name", SortOrder.Ascending, "", 1, 1000);
            lstItem = lstItem1.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name

            }).ToList();
            var defitem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Brand----"
            };
            lstItem.Insert(0, defitem);
            return lstItem;

        }
        private List<SelectListItem> GetCategories()
        {
            var lstItem = new List<SelectListItem>();
            PaginatedList<Category> lstItem1 = _catRepo.GetItems("Name", SortOrder.Ascending, "", 1, 1000);
            lstItem = lstItem1.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name

            }).ToList();
            var defitem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Category----"
            };
            lstItem.Insert(0, defitem);
            return lstItem;

        }
        private List<SelectListItem> GetProductProfile()
        {
            var lstItem = new List<SelectListItem>();
            PaginatedList<ProductProfile> lstItem1 = _ppRepo.GetItems("Name", SortOrder.Ascending, "", 1, 1000);
            lstItem = lstItem1.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name

            }).ToList();
            var defitem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Product Profile----"
            };
            lstItem.Insert(0, defitem);
            return lstItem;

        }
        private List<SelectListItem> GetProductGroup()
        {
            var lstItem = new List<SelectListItem>();
            PaginatedList<ProductGroup> lstItem1 = _pgRepo.GetItems("Name", SortOrder.Ascending, "", 1, 1000);
            lstItem = lstItem1.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.Name

            }).ToList();
            var defitem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Product Group----"
            };
            lstItem.Insert(0, defitem);
            return lstItem;

        }

        private string GetUploadFileName(Product product)
        {
            string uniquefilename = null;

            if(product.ProductPhote!=null)
            {
                string uploadFolder = Path.Combine(_henv.WebRootPath, "Images");
                uniquefilename = Guid.NewGuid().ToString() + "_" + product.ProductPhote.FileName;
                string filePath= Path.Combine(uploadFolder,uniquefilename);
                using (var fileStrem =new FileStream(filePath, FileMode.Create))
                {
                    product.ProductPhote.CopyToAsync(fileStrem);
                }

            }
            return uniquefilename;
        }

    }
}
