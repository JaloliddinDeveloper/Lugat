//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Lugat.Brokers.Storages;
using Lugat.Models.Foundations.Bolims;

namespace Lugat.Services.Foundations.Bolimlar
{
    public class BolimService: IBolimService
    {
        private readonly IStorageBroker storageBroker;

        public BolimService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<Bolim> AddBolimAsync(Bolim bolim) =>
            await this.storageBroker.InsertBolimAsync(bolim);

        public async ValueTask<IQueryable<Bolim>> RetrieveAllBolimlarAsync() =>
            await this.storageBroker.SelectAllBolimsAsync();

        public async ValueTask<Bolim> RetrieveBolimByIdAsync(int id) =>
            await this.storageBroker.SelectBolimByIdAsync(id);

        public async ValueTask<Bolim> UpdateBolimAsync(Bolim bolim) =>
            await this.storageBroker.UpdateBolimAsync(bolim);

        public async ValueTask<Bolim> RemoveBolimByIdAsync(int id)
        {
            Bolim maybeBolim = await this.storageBroker.SelectBolimByIdAsync(id);

            return await this.storageBroker.DeleteBolimAsync(maybeBolim);
        }
    }
}
