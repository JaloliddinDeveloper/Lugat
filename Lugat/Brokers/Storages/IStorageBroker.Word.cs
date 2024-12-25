//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Lugat.Models.Foundations.Words;

namespace Lugat.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Word> InsertWordAsync(Word Word);
        ValueTask<IQueryable<Word>> SelectAllWordsAsync();
        ValueTask<Word> SelectWordByIdAsync(int WordId);
        ValueTask<Word> UpdateWordAsync(Word Word);
        ValueTask<Word> DeleteWordAsync(Word Word);
    }
}
