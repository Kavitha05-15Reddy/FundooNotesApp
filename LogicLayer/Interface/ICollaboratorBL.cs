using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Interface
{
    public interface ICollaboratorBL
    {
        public object AddCollaborator(long userId, long noteId, string Email);
        public bool UpdateCollaborator(long userId, long c_Id, string c_Email);
        public bool DeleteCollaborator(long userId, long c_Id);
        public object GetAllCollaborators();
        public int CountOfAllCollaborators();
    }
}
