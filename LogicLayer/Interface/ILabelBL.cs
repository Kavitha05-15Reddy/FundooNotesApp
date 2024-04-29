using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Interface
{
    public interface ILabelBL
    {
        public object AddLabel(long userId, long notesId, string labelName);
        public object GetAllLabels();
        public object CountOfAllLabels();
        public bool UpdateLabel(long userId, long labelId, string labelName);
        public bool DeleteLabel(long userId, long labelId);
    }
}
