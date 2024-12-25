//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Lugat.Models.Foundations.Bolims;
using Lugat.Models.Foundations.Categories;

namespace Lugat.Models.Orchestrations.CategorySections
{
    public class CategorySection
    {
        public Category Category { get; set; }
        public List<Bolim> Bolimlar { get; set; }   
    }
}
