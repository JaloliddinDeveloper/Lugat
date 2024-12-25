//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Lugat.Models.Orchestrations.BolimlarWordss;
using Lugat.Services.Foundations.Bolimlar;
using Lugat.Services.Foundations.Words;

namespace Lugat.Services.Orchestrations.BolimsWords
{
    public class RetrieveBolimWithWords : IRetrieveBolimWithWords
    {
        private readonly IBolimService bolimService;
        private readonly IWordService wordService;

        public RetrieveBolimWithWords(
            IBolimService bolimService,
            IWordService wordService)
        {
            this.bolimService = bolimService;
            this.wordService = wordService;
        }

        public async ValueTask<BolimlarWords> RetrieveBolimlarWordsByIdAsync(int bolimId)
        {
            var bolim = (await this.bolimService.RetrieveAllBolimlarAsync())
                .FirstOrDefault(p => p.Id == bolimId);

            if (bolim == null)
            {
                return null;
            }

            var words = (await this.wordService.RetrieveAllWordsAsync())
                .Where(t => t.BolimId == bolim.Id).ToList();

            return new BolimlarWords
            {
                Bolim = bolim,
                Words = words
            };
        }
    }
}
