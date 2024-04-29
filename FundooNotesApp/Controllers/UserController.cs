using CommonLayer.Model;
using CommonLayer.UserModel;
using LogicLayer.Interface;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL iuserBL;
        private readonly IBus bus;
        public UserController(IUserBL iuserBL, IBus bus)
        {
            this.iuserBL = iuserBL;
           
            this.bus  = bus;
        }

        //Registration
        [HttpPost]
        [Route("Register")]
        public IActionResult RegisterUser(UserRegistrationModel registrationModel)
        {
            try
            {
                var response = iuserBL.CheckEmailExists(registrationModel.Email);
                if (!response)
                {
                    var result = iuserBL.UserRegistration(registrationModel);
                    if (result != null)
                    {
                        return Ok(new { success = true, message = "Registration succeessfull", data = result });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Registration failed" });
                    }
                }
                else
                {
                    return BadRequest(new { success = false, message = "Email already exists" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //GetAllData
        [HttpGet]
        [Route("GetAllData")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var result = iuserBL.GetAllUsers();

                if (result != null)
                {
                    return Ok(new { success = true, message = "Get all data successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Get all data failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Update
        [HttpPut]
        [Route("Update Data")]
        public IActionResult UpdateUser(long UserId, UserUpdateModel update)
        {
            try
            {
                bool result = iuserBL.UserUpdate(UserId, update);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { IsSuccess = true, Message = "Successfully Data Updated", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { IsSuccess = false, Message = "Data Updation failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Delete
        [HttpDelete]
        [Route("Delete Data")]
        public IActionResult DeleteUser(long UserId)
        {
            try
            {
                bool result = iuserBL.UserDelete(UserId);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { IsSuccess = true, Message = "Successfully Data Deleted", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { IsSuccess = false, Message = "Data Deletion failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Login
        [HttpPost]
        [Route("Login")]
        public IActionResult LoginUser(UserLoginModel user)
        {
            try
            {
                var result = iuserBL.UserLogin(user);
                if (result != null)
                {
                    return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Login Sucessful", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { IsSuccess = false, Message = "Login failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //EmailExists
        [HttpPost]
        [Route("Email Exists")]
        public IActionResult CheckEmailExists(string email)
        {
            try
            {
                bool result = iuserBL.CheckEmailExists(email);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { IsSuccess = true, Message = "Email Exists", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { IsSuccess = false, Message = "Email not Exists" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Forgot Password
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            try
            {
                var password = iuserBL.ForgotPassword(Email);

                if (password != null)
                {
                    Send send = new Send();
                    ForgotPasswordModel forgotPasswordModel = iuserBL.ForgotPassword(Email);
                    send.SendMail(forgotPasswordModel.Email, forgotPasswordModel.Token);
                    Uri uri = new Uri("rabbitmq:://localhost/FunDooNotesEmailQueue");
                    var endPoint = await bus.GetSendEndpoint(uri);
                    await endPoint.Send(forgotPasswordModel);
                    return Ok(new ResponseModel<string> { IsSuccess = true, Message = "Mail sent Successfully", Data = password.Token });
                }
                else
                {
                    return NotFound(new ResponseModel<string> { IsSuccess = false, Message = "Email Does not Exist" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Reset Password
        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel reset)
        {
            try
            {
                string Email = User.Claims.FirstOrDefault(x => x.Type == "Email").Value;
                var result = iuserBL.ResetPassword(Email, reset);
                if (result)
                {
                    return Ok(new { success = true, message = "Password Reset is done" });
                }
                else
                {
                    return BadRequest("Password is not Updated");
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //GetById
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(long UserId)
        {
            try
            {
                var result = iuserBL.GetById(UserId);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Data get successfully using by Id", Data = result });
                }
                else
                {
                    return Ok(new ResponseModel<object> { IsSuccess = false, Message = "Failed to get Data using by Id" });

                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Count
        [HttpGet]
        [Route("Count of Users")]
        public IActionResult CountOfUsers()
        {
            try
            {
                var result = iuserBL.CountOfUsers();
                if (result > 0)
                {
                    return Ok(new ResponseModel<int> { IsSuccess = true, Message = "Count of Users get successfully", Data = result });
                }
                else
                {
                    return Ok(new ResponseModel<int> { IsSuccess = false, Message = "Failed to get Count of Users" });

                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Review
        //Update data by userId
        [HttpPut]
        [Route("UpdateData")]
        public IActionResult UpdateUserDetails(long UserId, UpdateUserModel update)
        {
            try
            {
                var result = iuserBL.UpdateUser(UserId, update);
                if (result)
                {
                    return Ok(new ResponseModel<bool> { IsSuccess = true, Message = "Data updated successfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { IsSuccess = false, Message = "Data updated failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Show UserDetails by userName
        [HttpGet]
        [Route("UserDetails")]
        public IActionResult UserDetails(string userName)
        {
            try
            {
                var result = iuserBL.UserDetails(userName);
                if (result != null)
                {
                    return Ok(new ResponseModel<object> { IsSuccess = true, Message = "User details get successfully by username", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "User details get failed by username" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //UersCount
        [HttpGet]
        [Route("UsersCount")]
        public IActionResult UsersCount()
        {
            try
            {
                int result = iuserBL.UsersCount();
                if (result > 0)
                {
                    return Ok(new ResponseModel<int> { IsSuccess = true, Message = "Users count get succeessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<int> { IsSuccess = false, Message = "Users count get failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //UserLoginMethod
        [HttpPost]
        [Route("UserLoginMethod")]
        public IActionResult UserLoginMethod(UserLoginModel login)
        {
            try
            {
                var result = iuserBL.UserLoginMethod(login);
                if (result != null)
                {
                    HttpContext.Session.SetInt32("UserId", (int)result.UserId);
                    return Ok(new ResponseModel<UserDetailsModel> { IsSuccess = true, Message = "Users Details get succeessfully", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<UserDetailsModel> { IsSuccess = false, Message = "Failed to get User Details" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Final Review
        [HttpPost]
        [Route("CheckUserExist")]
        public IActionResult CheckUserExist(long userId, UserModel User)
        {
            var result = iuserBL.CheckUserExist(userId, User);
            if (result != null)
            {
                return Ok(new ResponseModel<object> { IsSuccess = true, Message = "Update or insert data succeessfully", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<object> { IsSuccess = false, Message = "Failed to Update or insert data" });
            }
        }
    }
}

