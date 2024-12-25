//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Lugat.Models.Foundations.Categories;

namespace Lugat.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Category> InsertCategoryAsync(Category Category);
        ValueTask<IQueryable<Category>> SelectAllCategorysAsync();
        ValueTask<Category> SelectCategoryByIdAsync(int CategoryId);
        ValueTask<Category> UpdateCategoryAsync(Category Category);
        ValueTask<Category> DeleteCategoryAsync(Category Category);
    }
}
