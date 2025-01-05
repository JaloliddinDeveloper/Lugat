//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Lugat.Models.Foundations.Bolims;

namespace Lugat.Models.ViewModels
{
    public class BolimPageViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<Bolim> Bolims { get; set; }
    }
}
