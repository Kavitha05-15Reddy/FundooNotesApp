using LogicLayer.Interface;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Service
{
    public class CollaboratorBL : ICollaboratorBL
    {
        private readonly ICollaboratorRL icollaboratorRL;
        public CollaboratorBL(ICollaboratorRL icollaboratorRL)
        {
            this.icollaboratorRL = icollaboratorRL;
        }

        //AddCollaborator
        public object AddCollaborator(long userId, long noteId, string Email)
        {
            try
            {
                return icollaboratorRL.AddCollaborator(userId, noteId, Email);
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
                return icollaboratorRL.UpdateCollaborator(userId, c_Id, c_Email);
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
                return icollaboratorRL.DeleteCollaborator(userId,c_Id);
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
                return icollaboratorRL.GetAllCollaborators();
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
                return icollaboratorRL.CountOfAllCollaborators();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
