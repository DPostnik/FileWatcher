using NLog;
using project.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace project.DA
{
    public class SQLNoteRepository: IRepository<Note>
    {
        
        private NotesContext _db;
        private readonly Logger _log;

        public SQLNoteRepository()
        {
            LogManager.LoadConfiguration("nlog.config");
            _log = LogManager.GetCurrentClassLogger();
            _db = new NotesContext();
        }

        public void Create(IEnumerable<Note> notes)
        {
            try
            {
                foreach (var note in notes)
                {
                    Create(note);
                }
            }
            catch (Exception e)
            {
                _log.Error($"Error while adding items to db: {e}");
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
