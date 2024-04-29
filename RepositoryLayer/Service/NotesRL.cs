using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using CommonLayer.NotesModel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        private readonly FundooContext fundooContext;

        public NotesRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        //CreateNotes
        public string CreateNotes(NotesModel request, long userId)
        {
            try
            {
                if (userId > 0)
                {
                    UserEntity user = fundooContext.UserTable.FirstOrDefault(x => x.UserId == userId);
                    if (user != null)
                    {
                        NotesEntity notes = new NotesEntity();
                        notes.Title = request.Title;
                        notes.Description = request.Description;
                        notes.Reminder = request.Reminder;
                        notes.Color = request.Color;
                        notes.Image = request.Image;
                        notes.Archive = request.Archive;
                        notes.PinNotes = request.PinNotes;
                        notes.Trash = request.Trash;
                        notes.Created = DateTime.Now;
                        notes.Modified = DateTime.Now;

                        notes.UserId = userId;

                        fundooContext.NotesTable.Add(notes);
                        fundooContext.SaveChanges();
                        return "Notes created successfully";
                    }
                    else
                    {
                        return null;
                    }
                }
                return "Invalid UserId";
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
                var result = fundooContext.NotesTable.ToList();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
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
                var count = fundooContext.NotesTable.Count();
                if (count > 0)
                {
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //UpdateNotes
        public bool UpdateNotes(long notesId, long userId, NotesModel update)
        {
            try
            {
                var result = fundooContext.NotesTable.FirstOrDefault(u => u.NotesId == notesId && u.UserId == userId);
                if (result != null)
                {
                    result.Title = update.Title;
                    result.Description = update.Description;
                    result.Reminder = update.Reminder;
                    result.Color = update.Color;
                    result.Image = update.Image;
                    result.Archive = update.Archive;
                    result.PinNotes = update.PinNotes;
                    result.Trash = update.Trash;
                    result.Created = DateTime.Now;
                    result.Modified = DateTime.Now;

                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
                var result = fundooContext.NotesTable.FirstOrDefault(d => d.NotesId == notesId && d.UserId == userId);
                if (result != null)
                {
                    fundooContext.NotesTable.Remove(result);
                    fundooContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //GetNoteByNotesId
        public object GetNoteByNotesId(long notesId,long userId)
        {
            try
            {
                var data = fundooContext.NotesTable.ToList().Find(x => x.NotesId == notesId && x.UserId == userId);
                if (data != null)
                {
                    return data;
                }
                else
                {
                    return null;
                }
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
                var result = fundooContext.NotesTable.ToList().Where(x => x.UserId == userId);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //ArchiveNotes
        public object ArchiveNotes(long notesId,long userId)
        {
            try
            {
                var notes = fundooContext.NotesTable.FirstOrDefault(x => x.NotesId == notesId && x.UserId == userId);
                if (notes != null)
                {
                    if (notes.Archive == true)
                    {
                        notes.Archive = false;
                        fundooContext.SaveChanges();
                        return "Move note from Archive to Notes";
                    }
                    else
                    {
                        notes.Archive = true;
                        fundooContext.SaveChanges();
                        return "Move note from Notes to Archive";
                    }
                }
                return null;
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
                var notes = fundooContext.NotesTable.FirstOrDefault(x => x.NotesId == notesId && x.UserId == userId);
                if (notes != null)
                {
                    if (notes.PinNotes == true)
                    {
                        notes.PinNotes = false;
                        fundooContext.SaveChanges();
                        return "Un_Pinned the note";
                    }
                    else
                    {
                        notes.PinNotes = true;
                        fundooContext.SaveChanges();
                        return "Pinned the note";
                    }
                }
                return null;
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
                var notes = fundooContext.NotesTable.FirstOrDefault(x => x.NotesId == notesId && x.UserId == userId);
                if (notes != null)
                {
                    if (notes.Trash == true)
                    {
                        notes.Trash = false;
                        fundooContext.SaveChanges();
                        return "Move note from Trash to Notes";
                    }
                    else
                    {
                        notes.Trash = true;
                        fundooContext.SaveChanges();
                        return "Move note from Notes to Trash";
                    }
                }
                return null;
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
                var notes = fundooContext.NotesTable.FirstOrDefault(x => x.NotesId == notesId && x.UserId == userId);
                if (notes != null)
                {
                    notes.Color = color;
                    fundooContext.SaveChanges();
                    return notes;
                }
                else
                {
                    return null;
                }
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
                var notes = fundooContext.NotesTable.FirstOrDefault(x => x.NotesId == notesId && x.UserId == userId);
                if (notes != null)
                {
                    if (reminder > DateTime.Now)
                    {
                        notes.Reminder = reminder;
                        fundooContext.SaveChanges();
                        return notes;
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
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
                var note = fundooContext.NotesTable.FirstOrDefault(x => x.NotesId == notesId && x.UserId == userId);
                if(note != null)
                {
                    Account account = new Account(
                                  "duausvylr",
                                  "773325657996989",
                                  "DtP05TQxVYO6RA0MQUBYSyhuyI4");

                    Cloudinary cloudinary = new Cloudinary(account);

                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(filepath),
                        PublicId = note.Title
                    };

                    ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

                    note.Modified = DateTime.Now;
                    note.Image = uploadResult.Url.ToString(); ;
                    fundooContext.SaveChanges();

                    return note;
                }
                else
                {
                    return null;
                }
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
                var note = fundooContext.NotesTable.FirstOrDefault(x => x.NotesId == notesId && x.UserId == userId);
                if (note != null)
                {
                    Account account = new Account(
                                  "duausvylr",
                                  "773325657996989",
                                  "DtP05TQxVYO6RA0MQUBYSyhuyI4");

                    Cloudinary cloudinary = new Cloudinary(account);

                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(Image.FileName, Image.OpenReadStream()),
                        PublicId = note.Title
                    };

                    ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

                    note.Modified = DateTime.Now;
                    note.Image = uploadResult.Url.ToString(); ;
                    fundooContext.SaveChanges();

                    return note;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Final Review

        //2) fetch a note of user using title and description

        public object FetchNote(long userId, string title, string description)
        {
            try
            {
                var note = fundooContext.NotesTable.FirstOrDefault(x => x.UserId == userId && x.Title == title && x.Description == description);
                if (note != null)
                {
                    return note;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //3) search a note on basis of their created date and fetch details of notes
        public object SearchNote(DateTime createdDate)
        {
            try
            {
                var note = fundooContext.NotesTable.FirstOrDefault(x => x.Created == createdDate);
                if (note != null)
                {
                    return note;
                }
                else
                {
                    return null;
                }
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
            var notes = fundooContext.NotesTable.FirstOrDefault(x => x.UserId == userId);
            if (notes != null)
            {
                int count = fundooContext.NotesTable.Count();
                return count;
            }
            else
            {
                return 0;
            }
        }

        //FindNotesByLabelName
        public object FindNotes(string labelName)
        {
            try
            {
                var note = fundooContext.LabelTable.ToList().FindAll(x => x.LabelName == labelName);
                if (note != null)
                {
                    return note;
                }
                else
                {
                    return null;
                }
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
                var note = fundooContext.CollaboratorTable.FirstOrDefault(x => x.NotesId == noteId);
                if (note != null)
                {
                    return note;
                }
                else
                {
                    return null;
                }
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
                var collab = fundooContext.CollaboratorTable.FirstOrDefault(x => x.UserId == userId);
                if (collab != null)
                {
                    int count = fundooContext.CollaboratorTable.Count();
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}


