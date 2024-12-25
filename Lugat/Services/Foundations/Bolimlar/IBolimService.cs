//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using Lugat.Models.Foundations.Bolims;

namespace Lugat.Services.Foundations.Bolimlar
{
    public interface IBolimService
    {
        ValueTask<Bolim> AddBolimAsync(Bolim bolim);
        ValueTask<IQueryable<Bolim>> RetrieveAllBolimlarAsync();
        ValueTask<Bolim> RetrieveBolimByIdAsync(int id);
        ValueTask<Bolim> UpdateBolimAsync(Bolim bolim);
        ValueTask<Bolim> RemoveBolimByIdAsync(int id);
    }
}
