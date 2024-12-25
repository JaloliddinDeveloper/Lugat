//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Lugat.Models.Foundations.Bolims;

namespace Lugat.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Bolim> Bolimlar { get; set; }

        public async ValueTask<Bolim> InsertBolimAsync(Bolim Bolim) =>
       await InsertAsync(Bolim);

        public async ValueTask<IQueryable<Bolim>> SelectAllBolimsAsync() =>
           await SelectAllAsync<Bolim>();

        public async ValueTask<Bolim> SelectBolimByIdAsync(int BolimId) =>
           await SelectAsync<Bolim>(BolimId);

        public async ValueTask<Bolim> UpdateBolimAsync(Bolim Bolim) =>
           await UpdateAsync(Bolim);

        public async ValueTask<Bolim> DeleteBolimAsync(Bolim Bolim) =>
          await DeleteAsync(Bolim);

        public async Task<IEnumerable<Bolim>> GetBolimlarByCategoryIdAsync(int categoryId)
        {
            return await this.Bolimlar
                .Where(b => b.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}
