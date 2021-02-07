using System;
using project.DA;
using project.Models;

namespace project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NotesContext notesContext = new NotesContext();
            notesContext.SaveChanges();
        }
    }
}
