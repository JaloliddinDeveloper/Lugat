//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Lugat.Models.Orchestrations.CategorySections;
using Lugat.Services.Foundations.Bolimlar;
using Lugat.Services.Foundations.Categories;

namespace Lugat.Services.Orchestrations.CategorySections
{
    public class CategorysWithSections : ICategorysWithSections
    {
        private readonly ICategoryService categoryService;
        private readonly IBolimService bolimService;

        public CategorysWithSections(
            ICategoryService categoryService,
            IBolimService bolimService)
        {
            this.categoryService = categoryService;
            this.bolimService = bolimService;
        }

        public async ValueTask<IQueryable<CategorySection>> RetrieveAllCategoryWithSectionAsync()
        {
            var categorlar = (await this.categoryService.RetrieveAllCategoriesAsync()).ToList();
            var bolimlar = (await this.bolimService.RetrieveAllBolimlarAsync()).ToList();

            var catbolimlar = categorlar.Select(group => new CategorySection
            {
                Category = group,
                Bolimlar = bolimlar.Where(student => student.CategoryId == group.Id).ToList()
            });

            return catbolimlar.AsQueryable();
        }
    }
}
