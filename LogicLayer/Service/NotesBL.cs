using CommonLayer.NotesModel;
using LogicLayer.Interface;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LogicLayer.Service
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL inotesRL;
        public NotesBL(INotesRL inotesRL)
        {
            this.inotesRL = inotesRL;
        }
        //CreateNotes
        public string CreateNotes(NotesModel request, long userId)
        {
            try
            {
                return inotesRL.CreateNotes(request, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //RetrieveAllNotes
        public object GetAllNotes()
        {
            try
            {
                return inotesRL.GetAllNotes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //CountOfAllNotes
        public int CountOfAllNotes()
        {
            try
            {
                return inotesRL.CountOfAllNotes();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //UpdateNotes
        public bool UpdateNotes(long noteId, long userId,NotesModel update)
        {
            try
            {
                return inotesRL.UpdateNotes(noteId, userId, update);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //DeleteNotes
        public bool DeleteNotes(long notesId, long userId)
        {
            try
            {
                return inotesRL.DeleteNotes(notesId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //GetNoteByNotesId
        public object GetNoteByNotesId(long notesId, long userId)
        {
            try
            {
                return inotesRL.GetNoteByNotesId(notesId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //GetNotesByUserId
        public object GetNotesByUserId(long userId)
        {
            try
            {
                return inotesRL.GetNotesByUserId(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //ArchiveNotes
        public object ArchiveNotes(long notesId, long userId)
        {
            try
            {
                return inotesRL.ArchiveNotes(notesId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //PinNotes
        public object PinNotes(long notesId, long userId)
        {
            try
            {
                return inotesRL.PinNotes(notesId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //TrashNotes
        public object TrashNotes(long notesId, long userId)
        {
            try
            {
                return inotesRL.TrashNotes(notesId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //ChangeNoteColor
        public object ChangeNoteColor(long notesId, long userId, string color)
        {
            try
            {
                return inotesRL.ChangeNoteColor(notesId, userId, color);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //AddRemider
        public object AddRemider(long notesId, long userId, DateTime reminder)
        {
            try
            {
                return inotesRL.AddRemider(notesId, userId, reminder);  
            }
            catch (Exception)
            {
                throw;
            }
        }

        //AddImage
        public object AddImage(long notesId, long userId, string filepath)
        {
            try
            {
                return inotesRL.AddImage(notesId, userId, filepath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //AddImage2
        public object AddImage2(long notesId, long userId, IFormFile Image)
        {
            try
            {
                return inotesRL.AddImage2(notesId, userId, Image);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Final Review

        //FetchNoteByTD
        public object FetchNote(long userId, string title, string description)
        {
            try
            {
                return inotesRL.FetchNote(userId, title, description);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //SearchNoteByDate
        public object SearchNote(DateTime createdDate)
        {
            try
            {
                return inotesRL.SearchNote(createdDate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Practice

        //CountOfNotesByUserId
        public int CountOfNotes(long userId)
        {
            try
            {
                return inotesRL.CountOfNotes(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //FindNotesByLabelName
        public object FindNotes(string labelName)
        {
            try
            {
                return inotesRL.FindNotes(labelName);   
            }
            catch (Exception)
            {
                throw;
            }
        }


        //FindCollaboratorByNotesId 
        public object FindCo(long noteId)
        {
            try
            {
                return inotesRL.FindCo(noteId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //CountOfCollaboratorsByUserId
        public int CountOfCo(long userId)
        {
            try
            {
                return inotesRL.CountOfCo(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
