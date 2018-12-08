using HomeLibrary.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary.WebApp.Repository.Interface
{
    public interface IAuthorRepository:IDisposable
    {
        IEnumerable<Author> GetAuthors();
        Author GetAuthorById(int authorId);
        Author InsertAuthor(Author author);
        void DeleteAuthor(int authorId);
        void UpdateAuthor(Author author);
        void Save();
    }
}
