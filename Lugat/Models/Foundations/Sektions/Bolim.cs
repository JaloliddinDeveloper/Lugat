//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Lugat.Models.Foundations.Words;

namespace Lugat.Models.Foundations.Bolims
{
    public class Bolim
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SectionPicture { get; set; }
        public int Star { get; set; }
        public int CategoryId { get; set; }
        public List<Word>? Words { get; set; }
    }
}
