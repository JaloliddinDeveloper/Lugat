//--------------------------------------------------
// Copyright (c) Coalition Of Good-Hearted Engineers
// Free To Use To Find Comfort And Peace
//--------------------------------------------------
using Lugat.Brokers.Storages;
using Lugat.Models.Foundations.Words;

namespace Lugat.Services.Foundations.Words
{
    public class WordService: IWordService
    {
        private readonly IStorageBroker storageBroker;

        public WordService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public async ValueTask<Word> AddWordAsync(Word Word) =>
            await this.storageBroker.InsertWordAsync(Word);

        public async ValueTask<IQueryable<Word>> RetrieveAllWordsAsync() =>
            await this.storageBroker.SelectAllWordsAsync();

        public async ValueTask<Word> RetrieveWordByIdAsync(int id) =>
            await this.storageBroker.SelectWordByIdAsync(id);

        public async ValueTask<Word> UpdateWordAsync(Word Word) =>
            await this.storageBroker.UpdateWordAsync(Word);

        public async ValueTask<Word> RemoveWordByIdAsync(int id)
        {
            Word maybeWord = await this.storageBroker.SelectWordByIdAsync(id);

            return await this.storageBroker.DeleteWordAsync(maybeWord);
        }
    }
}
