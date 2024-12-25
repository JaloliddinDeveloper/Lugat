//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Lugat.Models.Foundations.Words;

namespace Lugat.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<Word> Words { get; set; }

        public async ValueTask<Word> InsertWordAsync(Word Word) =>
            await InsertAsync(Word);

        public async ValueTask<IQueryable<Word>> SelectAllWordsAsync() =>
            await SelectAllAsync<Word>();

        public async ValueTask<Word> SelectWordByIdAsync(int WordId) =>
           await SelectAsync<Word>(WordId);

        public async ValueTask<Word> UpdateWordAsync(Word Word) =>
           await UpdateAsync(Word);

        public async ValueTask<Word> DeleteWordAsync(Word Word) =>
          await DeleteAsync(Word);
    }
}
