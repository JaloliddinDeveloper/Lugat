//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Lugat.Models.Orchestrations.BolimlarWordss;

namespace Lugat.Services.Orchestrations.BolimsWords
{
    public interface IRetrieveBolimWithWords
    {
        ValueTask<BolimlarWords> RetrieveBolimlarWordsByIdAsync(int bolimId);
    }
}
