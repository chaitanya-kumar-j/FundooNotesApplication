using System;
using System.Collections.Generic;
using System.Text;
using UserDataBusinessLogicLayer.Interfaces;
using UserDataCommonLayer.Models;
using UserDataRepositoryLayer.Interfaces;

namespace UserDataBusinessLogicLayer.Services
{
    public class NotesOperations : INotesOperations
    {
        private INotesAccess _notesAccess;
        public NotesOperations(INotesAccess notesAccess)
        {
            this._notesAccess = notesAccess;
        }

        public Note AddOrRemoveLabel(int userId, int noteId, string label)
        {
            try
            {
                return this._notesAccess.AddOrRemoveLabel(userId, noteId, label);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Note AddOrRemoveCollaborations(int userId, int noteId, string email)
        {
            try
            {
                return this._notesAccess.AddOrRemoveCollaborations(userId, noteId, email);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Note ArchiveOrUnArchive(int userId, int noteId)
        {
            try
            {
                return this._notesAccess.ArchiveOrUnArchive(userId, noteId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Note ChangeNoteColour(int userId, int noteId, string newNoteColour)
        {
            try
            {
                return this._notesAccess.ChangeNoteColour(userId, noteId, newNoteColour);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Note CreateNote(int userId, CreateNote createNote)
        {
            try
            {
                return this._notesAccess.CreateNote(userId, createNote);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Note DeleteOrRestoreNote(int userId, int noteId)
        {
            try
            {
                return this._notesAccess.DeleteOrRestoreNote(userId, noteId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Note EditNote(int userId, int noteId, string bodyOfText)
        {
            try
            {
                return this._notesAccess.EditNote(userId, noteId, bodyOfText);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Note PinOrUnPin(int userId, int noteId)
        {
            try
            {
                return this._notesAccess.PinOrUnPin(userId, noteId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Note ViewNote(int userId, int noteId)
        {
            try
            {
                return this._notesAccess.ViewNote(userId, noteId);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
