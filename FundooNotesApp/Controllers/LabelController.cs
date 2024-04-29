using CommonLayer.Model;
using LogicLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL ilabelBL;
        public LabelController(ILabelBL ilabelBL)
        {
            this.ilabelBL = ilabelBL;
        }

        //AddLabel
        [HttpPost]
        [Route("AddLabel")]
        [Authorize]
        public IActionResult AddLabel(long notesId, string labelName)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = ilabelBL.AddLabel(userId, notesId, labelName);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Add Label Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to Add Label" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //GetAllLabels
        [HttpGet]
        [Route("GetAllLabels")]
        public IActionResult GetAllLabels()
        {
            try {
                var result = ilabelBL.GetAllLabels();
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Get All Labels Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to Get All Labels" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //CountOfAllLabels
        [HttpGet]
        [Route("CountOfAllLabels")]
        public IActionResult CountOfAllLabels()
        {
            try
            {
                var result = ilabelBL.CountOfAllLabels();
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Get Count Of All Labels Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed Get to Count Of All Labels" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        
        //UpdateLabel
        [HttpPut]
        [Route("UpdateLabel")]
        [Authorize]
        public IActionResult UpdateLabel(long labelId, string labelName)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = ilabelBL.UpdateLabel(userId, labelId, labelName);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { IsSuccess = true, Message = "Update Label Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { IsSuccess = false, Message = "Failed to Update Label" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //DeleteLabel
        [HttpDelete]
        [Route("DeleteLabel")]
        [Authorize]
        public IActionResult DeleteLabel(long labelId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = ilabelBL.DeleteLabel(userId, labelId);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { IsSuccess = true, Message = "Delete Label Successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { IsSuccess = false, Message = "Failed to Delete Label" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
