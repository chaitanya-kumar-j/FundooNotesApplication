using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UserDataCommonLayer.Models
{
    public class CreateNote
    {
        [Required]
        public string NoteTitle { get; set; }
        public string NoteBody { get; set; }
        public string NoteColour { get; set; }
        public DateTime Reminder { get; set; }
        public string Label { get; set; }
        public string Collaborations { get; set; }
        public bool IsArchive { get; set; }
        public bool IsPin { get; set; }
    }
}
