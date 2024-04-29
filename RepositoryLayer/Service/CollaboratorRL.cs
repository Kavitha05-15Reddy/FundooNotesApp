using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class CollaboratorRL : ICollaboratorRL
    {
        private readonly FundooContext fundooContext;
        public CollaboratorRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        //AddCollaborator
        public object AddCollaborator(long userId,long noteId, string Email)
        {
            try
            {
                var UserDetails = fundooContext.NotesTable.FirstOrDefault(x => x.NotesId == noteId && x.UserId == userId);
                if (UserDetails != null)
                {
                    CollaboratorEntity collaborator = new CollaboratorEntity();

                    collaborator.UserId = userId;
                    collaborator.NotesId = noteId;
                    collaborator.C_Email = Email;

                    fundooContext.CollaboratorTable.Add(collaborator);
                    fundooContext.SaveChanges();
                    return collaborator;
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

        //UpdateCollaborator
        public bool UpdateCollaborator(long userId, long c_Id, string c_Email)
        {
            try
            {
                var collborator = fundooContext.CollaboratorTable.FirstOrDefault(x => x.C_Id == c_Id && x.UserId == userId);
                if (collborator != null)
                {
                    collborator.C_Email = c_Email;
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

        //DeleteCollaborator
        public bool DeleteCollaborator(long userId, long c_Id)
        {
            try
            {
                var collaborator = fundooContext.CollaboratorTable.FirstOrDefault(x => x.C_Id == c_Id && x.UserId == userId);
                if (collaborator != null)
                {
                    fundooContext.CollaboratorTable.Remove(collaborator);
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

        //GetAllCollaborators
        public object GetAllCollaborators()
        {
            try
            {
                var data = fundooContext.CollaboratorTable.ToList();
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

        //CountOfAllCollaborators
        public int CountOfAllCollaborators()
        {
            try
            {
                var count = fundooContext.CollaboratorTable.Count();
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
    }
}
