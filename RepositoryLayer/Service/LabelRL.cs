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
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundooContext;
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        //AddLabel
        public object AddLabel(long userId, long notesId, string labelName)
        {
            try
            {
                var note = fundooContext.NotesTable.FirstOrDefault(x => x.UserId == userId && x.NotesId == notesId);
                if (note != null)
                {
                    LabelEntity label = new LabelEntity();

                    label.UserId = userId;
                    label.NotesId = notesId;
                    label.LabelName = labelName;

                    fundooContext.LabelTable.Add(label);
                    fundooContext.SaveChanges();

                    return label;
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

        //GetAllLabels
        public object GetAllLabels()
        {
            try
            {
                var data = fundooContext.LabelTable.ToList();
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

        //CountOfAllLabels
        public object CountOfAllLabels()
        {
            try
            {
                var count = fundooContext.LabelTable.Count();
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
        //UpdateLabel
        public bool UpdateLabel(long userId, long labelId, string labelName)
        {
            try
            {
                var label = fundooContext.LabelTable.FirstOrDefault(x => x.LabelId == labelId && x.UserId == userId);
                if (label != null)
                {
                    label.LabelName = labelName;
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

        //DeleteLabel
        public bool DeleteLabel(long userId, long labelId)
        {
            try
            {
                var label = fundooContext.LabelTable.FirstOrDefault(x => x.LabelId == labelId && x.UserId == userId);
                if (label != null)
                {
                    fundooContext.LabelTable.Remove(label);
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
    }
}
