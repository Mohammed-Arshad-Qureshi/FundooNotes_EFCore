using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.LabelModels
{
    public class LabelUpdateModel
    {
        public int UserId { get; set; }

        public int NoteId { get; set; }

        public string LabelName { get; set; }
    }
}
