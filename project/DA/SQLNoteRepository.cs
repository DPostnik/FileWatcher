using System;
using System.Collections.Generic;
using System.Text;
using project.Models;

namespace project.DA
{
    class SQLNoteRepository: IRepository<Note>
    {
        private bool _disposed;
        private NotesContext _db;
        private readonly log4net.ILog _log;

        public SQLNoteRepository()
        {
            _log = log4net.LogManager.GetLogger(typeof(SQLNoteRepository));
        }

        public void Create(IEnumerable<Note> notes)
        {
            foreach (var note in notes)
            {
                Create(note);
            }
        }

        public void Create(Note note)
        {
            try
            {
                _db.Notes.Add(note);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                _log.Error($"Error while adding single item to db: {e}");
            }
        }


        public void Dispose()
        {
            _db?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
