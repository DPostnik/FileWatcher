using System;
using System.Collections.Generic;
using project.Models;

namespace project.DA
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        void Create(IEnumerable<Note> notes);
        void Create(Note note);
    }
}
