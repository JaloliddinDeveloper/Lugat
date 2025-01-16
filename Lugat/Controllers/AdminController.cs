//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Lugat.Brokers.Storages;
using Lugat.Models.Foundations.Bolims;
using Lugat.Models.Foundations.Categories;
using Lugat.Models.Foundations.Words;
using Lugat.Models.ViewModels;
using Lugat.Services.Foundations.Bolimlar;
using Lugat.Services.Foundations.Categories;
using Lugat.Services.Foundations.Words;
using Microsoft.AspNetCore.Mvc;

namespace Lugat.Controllers
{
    public class AdminController : Controller
    {
        private readonly IStorageBroker storageBroker;
        private readonly ICategoryService categoryService;
        private readonly IBolimService bolimService;
        private readonly IWordService wordService;
        private readonly string uploadsFolder = "/var/www/pictures";

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


            var viewModel = new BolimPageViewModel
            {
                CategoryId = category.Id,
                CategoryName = category.Name,
                Bolims = bolimlar
            };
            return View(viewModel);
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
        public async ValueTask<IActionResult> CreateBolim(Bolim bolim, IFormFile sectionPicture)
        {
            if (sectionPicture != null && sectionPicture.Length > 0)
            {
                // uploadsFolder bu yerda sinf darajasida aniqlangan
                string uniqueFileName = $"{Guid.NewGuid()}_{sectionPicture.FileName}";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName); // Faylni "/var/www/pictures"ga saqlaymiz

                var directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);  // Agar papka mavjud bo'lmasa, yaratamiz
                }

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await sectionPicture.CopyToAsync(fileStream);  // Faylni serverga yuklash
                }

                bolim.SectionPicture = $"/pictures/{uniqueFileName}";  // Fayl manzilini saqlash
            }

            await this.bolimService.AddBolimAsync(bolim);  // Bolimni qo'shish
            return RedirectToAction("BolimPage", new { id = bolim.CategoryId });
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

            return RedirectToAction("BolimPage", new { id = bolim.CategoryId });
        }


        //22
        public async Task<IActionResult> UpdateBolim(int id)
        {
            var section = await this.bolimService.RetrieveBolimByIdAsync(id);

            if (section == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = section.CategoryId;

            return View(section);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateBolim(Bolim bolim, IFormFile SectionPicture)
        {
            if (bolim == null)
            {
                return BadRequest("Noto'g'ri bo'lim ma'lumotlari.");
            }

            if (SectionPicture != null && SectionPicture.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(SectionPicture.FileName) +
                               "_" + Guid.NewGuid().ToString() + Path.GetExtension(SectionPicture.FileName);

                var filePath = Path.Combine(uploadsFolder, fileName);

                var directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await SectionPicture.CopyToAsync(stream);
                }

                bolim.SectionPicture = $"/pictures/{fileName}";
            }
            else if (string.IsNullOrEmpty(bolim.SectionPicture))
            {
                var existingBolim = await this.bolimService.RetrieveBolimByIdAsync(bolim.Id);
                if (existingBolim != null)
                {
                    bolim.SectionPicture = existingBolim.SectionPicture;
                }
            }

            var existingBolimToUpdate = await this.bolimService.RetrieveBolimByIdAsync(bolim.Id);
            if (existingBolimToUpdate == null)
            {
                return NotFound("Bo'lim topilmadi.");
            }

            existingBolimToUpdate.Name = bolim.Name;
            existingBolimToUpdate.SectionPicture = bolim.SectionPicture;
            existingBolimToUpdate.Star = bolim.Star;
            existingBolimToUpdate.CategoryId = bolim.CategoryId;

            try
            {
                await this.bolimService.UpdateBolimAsync(existingBolimToUpdate);
                TempData["Message"] = "Bo'lim muvaffaqiyatli yangilandi!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Bo'limni yangilashda xato yuz berdi.";
            }

            return RedirectToAction("BolimPage", new { id = bolim.CategoryId });
        }


        //--------------------------------------------------
        // Copyright (c) Coalition Of Good-Hearted Engineers
        // Free To Use To Find Comfort And Peace
        //--------------------------------------------------

        public async Task<IActionResult> WordPage(int id)
        {
            var bolim = await this.bolimService.RetrieveBolimByIdAsync(id);

            if (bolim == null)
            {
                return NotFound();
            }

            var words = await storageBroker.GetWordsBolimlarByIdAsync(id);

            var viewModel = new WordPageViewModel
            {
                BolimId = bolim.Id,
                BolimName = bolim.Name,
                Words = words
            };

            return View(viewModel);
        }

        public async Task<IActionResult> AddWord(int id)
        {
            var bolim = await this.bolimService.RetrieveBolimByIdAsync(id);

            if (bolim == null)
            {
                return NotFound();
            }

            var word = new Word
            {
                BolimId = bolim.Id
            };
            return View(word);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWord(Word word, IFormFile wordPicture)
        {
            if (wordPicture != null && wordPicture.Length > 0)
            {
                var uniqueFileName = $"{Guid.NewGuid()}_{wordPicture.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await wordPicture.CopyToAsync(fileStream);
                }

                word.WordPicture = $"/pictures/{uniqueFileName}";
            }

            await this.wordService.AddWordAsync(word);

            return RedirectToAction("WordPage", new { id = word.BolimId });
        }

        public async Task<IActionResult> UpdateWord(int id)
        {
            var word = await this.wordService.RetrieveWordByIdAsync(id);

            if (word == null)
            {
                return NotFound();
            }

            ViewData["BolimId"] = word.BolimId;

            return View(word);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateWord(Word word, IFormFile WordPicture)
        {
            if (word == null)
            {
                return BadRequest("Noto'g'ri so'z ma'lumotlari.");
            }

            if (WordPicture != null && WordPicture.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(WordPicture.FileName) +
                               "_" + Guid.NewGuid().ToString() + Path.GetExtension(WordPicture.FileName);

                var filePath = Path.Combine(uploadsFolder, fileName);

                var directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await WordPicture.CopyToAsync(stream);
                }

                word.WordPicture = $"/pictures/{fileName}";
            }
            else if (string.IsNullOrEmpty(word.WordPicture))
            {
                var existingWord = await this.wordService.RetrieveWordByIdAsync(word.Id);
                if (existingWord != null)
                {
                    word.WordPicture = existingWord.WordPicture;
                }
            }

            var existingWordToUpdate = await this.wordService.RetrieveWordByIdAsync(word.Id);
            if (existingWordToUpdate == null)
            {
                return NotFound("So'z topilmadi.");
            }

            existingWordToUpdate.English = word.English;
            existingWordToUpdate.EnglishTrans = word.EnglishTrans;
            existingWordToUpdate.Uzbek = word.Uzbek;
            existingWordToUpdate.ExampleEng = word.ExampleEng;
            existingWordToUpdate.ExampleUz = word.ExampleUz;
            existingWordToUpdate.WordPicture = word.WordPicture;
            existingWordToUpdate.BolimId = word.BolimId;

            try
            {
                await this.wordService.UpdateWordAsync(existingWordToUpdate);
                TempData["Message"] = "So'z muvaffaqiyatli yangilandi!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "So'zni yangilashda xato yuz berdi.";
            }

            return RedirectToAction("WordPage", new { id = word.BolimId });
        }


        public async Task<IActionResult> DeleteWord(int id)
        {
            var word = await this.wordService.RetrieveWordByIdAsync(id);
            if (word == null)
            {
                return NotFound();
            }

            return View(word);
        }

        [HttpPost, ActionName("DeleteWord")]
        public async Task<IActionResult> DeleteWordConfirmed(int id)
        {
            var word = await this.wordService.RetrieveWordByIdAsync(id);
            if (word == null)
            {
                return NotFound();
            }

            await this.wordService.RemoveWordByIdAsync(word.Id);
            return RedirectToAction("WordPage", new { id = word.BolimId });
        }
    }
}
