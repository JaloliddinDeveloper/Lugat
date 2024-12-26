using Lugat.Brokers.Storages;
using Lugat.Models.Foundations.Bolims;
using Lugat.Models.Foundations.Categories;
using Lugat.Services.Foundations.Bolimlar;
using Lugat.Services.Foundations.Categories;
using Lugat.Services.Foundations.Words;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lugat.Controllers
{
    public  class AdminController : Controller
    {
        private readonly IStorageBroker storageBroker;
        private readonly ICategoryService categoryService;
        private readonly IBolimService bolimService;
        private readonly IWordService wordService;

        public AdminController(
            IStorageBroker storageBroker,
            ICategoryService categoryService,
            IBolimService bolimService,
            IWordService wordService)
        {
            this.storageBroker = storageBroker;
            this.categoryService = categoryService;
            this.bolimService = bolimService;
            this.wordService = wordService;
        }

        public async ValueTask<IActionResult> Index()
        {
            var cates = await this.storageBroker.SelectAllCategorysAsync();
            return View(cates);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View(new Category());
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            var newCategory = new Category
            {
                Name = category.Name,
                Star = category.Star
            };

            await this.storageBroker.InsertCategoryAsync(newCategory);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await this.categoryService.RetrieveCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.categoryService.RemoveCategoryByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> UpdateCate(int id)
        {
            var category = await this.categoryService.RetrieveCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateCate(Category category)
        {
            if (ModelState.IsValid)
            {
                await this.categoryService.UpdateCategoryAsync(category);
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }



        //Bolimlar

        public async Task<IActionResult> BolimPage(int id)
        {
            var category = await this.categoryService.RetrieveCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var bolimlar = await storageBroker.GetBolimlarByCategoryIdAsync(id);


            ViewBag.CategoryName = category.Name;
            return View(bolimlar);
        }

        public async ValueTask<IActionResult> AddBolim(int id)
        {
          
            var category = await this.categoryService.RetrieveCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound(); 
            }

           
            var bolim = new Bolim
            {
                CategoryId = category.Id
            };

            return View(bolim); 
        }


        [HttpPost]
        public async ValueTask<IActionResult> CreateBolim(Bolim bolim)
        {
            if (ModelState.IsValid)
            {
              await this.bolimService.AddBolimAsync(bolim);

                return RedirectToAction("Index", new { id = bolim.CategoryId });
            }

            return View("AddBolim", bolim);
        }

        public async Task<IActionResult> DeleteBolim(int id)
        {
            var bolim = await this.bolimService.RetrieveBolimByIdAsync(id);

            if (bolim == null)
            {
                return NotFound();
            }

            return View(bolim); 
        }

        [HttpPost, ActionName("DeleteBolim")]
        public async Task<IActionResult> DeleteConfirmed2(int id)
        {
            var bolim = await this.bolimService.RetrieveBolimByIdAsync(id);

            if (bolim == null)
            {
                return NotFound(); 
            }

            await this.bolimService.RemoveBolimByIdAsync(id); 

            return RedirectToAction(nameof(Index)); 
        }

        public async Task<IActionResult> UpdateBolim(int id)
        {
            var bolim = await this.bolimService.RetrieveBolimByIdAsync(id);

            if (bolim == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = bolim.CategoryId;

            return View(bolim);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBolim(Bolim bolim)
        {
            if (!ModelState.IsValid)
            {
                return View(bolim);
            }

            try
            {
                var existingBolim = await this.bolimService.RetrieveBolimByIdAsync(bolim.Id);
                if (existingBolim == null)
                {
                    return NotFound();
                }

                existingBolim.Name = bolim.Name;
                existingBolim.SectionPicture = bolim.SectionPicture;
                existingBolim.Star = bolim.Star;
                existingBolim.CategoryId = bolim.CategoryId;

                await this.bolimService.UpdateBolimAsync(existingBolim);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, $"Xatolik yuz berdi: {ex.Message}");
                return View(bolim);
            }
        }
    }
}
