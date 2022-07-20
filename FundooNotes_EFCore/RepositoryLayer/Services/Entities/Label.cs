using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Services.Entities
{
    public class Label
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }

        public string LabelName { get; set; }

        [ForeignKey("User")]
        public virtual int? UserId { get; set; }

        public virtual User users { get; set; }

        [ForeignKey("Notes")]
        public virtual int? NoteId { get; set; }

        public virtual Note note { get; set; }
    }
}
