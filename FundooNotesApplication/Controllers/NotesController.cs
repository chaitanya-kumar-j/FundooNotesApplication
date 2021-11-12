using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        [Route("CreateNote")]
        [HttpGet]
        public void CreateNote(string nameOfNote)
        {

        }

        [Route("EditNote")]
        [HttpGet]
        public void EditNote(string nameOfNote)
        {

        }

        [Route("DeleteNote")]
        [HttpGet]
        public void DeleteNote(string nameOfNote)
        {

        }
    }
}
