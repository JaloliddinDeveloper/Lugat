//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Lugat.Models.Foundations.Bolims;

namespace Lugat.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Bolim> InsertBolimAsync(Bolim Bolim);
        ValueTask<IQueryable<Bolim>> SelectAllBolimsAsync();
        ValueTask<Bolim> SelectBolimByIdAsync(int BolimId);
        ValueTask<Bolim> UpdateBolimAsync(Bolim Bolim);
        ValueTask<Bolim> DeleteBolimAsync(Bolim Bolim);

        Task<IEnumerable<Bolim>> GetBolimlarByCategoryIdAsync(int categoryId);
    }
}
