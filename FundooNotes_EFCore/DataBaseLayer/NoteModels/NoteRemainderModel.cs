using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataBaseLayer.NoteModels
{
    public class NoteRemainderModel
    {
        [Required]
        [DefaultValue("2022-01-01T00:00:00.000Z")]
        [RegularExpression(@"^([0-9]{4})-([0-1][0-9])-([0-3][0-9])T([0-1][0-9]|[2][0-3]):([0-5][0-9]):([0-5][0-9].[0-9]{3})Z$", ErrorMessage = "Please Enter the Valid Data time eg.2021-07-20 07:10:00.00")]

        public string Remainder { get; set; }
    }
}
