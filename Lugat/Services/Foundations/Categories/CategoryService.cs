//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Lugat.Brokers.Storages;
using Lugat.Models.Foundations.Categories;

namespace Lugat.Services.Foundations.Categories
{
    public class CategoryService: ICategoryService
    {
        private readonly IStorageBroker storageBroker;

        public CategoryService(IStorageBroker storageBroker)=>
            this.storageBroker = storageBroker;

        public async ValueTask<Category> AddCategoryAsync(Category category)=>
            await this.storageBroker.InsertCategoryAsync(category);
       
        public async ValueTask<IQueryable<Category>> RetrieveAllCategoriesAsync()=>
        await this.storageBroker.SelectAllCategorysAsync();

        public async ValueTask<Category> RetrieveCategoryByIdAsync(int id)=>
            await this.storageBroker.SelectCategoryByIdAsync(id);
      
        public async ValueTask<Category> UpdateCategoryAsync(Category category)=>
            await this.storageBroker.UpdateCategoryAsync(category);

        public async ValueTask<Category> RemoveCategoryByIdAsync(int  id)
        {
            Category maybeCategory=await this.storageBroker.SelectCategoryByIdAsync(id);

            return await this.storageBroker.DeleteCategoryAsync(maybeCategory);
        }
    }
}
