using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.NoteModels
{
    public class NoteResponseModel
    {
        public int NoteId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Bgcolor { get; set; }

        public bool IsTrash { get; set; }

        public bool IsArchive { get; set; }

        public int UserId { get; set; }

        public string Firstname { get; set; }

        public string Lasttname { get; set; }

        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
