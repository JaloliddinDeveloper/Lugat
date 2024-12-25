//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------

using Lugat.Models.Foundations.Words;

namespace Lugat.Services.Foundations.Words
{
    public interface IWordService
    {
        ValueTask<Word> AddWordAsync(Word Word);
        ValueTask<IQueryable<Word>> RetrieveAllWordsAsync();
        ValueTask<Word> RetrieveWordByIdAsync(int id);
        ValueTask<Word> UpdateWordAsync(Word Word);
        ValueTask<Word> RemoveWordByIdAsync(int id);
    }
}
