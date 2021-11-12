using System;
using System.Collections.Generic;
using System.Text;

namespace UserDataRepositoryLayer.Interfaces
{
    interface INotesAccess
    {
        public void CreateNote(string nameOfNote);
        public void EditNote(string nameOfNote);
        public void DeleteNote(string nameOfNote);
    }
}
