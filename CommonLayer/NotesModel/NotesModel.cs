﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.NotesModel
{
    public class NotesModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool Archive { get; set; }
        public bool PinNotes { get; set; }
        public bool Trash { get; set; }
    }
}
