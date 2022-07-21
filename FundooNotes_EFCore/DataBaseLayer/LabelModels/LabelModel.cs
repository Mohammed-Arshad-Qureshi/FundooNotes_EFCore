using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseLayer.LabelModels
{
    public class LabelModel
    {
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

}
