using CommonLayer.Model;
using LogicLayer.Interface;
using LogicLayer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerBL icustomerBL;
        private readonly ILogger<CustomerController> logger;
        public CustomerController(ICustomerBL icustomerBL, ILogger<CustomerController> logger)
        {
            this.icustomerBL = icustomerBL;
            this.logger = logger;
        }

        //Create
        [HttpPost]
        [Route("Register")]
        public IActionResult CustomerRegistration(CustomerRegistrationModel Insert)
        {
            try
            {
                var response = icustomerBL.EmailExists(Insert.Email);
                if (response == null)
                {
                    var result = icustomerBL.CustomerRegistration(Insert);
                    if (result != null)
                    {
                        return Ok(new { success = true, message = "Data inserted successfully", data = result });
                    }
                    else
                    {
                        return BadRequest(new { success = false, message = "Inserted data failed" });
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

        //Retrieve
        [HttpGet]
        [Route("GetAllDetails")]
        public IActionResult RetrieveData()
        {
            try
            {
                var result = icustomerBL.RetrieveData();
                if (result != null)
                {
                    throw new Exception("Error Occured");
                    return Ok(new { success = true, message = "Retrieve data successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Retrieve data failed" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;
            }
        }

        //Update
        [HttpPut]
        [Route("UpdateData")]
        public IActionResult UpdateData(long CustomerId, CustomerUpdateModel update)
        {
            try
            {
                var result = icustomerBL.UpdateData(CustomerId, update);
                if (result)
                {
                    return Ok(new { success = true, message = "Update data successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Update data failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Delete
        [HttpDelete]
        [Route("DeleteData")]
        public IActionResult DeleteData(long CustomerId)
        {
            try
            {
                var result = icustomerBL.DeleteData(CustomerId);
                if (result)
                {
                    return Ok(new { success = true, message = "Delete data successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Delete data failed" });
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
        public IActionResult GetById(long CustomerId)
        {
            try
            {
                var result = icustomerBL.GetById(CustomerId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Get data successfully using by Id", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Get data failed using by Id" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Count
        [HttpGet]
        [Route("CountOfCustomers")]
        public IActionResult CountOfCustomers()
        {
            try
            {
                int result = icustomerBL.CountOfCustomers();
                if (result != 0)
                {
                    return Ok(new { success = true, message = "CountOfCustomers get successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "CountOfCustomers get failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //EmailExists
        [HttpGet]
        [Route("CheckEmailExists")]
        public IActionResult EmailExists(string Email)
        {
            try
            {
                var result = icustomerBL.EmailExists(Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Email Exists", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Email not Exists" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        //Login
        [HttpPost]
        [Route("CustomerLogin")]
        public IActionResult CustomerLogin(CustomerLoginModel login)
        {
            try
            {
                var result = icustomerBL.CustomerLogin(login);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Login Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Login failed" });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
