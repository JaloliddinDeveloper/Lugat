using Lugat.Models.Foundations.Bolims;
using Lugat.Models.Foundations.Words;

namespace Lugat.Models.ViewModels
{
    public class WordPageViewModel
    {
        public int BolimId { get; set; }
        public string BolimName { get; set; }
        public IEnumerable<Word> Words { get; set; }
    }
}
