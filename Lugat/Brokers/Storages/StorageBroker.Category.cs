//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Lugat.Models.Foundations.Categories;

namespace Lugat.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Category> Categories { get; set; }

        public async ValueTask<Category> InsertCategoryAsync(Category Category) =>
            await InsertAsync(Category);

        public async ValueTask<IQueryable<Category>> SelectAllCategorysAsync() =>
            await SelectAllAsync<Category>();

        public async ValueTask<Category> SelectCategoryByIdAsync(int CategoryId) =>
           await SelectAsync<Category>(CategoryId);

        public async ValueTask<Category> UpdateCategoryAsync(Category Category) =>
           await UpdateAsync(Category);

        public async ValueTask<Category> DeleteCategoryAsync(Category Category) =>
          await DeleteAsync(Category);
    }
}
