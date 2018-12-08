﻿using HomeLibrary.DatabaseModel;
using HomeLibrary.WebApp.Data;
using HomeLibrary.WebApp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeLibrary.WebApp.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private ApplicationDbContext context;
        private bool disposed = false;

        public AuthorRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void DeleteAuthor(int authorId)
        {
            Author toDelete = context.Authors.Find(authorId);
            context.Authors.Remove(toDelete);
        }

       

        public Author GetAuthorById(int authorId)
        {
            return context.Authors.Find(authorId);
        }

        public IEnumerable<Author> GetAuthors()
        {
            return context.Authors.ToList();
        }

        public Author InsertAuthor(Author author)
        {
            context.Authors.Add(author);
            return author;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateAuthor(Author author)
        {
            context.Entry(author).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
