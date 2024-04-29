using CommonLayer.Model;
using LogicLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : ControllerBase
    {
        private readonly ICollaboratorBL icollaboratorBL;
        public CollaboratorController(ICollaboratorBL icollaboratorBL)
        {
            this.icollaboratorBL = icollaboratorBL;
        }

        //AddCollaborator
        [HttpPost]
        [Route("AddCollaborator")]
        [Authorize]
        public IActionResult AddCollaborator(long noteId, string Email)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = icollaboratorBL.AddCollaborator(userId, noteId, Email);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Add Collaborator Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to Add Collaborator" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //UpdateCollaborator
        [HttpPut]
        [Route("UpdateCollaborator")]
        [Authorize]
        public IActionResult UpdateCollaborator(long c_Id, string c_Email)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = icollaboratorBL.UpdateCollaborator(userId, c_Id, c_Email);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { IsSuccess = true, Message = "Update Collaborator Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { IsSuccess = false, Message = "Failed to Add Collaborator" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //DeleteCollaborator
        [HttpDelete]
        [Route("DeleteCollaborator")]
        [Authorize]
        public IActionResult DeleteCollaborator(long c_Id)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = icollaboratorBL.DeleteCollaborator(userId, c_Id);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { IsSuccess = true, Message = "Delete Collaborator Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { IsSuccess = false, Message = "Failed to Delete Collaborator " });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //GetAllCollaborators
        [HttpGet]
        [Route("GetAllCollaborators")]
        public IActionResult GetAllCollaborators()
        {
            try
            {
                var result = icollaboratorBL.GetAllCollaborators();
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Get All Collaborators Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to Get All Collaborators " });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //CountOfAllCollaborators
        [HttpGet]
        [Route("CountOfAllCollaborators")]
        public IActionResult CountOfAllCollaborators()
        {
            try
            {
                var result = icollaboratorBL.CountOfAllCollaborators();
                if (result > 0)
                {
                    return Ok(new ResponseModel<int> { IsSuccess = true, Message = "Get Count Of All Collaborators Successfully", Data = result });
                }
                else
                {
                    return Ok(new ResponseModel<int> { IsSuccess = true, Message = "Failed to Get Count Of All Collaborators" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
