using Bookie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookie.Application.Interfaces
{
    public interface IShelfBookRepository
    {
        Task AddBookToShelfAsync(Guid shelfId, Guid bookId, DateTime addedAt);
        Task RemoveBookFromShelfAsync(Guid shelfId, Guid bookId);
        Task<IEnumerable<ShelfBook>> GetBooksInShelfAsync(Guid shelfId);
    }
}
