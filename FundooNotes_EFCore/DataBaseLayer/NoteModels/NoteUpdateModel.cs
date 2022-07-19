using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DataBaseLayer.NoteModels
{
    public class NoteUpdateModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Bgcolor { get; set; }

        public bool IsPin { get; set; }

        [DefaultValue("false")]
        public bool IsArchive { get; set; }

        [DefaultValue("false")]
        public bool IsTrash { get; set; }
    }
}
