using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using UserDataCommonLayer.Models;
using UserDataRepositoryLayer.Interfaces;

namespace UserDataRepositoryLayer.Services
{
    public class NotesAccess : INotesAccess
    {
        private readonly string _connectionString;
        private readonly string _secretKey;
        private IConfiguration _configuration;
        public NotesAccess(IConfiguration configuration, IEmailService emailService)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("defaultConnection");
            _secretKey = configuration.GetValue<string>("AppSettings:SecretKey");
            
        }


        public Note ArchiveOrUnArchive(int userId, int noteId)
        {
            try
            {
                DataSet dataSet = new DataSet();
                Note note = new Note();
                string storedProcedure = "spArchiveOrUnArchive";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NoteId", noteId);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataSet, "UsersNotesInfo");
                        }
                    }
                    foreach (DataRow row in dataSet.Tables["UsersNotesInfo"].Rows)
                    {
                        note.UserId = (int)row["UserId"];
                        note.NoteId = (int)row["NoteId"];
                        note.NoteTitle = (string)row["NoteTitle"];
                        note.NoteBody = (string)row["NoteBody"];
                        note.NoteColour = (string)row["NoteColour"];
                        note.Label = (string)row["Label"];
                        note.Collaborations = (string)row["Collaborations"];
                        note.Reminder = (DateTime)row["Reminder"];
                        note.IsArchive = (bool)row["IsArchive"];
                        note.IsPin = (bool)row["IsPin"]; ;
                        note.IsTrash = (bool)row["IsTrash"];
                        note.CreatedDateTime = (DateTime)row["CreatedDateTime"];
                        note.LastModifiedDateTime = (DateTime)row["LastModifiedDateTime"];
                    }
                    connection.Close();
                }
                return note;
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
                DataSet dataSet = new DataSet();
                Note note = new Note();
                string storedProcedure = "spChangeColourOfNote";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NoteId", noteId);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@NoteColour", newNoteColour);
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataSet, "UsersNotesInfo");
                        }
                    }
                    foreach (DataRow row in dataSet.Tables["UsersNotesInfo"].Rows)
                    {
                        note.UserId = (int)row["UserId"];
                        note.NoteId = (int)row["NoteId"];
                        note.NoteTitle = (string)row["NoteTitle"];
                        note.NoteBody = (string)row["NoteBody"];
                        note.NoteColour = (string)row["NoteColour"];
                        note.Label = (string)row["Label"];
                        note.Collaborations = (string)row["Collaborations"];
                        note.Reminder = (DateTime)row["Reminder"];
                        note.IsArchive = (bool)row["IsArchive"];
                        note.IsPin = (bool)row["IsPin"]; ;
                        note.IsTrash = (bool)row["IsTrash"];
                        note.CreatedDateTime = (DateTime)row["CreatedDateTime"];
                        note.LastModifiedDateTime = (DateTime)row["LastModifiedDateTime"];
                    }
                    connection.Close();
                }
                return note;
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
                DataSet dataSet = new DataSet();
                Note note = new Note();
                string storedProcedure = "spCreateNote";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@NoteTitle", createNote.NoteTitle);
                        cmd.Parameters.AddWithValue("@NoteBody", createNote.NoteBody);
                        cmd.Parameters.AddWithValue("@NoteColour", createNote.NoteColour);
                        cmd.Parameters.AddWithValue("@Reminder", createNote.Reminder);
                        cmd.Parameters.AddWithValue("@Label", createNote.Label);
                        cmd.Parameters.AddWithValue("@Collaborations", createNote.Collaborations);
                        cmd.Parameters.AddWithValue("@IsArchive", createNote.IsArchive);
                        cmd.Parameters.AddWithValue("@IsPin", createNote.IsPin);
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataSet, "UsersNotesInfo");
                        }
                    }
                    foreach (DataRow row in dataSet.Tables["UsersNotesInfo"].Rows)
                    {
                        note.UserId = (int)row["UserId"];
                        note.NoteId = (int)row["NoteId"];
                        note.NoteTitle = (string)row["NoteTitle"];
                        note.NoteBody = (string)row["NoteBody"];
                        note.NoteColour = (string)row["NoteColour"];
                        note.Label = (string)row["Label"];
                        note.Collaborations = (string)row["Collaborations"];
                        note.Reminder = (DateTime)row["Reminder"];
                        note.IsArchive = (bool)row["IsArchive"];
                        note.IsPin = (bool)row["IsPin"]; ;
                        note.IsTrash = (bool)row["IsTrash"];
                        note.CreatedDateTime = (DateTime)row["CreatedDateTime"];
                        note.LastModifiedDateTime = (DateTime)row["LastModifiedDateTime"];
                    }
                    connection.Close();
                }
                return note;
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
                DataSet dataSet = new DataSet();
                Note note = new Note();
                string storedProcedure = "spDeleteOrRestoreNote";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NoteId", noteId);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataSet, "UsersNotesInfo");
                        }
                    }
                    foreach (DataRow row in dataSet.Tables["UsersNotesInfo"].Rows)
                    {
                        note.UserId = (int)row["UserId"];
                        note.NoteId = (int)row["NoteId"];
                        note.NoteTitle = (string)row["NoteTitle"];
                        note.NoteBody = (string)row["NoteBody"];
                        note.NoteColour = (string)row["NoteColour"];
                        note.Label = (string)row["Label"];
                        note.Collaborations = (string)row["Collaborations"];
                        note.Reminder = (DateTime)row["Reminder"];
                        note.IsArchive = (bool)row["IsArchive"];
                        note.IsPin = (bool)row["IsPin"]; ;
                        note.IsTrash = (bool)row["IsTrash"];
                        note.CreatedDateTime = (DateTime)row["CreatedDateTime"];
                        note.LastModifiedDateTime = (DateTime)row["LastModifiedDateTime"];
                    }
                    connection.Close();
                }
                return note;
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
                DataSet dataSet = new DataSet();
                Note note = new Note();
                string storedProcedure = "spEditNote";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NoteId", noteId);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        cmd.Parameters.AddWithValue("@NoteBody", bodyOfText);
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataSet, "UsersNotesInfo");
                        }
                    }
                    foreach (DataRow row in dataSet.Tables["UsersNotesInfo"].Rows)
                    {
                        note.UserId = (int)row["UserId"];
                        note.NoteId = (int)row["NoteId"];
                        note.NoteTitle = (string)row["NoteTitle"];
                        note.NoteBody = (string)row["NoteBody"];
                        note.NoteColour = (string)row["NoteColour"];
                        note.Label = (string)row["Label"];
                        note.Collaborations = (string)row["Collaborations"];
                        note.Reminder = (DateTime)row["Reminder"];
                        note.IsArchive = (bool)row["IsArchive"];
                        note.IsPin = (bool)row["IsPin"]; ;
                        note.IsTrash = (bool)row["IsTrash"];
                        note.CreatedDateTime = (DateTime)row["CreatedDateTime"];
                        note.LastModifiedDateTime = (DateTime)row["LastModifiedDateTime"];
                    }
                    connection.Close();
                }
                return note;
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
                DataSet dataSet = new DataSet();
                Note note = new Note();
                string storedProcedure = "spPinOrUnPin";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NoteId", noteId);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataSet, "UsersNotesInfo");
                        }
                    }
                    foreach (DataRow row in dataSet.Tables["UsersNotesInfo"].Rows)
                    {
                        note.UserId = (int)row["UserId"];
                        note.NoteId = (int)row["NoteId"];
                        note.NoteTitle = (string)row["NoteTitle"];
                        note.NoteBody = (string)row["NoteBody"];
                        note.NoteColour = (string)row["NoteColour"];
                        note.Label = (string)row["Label"];
                        note.Collaborations = (string)row["Collaborations"];
                        note.Reminder = (DateTime)row["Reminder"];
                        note.IsArchive = (bool)row["IsArchive"];
                        note.IsPin = (bool)row["IsPin"]; ;
                        note.IsTrash = (bool)row["IsTrash"];
                        note.CreatedDateTime = (DateTime)row["CreatedDateTime"];
                        note.LastModifiedDateTime = (DateTime)row["LastModifiedDateTime"];
                    }
                    connection.Close();
                }
                return note;
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
                DataSet dataSet = new DataSet();
                Note note = new Note();
                string storedProcedure = "spViewNote";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NoteId", noteId);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataSet, "UsersNotesInfo");
                        }
                    }
                    foreach (DataRow row in dataSet.Tables["UsersNotesInfo"].Rows)
                    {
                        note.UserId = (int)row["UserId"];
                        note.NoteId = (int)row["NoteId"];
                        note.NoteTitle = (string)row["NoteTitle"];
                        note.NoteBody = (string)row["NoteBody"];
                        note.NoteColour = (string)row["NoteColour"];
                        note.Label = (string)row["Label"];
                        note.Collaborations = (string)row["Collaborations"];
                        note.Reminder = (DateTime)row["Reminder"];
                        note.IsArchive = (bool)row["IsArchive"];
                        note.IsPin = (bool)row["IsPin"]; ;
                        note.IsTrash = (bool)row["IsTrash"];
                        note.CreatedDateTime = (DateTime)row["CreatedDateTime"];
                        note.LastModifiedDateTime = (DateTime)row["LastModifiedDateTime"];
                    }
                    connection.Close();
                }
                return note;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public Note AddOrRemoveLabel(int userId, int noteId, string label)
        {
            try
            {
                DataSet dataSet = new DataSet();
                Note note = new Note();
                string storedProcedure = "spAddOrRemoveLabel";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NoteId", noteId);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataSet, "UsersNotesInfo");
                        }
                    }
                    foreach (DataRow row in dataSet.Tables["UsersNotesInfo"].Rows)
                    {
                        note.UserId = (int)row["UserId"];
                        note.NoteId = (int)row["NoteId"];
                        note.NoteTitle = (string)row["NoteTitle"];
                        note.NoteBody = (string)row["NoteBody"];
                        note.NoteColour = (string)row["NoteColour"];
                        note.Label = (string)row["Label"];
                        note.Collaborations = (string)row["Collaborations"];
                        note.Reminder = (DateTime)row["Reminder"];
                        note.IsArchive = (bool)row["IsArchive"];
                        note.IsPin = (bool)row["IsPin"]; ;
                        note.IsTrash = (bool)row["IsTrash"];
                        note.CreatedDateTime = (DateTime)row["CreatedDateTime"];
                        note.LastModifiedDateTime = (DateTime)row["LastModifiedDateTime"];
                    }
                    connection.Close();
                }
                return note;
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
                DataSet dataSet = new DataSet();
                Note note = new Note();
                string storedProcedure = "spAddOrRemoveCollaborations";
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedure, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@NoteId", noteId);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataSet, "UsersNotesInfo");
                        }
                    }
                    foreach (DataRow row in dataSet.Tables["UsersNotesInfo"].Rows)
                    {
                        note.UserId = (int)row["UserId"];
                        note.NoteId = (int)row["NoteId"];
                        note.NoteTitle = (string)row["NoteTitle"];
                        note.NoteBody = (string)row["NoteBody"];
                        note.NoteColour = (string)row["NoteColour"];
                        note.Label = (string)row["Label"];
                        note.Collaborations = (string)row["Collaborations"];
                        note.Reminder = (DateTime)row["Reminder"];
                        note.IsArchive = (bool)row["IsArchive"];
                        note.IsPin = (bool)row["IsPin"]; ;
                        note.IsTrash = (bool)row["IsTrash"];
                        note.CreatedDateTime = (DateTime)row["CreatedDateTime"];
                        note.LastModifiedDateTime = (DateTime)row["LastModifiedDateTime"];
                    }
                    connection.Close();
                }
                return note;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
