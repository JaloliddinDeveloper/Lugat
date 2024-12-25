//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Lugat.Models.Foundations.Categories;

namespace Lugat.Services.Foundations.Categories
{
    public interface ICategoryService
    {
        ValueTask<Category> AddCategoryAsync(Category category);    
        ValueTask<IQueryable<Category>> RetrieveAllCategoriesAsync();
        ValueTask<Category> RetrieveCategoryByIdAsync(int id);  
        ValueTask<Category> UpdateCategoryAsync(Category category);
        ValueTask<Category> RemoveCategoryByIdAsync(int id);
    }
}
