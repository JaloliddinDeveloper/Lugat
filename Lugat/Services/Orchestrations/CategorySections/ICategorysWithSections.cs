//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Lugat.Models.Orchestrations.CategorySections;

namespace Lugat.Services.Orchestrations.CategorySections
{
    public interface ICategorysWithSections
    {
        ValueTask<IQueryable<CategorySection>> RetrieveAllCategoryWithSectionAsync();
    }
}
