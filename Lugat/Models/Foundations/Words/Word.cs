//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
namespace Lugat.Models.Foundations.Words
{
    public class Word
    {
        public int Id { get; set; }
        public string English { get; set; }
        public string EnglishTrans { get; set; }
        public string Uzbek { get; set; }
        public string WordPicture { get; set; }
        public string ExampleEng { get; set; }
        public string ExampleUz { get; set; }
        public int BolimId { get; set; }
    }
}
