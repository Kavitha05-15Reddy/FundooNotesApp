using CommonLayer.NotesModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public string CreateNotes(NotesModel request, long userId);
        public object GetAllNotes();
        public int CountOfAllNotes();
        public bool UpdateNotes(long noteId, long userId, NotesModel update);
        public bool DeleteNotes(long notesId, long userId);
        public object GetNoteByNotesId(long notesId, long userId);
        public object GetNotesByUserId(long userId);
        public object ArchiveNotes(long notesId, long userId);
        public object PinNotes(long notesId, long userId);
        public object TrashNotes(long notesId, long userId);
        public object ChangeNoteColor(long notesId, long userId, string color);
        public object AddRemider(long notesId, long userId, DateTime reminder);
        public object AddImage(long notesId, long userId, string filepath);
        public object AddImage2(long notesId, long userId, IFormFile Image);

        //Final Review
        public object FetchNote(long userId, string title, string description);
        public object SearchNote(DateTime createdDate);

        //Practice
        public int CountOfNotes(long userId);
        public object FindNotes(string labelName);
        public object FindCo(long noteId);
        public int CountOfCo(long userId);
    }
}
