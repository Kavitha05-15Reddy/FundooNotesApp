using CloudinaryDotNet;
using CommonLayer.Model;
using CommonLayer.UserModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration _config;
        public UserRL(FundooContext fundooContext, IConfiguration _config)
        {
            this.fundooContext = fundooContext;
            this._config = _config;
        }

        //Registration
        public UserEntity UserRegistration(UserRegistrationModel registrationModel)
        {
            try
            {
                UserEntity userEntity = new UserEntity();

                userEntity.FirstName = registrationModel.FirstName;
                userEntity.LastName = registrationModel.LastName;
                userEntity.Email = registrationModel.Email;
                //userEntity.Password = registrationModel.Password;
                userEntity.Password = EncodePassword(registrationModel.Password);

                fundooContext.UserTable.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if (result != 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Get all details 
        public object GetAllUsers()
        {
            try
            {
                var data = fundooContext.UserTable.ToList();
                if (data != null)
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Update
        public bool UserUpdate(long UserId, UserUpdateModel update)
        {
            try
            {
                var result = fundooContext.UserTable.FirstOrDefault(u => u.UserId == UserId);
                if (result != null)
                {
                    result.FirstName = update.FirstName;
                    result.LastName = update.LastName;
                    result.Email = update.Email;
                    result.Password = update.Password;

                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Delete
        public bool UserDelete(long UserId)
        {
            try
            {
                var result = fundooContext.UserTable.FirstOrDefault(d => d.UserId == UserId);
                if (result != null)
                {
                    fundooContext.UserTable.Remove(result);
                    fundooContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Encode 
        public static string EncodePassword(string password)
        {
            try
            {
                byte[] encodePassword = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(encodePassword);
            }
            catch (Exception ex)
            {
                return $"Encode failed: {ex.Message}";
            }
        }

        //Decode
        public static string DecodePassword(string password)
        {
            try
            {
                byte[] decodePassword = Convert.FromBase64String(password);
                return Encoding.UTF8.GetString(decodePassword);
            }
            catch (Exception ex)
            {
                return $"Decode failed: {ex.Message}";
            }
        }

        //Token
        private string GenerateToken(long userId, string userEmail)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",userEmail),
                new Claim("UserId",userId.ToString())
            };
            var token = new JwtSecurityToken
                (_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMonths(1),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        //Login
        public string UserLogin(UserLoginModel user)
        {
            try
            {
                //var userlogin = fundooContext.UserTable.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
                //if (userlogin != null)
                //{
                //    return GenerateToken(userlogin.UserId, userlogin.Email);
                //}

                //var userlogin = fundooContext.UserTable.FirstOrDefault(u => u.Email == user.Email);
                //if (userlogin != null)
                //{
                //    string userPass = DecodePassword(userlogin.Password);

                //    if (userlogin.Email.Equals(user.Email) && userPass.Equals(user.Password))
                //    {
                //        return GenerateToken(userlogin.UserId, userlogin.Email);
                //    }
                //}

                string encodePassword = EncodePassword(user.Password);
                var userlogin = fundooContext.UserTable.FirstOrDefault(u => u.Email == user.Email && u.Password == encodePassword);
                if (userlogin != null)
                {
                    return GenerateToken(userlogin.UserId, userlogin.Email);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Not founded" + ex.Message);
            }
            return null;
        }


        //EmailExists
        public bool CheckEmailExists(string email)
        {
            try
            {
                var result = fundooContext.UserTable.Any(u => u.Email == email);

                if (result)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Forgot Password
        public ForgotPasswordModel ForgotPassword(string Email)
        {
            var User = fundooContext.UserTable.ToList().Find(u => u.Email == Email);
            ForgotPasswordModel forgotPassword = new ForgotPasswordModel();
            forgotPassword.Email = User.Email;
            forgotPassword.UserId = User.UserId;
            forgotPassword.Token = GenerateToken(User.UserId, User.Email);

            return forgotPassword;
        }

        //Reset Password
        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        {
            var User = fundooContext.UserTable.ToList().Find(x => x.Email == Email);
            if (User != null)
            {
                User.Password = EncodePassword(resetPasswordModel.ConfirmPassword);
                fundooContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        //GetById
        public object GetById(long UserId)
        {
            try
            {
                var data = fundooContext.UserTable.ToList().Find(x => x.UserId == UserId);
                if (data != null)
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Count
        public int CountOfUsers()
        {
            try
            {
                var count = fundooContext.UserTable.Count();
                if (count > 0)
                {
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        //Review

        //-> Check availability of User using any table parameter(columns) and 
        //    if user exist update lastname and firstname.
        public bool UpdateUser(long UserId, UpdateUserModel update)
        {
            try
            {
                var result = fundooContext.UserTable.FirstOrDefault(x => x.UserId == UserId);

                if (result != null)
                {
                    result.FirstName = update.FirstName;
                    result.LastName = update.LastName;

                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //-> search for User using username, if user exist show user details.
        //    If more than one user exists show details of all of them.
        public object UserDetails(string userName)
        {
            try
            {
                var result = fundooContext.UserTable.ToList().FindAll(x => x.FirstName == userName);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //-> count the no of users
        public int UsersCount()
        {
            try
            {
                int result = fundooContext.UserTable.Count();
                if (result > 0)
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //UserLoginMethod
        public UserDetailsModel UserLoginMethod(UserLoginModel login)
        {
            try
            {
                string encodePassword = EncodePassword(login.Password);
                var userLogin = fundooContext.UserTable.FirstOrDefault(u => u.Email == login.Email && u.Password == encodePassword);
                if (userLogin != null)
                {
                    UserDetailsModel user = new UserDetailsModel();

                    user.UserId = userLogin.UserId;
                    user.FirstName = userLogin.FirstName;
                    user.LastName = userLogin.LastName;
                    user.Email = userLogin.Email;
                    user.Password = encodePassword;
                    user.Token = GenerateToken(userLogin.UserId, userLogin.Email);

                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Final Review

        //1) check if a user id exist. if exists, update the userdetails else insert a new deatail of user
        public object CheckUserExist(long userId, UserModel User)
        {
            try
            {
                var user = fundooContext.UserTable.FirstOrDefault(u => u.UserId == userId);
                if (user != null)
                {
                    user.FirstName = User.FirstName;
                    user.LastName = User.LastName;
                    user.Email = User.Email;
                    user.Password = User.Password;

                    fundooContext.SaveChanges();

                    return user;
                }
                else
                {
                    UserEntity userEntity = new UserEntity();

                    userEntity.FirstName = User.FirstName;
                    userEntity.LastName = User.LastName;
                    userEntity.Email = User.Email;
                    userEntity.Password = User.Password;

                    fundooContext.UserTable.Add(userEntity);
                    fundooContext.SaveChanges();

                    return userEntity;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}



