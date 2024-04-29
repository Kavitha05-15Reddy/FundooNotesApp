using LogicLayer.Interface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Service
{
    public class LabelBL :ILabelBL
    {
        private readonly ILabelRL ilabelRL;
        public LabelBL(ILabelRL ilabelRL)
        {
            this.ilabelRL = ilabelRL;
        }

        //AddLabel
        public object AddLabel(long userId, long notesId, string labelName)
        {
            try
            {
                return ilabelRL.AddLabel(userId,notesId,labelName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //GetAllLabels
        public object GetAllLabels()
        {
            try
            {
                return ilabelRL.GetAllLabels();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //CountOfAllLabels
        public object CountOfAllLabels()
        {
            try
            {
                return ilabelRL.CountOfAllLabels();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //UpdateLabel
        public bool UpdateLabel(long userId, long labelId, string labelName)
        {
            try
            {
                return ilabelRL.UpdateLabel(userId,labelId,labelName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //DeleteLabel
        public bool DeleteLabel(long userId, long labelId)
        {
            try
            {
                return ilabelRL.DeleteLabel(userId, labelId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
