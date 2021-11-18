using System;
using System.Collections.Generic;
using System.Text;
using UserDataCommonLayer.Models;

namespace UserDataBusinessLogicLayer.Interfaces
{
    public interface INotesOperations
    {
        Note CreateNote(int userId, CreateNote createNote);
        Note ViewNote(int userId, int noteId);
        Note EditNote(int userId, int noteId, string bodyOfText);
        Note DeleteOrRestoreNote(int userId, int noteId);
        Note ChangeNoteColour(int userId, int noteId, string newNoteColour);
        Note ArchiveOrUnArchive(int userId, int noteId);
        Note PinOrUnPin(int userId, int noteId);
        Note AddOrRemoveLabel(int userId, int noteId, string label);
        Note AddOrRemoveCollaborations(int userId, int noteId, string email);
    }
}
