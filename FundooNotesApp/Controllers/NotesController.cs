using CommonLayer.Model;
using CommonLayer.NotesModel;
using LogicLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL inotesBL;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;
        public NotesController(INotesBL inotesBL, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.inotesBL = inotesBL;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
        }

        //CreateNotes
        [HttpPost]
        [Route("CreateNotes")]
        //[Authorize]
        public IActionResult CreateNotes(NotesModel request)
        {
            try
            {
                //long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                long userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
                var notes = inotesBL.CreateNotes(request, userId);
                if (notes != null)
                {
                    return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Notes added successfully", Data = notes });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Failed to add notes" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //RetrieveAllNotes
        [HttpGet]
        [Route("RetrieveAllNotes")]
        public IActionResult GetAllNotes()
        {
            try
            {
                var result = inotesBL.GetAllNotes();
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "All Notes get successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to get All Notes" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //CountOfAllNotes
        [HttpGet]
        [Route("CountOfAllNotes")]
        public IActionResult CountOfAllNotes()
        {
            try
            {
                var result = inotesBL.CountOfAllNotes();
                if (result > 0)
                {
                    return Ok(new ResponseModel<int> { IsSuccess = true, Message = "CountOfNotes get successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<int> { IsSuccess = false, Message = "Failed to get CountOfNotes" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //UpdateNotes
        [HttpPut]
        [Route("UpdateNotes")]
        [Authorize]
        public IActionResult UpdateNotes(long notesId,NotesModel update)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = inotesBL.UpdateNotes(notesId, userId, update);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { IsSuccess = true, Message = "Notes updated successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { IsSuccess = false, Message = "Failed to update notes" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //DeleteNotes
        [HttpDelete]
        [Route("DeleteNotes")]
        [Authorize]
        public IActionResult DeleteNotes(long notesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = inotesBL.DeleteNotes(notesId, userId);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { IsSuccess = true, Message = "Notes deleted successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { IsSuccess = false, Message = "Failed to delete notes" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //GetNoteByNotesId
        [HttpGet]
        [Route("GetNoteByNotesId")]
        [Authorize]
        public IActionResult GetNoteByNotesId(long notesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = inotesBL.GetNoteByNotesId(notesId,userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Note get successfully using NotesId", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to get Note using NotesId" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //GetNotesByUserId
        [HttpGet]
        [Route("GetNotesByUserId")]
        public IActionResult GetNotesByUserId(long userId)
        {
            try
            {
                var result = inotesBL.GetNotesByUserId(userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Notes get successfully using UserId", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to get Notes using UserId" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //ArchiveNotes
        [HttpGet]
        [Route("ArchiveNotes")]
        [Authorize]
        public object ArchiveNotes(long notesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = inotesBL.ArchiveNotes(notesId, userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Archive Notes Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to Archive Notes" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //PinNotes
        [HttpGet]
        [Route("PinNotes")]
        [Authorize]
        public IActionResult PinNotes(long notesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = inotesBL.PinNotes(notesId,userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Pin Notes Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to Pin Notes" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //TrashNotes
        [HttpGet]
        [Route("TrashNotes")]
        [Authorize]
        public IActionResult TrashNotes(long notesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = inotesBL.TrashNotes(notesId, userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Trash Notes Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to Trash Notes " });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //ChangeNoteColor
        [HttpGet]
        [Route("ChangeNoteColor")]
        [Authorize]
        public IActionResult ChangeNoteColor(long notesId, string color)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = inotesBL.ChangeNoteColor(notesId, userId, color);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Change Note Color Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to Change Note Color" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //AddRemider
        [HttpGet]
        [Route("AddRemider")]
        [Authorize]
        public IActionResult AddRemider(long notesId, DateTime reminder)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = inotesBL.AddRemider(notesId, userId, reminder);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Add Remider Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to Add Remider" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //GetAllNotesUsiingRedisCache
        [HttpGet]
        [Route("Redis")]
        public async Task<IActionResult> GetAllNotesUsiingRedisCache()
        {
            var cachKey = "NotesList";
            string serializedNotedList;

            var NotesList = new List<NotesEntity>();
            byte[] redisNotesList = await distributedCache.GetAsync(cachKey);
            if (redisNotesList != null)
            {
                serializedNotedList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotedList);

            }
            else
            {
                NotesList = fundooContext.NotesTable.ToList();
                serializedNotedList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotedList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cachKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }

        //AddImage
        [HttpPost]
        [Route("AddImage")]
        [Authorize]
        public object AddImage(long notesId, string filepath)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = inotesBL.AddImage(notesId, userId, filepath);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Add Image Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to Add Image" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //AddImage2
        [HttpPost]
        [Route("AddImage2")]
        [Authorize]
        public object AddImage2(long notesId, IFormFile Image)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = inotesBL.AddImage2(notesId, userId, Image);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Add Image Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to Add Image" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Final Review

        //FetchNoteByTD
        [HttpGet]
        [Route("FetchNoteByTD")]
        [Authorize]
        public IActionResult FetchNote(string title, string description)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = inotesBL.FetchNote(userId, title, description);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Fetch Note successfully using title and descripton", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to Fetch Note using title and descripton" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //SearchNoteByDate
        [HttpGet]
        [Route("SearchNoteByDate")]
        public IActionResult SearchNote(DateTime createdDate)
        {
            try
            {
                var result = inotesBL.SearchNote(createdDate);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Search Note successfully using created Date", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to Search Note using created Date" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Practice

        //CountOfNotesByUserId
        [HttpGet]
        [Route("CountOfNotesByUserId")]
        public IActionResult CountOfNotes(long userId)
        {
            try
            {
                var result = inotesBL.CountOfNotes(userId);
                if (result > 0)
                {
                    return Ok(new ResponseModel<int> { IsSuccess = true, Message = "Count Of Notes get successfully using particular user", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<int> { IsSuccess = false, Message = "Failed to get Count Of Notes using particular user" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //FindNotesByLabelName
        [HttpGet]
        [Route("FindNotesByLabelName")]
        public IActionResult FindNotes(string labelName)
        {
            try
            {
                var result = inotesBL.FindNotes(labelName);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Find Notes successfully using label name", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to Find Notes using label name" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //FindCollaboratorByNotesId
        [HttpGet]
        [Route("FindCollaboratorByNotesId")]
        public IActionResult FindCo(long noteId)
        {
            try
            {
                var result = inotesBL.FindCo(noteId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Find Collab successfully in notes", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to Find Collab in notes" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //CountOfCollaboratorsByUserId
        [HttpGet]
        [Route("CountOfCollaboratorsByUserId")]
        public IActionResult CountOfCo(long userId)
        {
            try
            {
                var result = inotesBL.CountOfCo(userId);
                if (result > 0)
                {
                    return Ok(new ResponseModel<int> { IsSuccess = true, Message = "Count Of Collab get successfully using particular user", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<int> { IsSuccess = false, Message = "Failed to get Count Of collab using particular user" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
