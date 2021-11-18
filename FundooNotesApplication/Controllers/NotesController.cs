using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserDataBusinessLogicLayer.Interfaces;
using UserDataCommonLayer.Models;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private INotesOperations _notesOperations;

        public NotesController(INotesOperations notesOperations)
        {
            this._notesOperations = notesOperations;
        }

        [Authorize]
        [Route("Create")]
        [HttpPost]
        public ActionResult CreateNote(CreateNote createNote)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                Note note = this._notesOperations.CreateNote(userId,createNote);
                return this.Ok(new { Success = true, Message = "Note Creation is successful", Data = note });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }

        [Authorize]
        
        [HttpGet]
        [Route("View/{noteId:int}")]
        public ActionResult ViewNote(int noteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                Note note = this._notesOperations.ViewNote(userId, noteId);
                return this.Ok(new { Success = true, Message = "View note is successful", Data = note });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }

        [Authorize]
        [Route("Edit/{noteId:int}")]
        [HttpPut]
        public ActionResult EditNote(int noteId, string bodyOfText)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                Note note = this._notesOperations.EditNote(userId, noteId, bodyOfText);
                return this.Ok(new { Success = true, Message = "Note Edit is successful", Data = note });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("Delete/{noteId:int}")]
        public ActionResult DeleteOrRestoreNote(int noteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                Note note = this._notesOperations.DeleteOrRestoreNote(userId, noteId);
                return this.Ok(new { Success = true, Message = "Note delete or restore is successful", Data = note });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }

        [Authorize]
        [Route("Colour/{noteId:int}")]
        [HttpPut]
        public ActionResult ChangeNoteColour(int noteId, string newNoteColour)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                Note note = this._notesOperations.ChangeNoteColour(userId, noteId, newNoteColour);
                return this.Ok(new { Success = true, Message = "Note colour change is successful", Data = note });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }

        [Authorize]
        [Route("Archive/{noteId:int}")]
        [HttpPut]
        public ActionResult ArchiveOrUnArchive(int noteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                Note note = this._notesOperations.ArchiveOrUnArchive(userId, noteId);
                return this.Ok(new { Success = true, Message = "Note Archive/UnArchive is successful", Data = note });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }

        [Authorize]
        [Route("Pin/{noteId:int}")]
        [HttpPut]
        public ActionResult PinOrUnPin(int noteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                Note note = this._notesOperations.PinOrUnPin(userId, noteId);
                return this.Ok(new { Success = true, Message = "Note Pin/UnPin is successful", Data = note });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }

        [Authorize]
        [Route("Label/{noteId:int}")]
        [HttpPut]
        public ActionResult Label(int noteId, string label)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                Note note = this._notesOperations.AddOrRemoveLabel(userId, noteId, label);
                return this.Ok(new { Success = true, Message = "Adding/removing label to note is successful", Data = note });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }

        [Authorize]
        [Route("Collaborations/{noteId:int}")]
        [HttpPut]
        public ActionResult AddOrRemoveCollaborations(int noteId, string email)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                Note note = this._notesOperations.AddOrRemoveCollaborations(userId, noteId, email);
                return this.Ok(new { Success = true, Message = "Adding/removing collaborations to note is successful", Data = note });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Success = false, Message = e.Message });
            }
        }
    }
}
