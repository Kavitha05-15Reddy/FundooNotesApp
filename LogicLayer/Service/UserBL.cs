using CommonLayer.Model;
using CommonLayer.UserModel;
using LogicLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL iuserRL;

        public UserBL(IUserRL iuserRL)
        {
            this.iuserRL = iuserRL;
        }

        //Registration
        public UserEntity UserRegistration(UserRegistrationModel registrationModel)
        {
            try
            {
                return iuserRL.UserRegistration(registrationModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //GetAllData
        public object GetAllUsers()
        {
            try
            {
                return iuserRL.GetAllUsers();
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
                return iuserRL.UserUpdate(UserId, update);
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
                return iuserRL.UserDelete(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Login
        public string UserLogin(UserLoginModel user)
        {
            try
            {
                return iuserRL.UserLogin(user);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //EmailExists
        public bool CheckEmailExists(string email)
        {
            try
            {
                return iuserRL.CheckEmailExists(email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Forgot Password
        public ForgotPasswordModel ForgotPassword(string email)
        {
            try
            {
                return iuserRL.ForgotPassword(email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Reset Password
        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        {
            try
            {
                return iuserRL.ResetPassword(Email, resetPasswordModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //GetById
        public object GetById(long UserId)
        {
            try
            {
                return iuserRL.GetById(UserId);
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
                return iuserRL.CountOfUsers();
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Review

        //Update data by userId
        public bool UpdateUser(long UserId, UpdateUserModel update)
        {
            try
            {
                return iuserRL.UpdateUser(UserId, update);
            }
            catch (Exception)
            {
                throw;
            }

        }

        //Show UserDetails by userName
        public object UserDetails(string userName)
        {
            try
            {
                return iuserRL.UserDetails(userName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //UsersCount
        public int UsersCount()
        {
            try
            {
                return iuserRL.UsersCount();
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
                return iuserRL.UserLoginMethod(login);
            }
            catch (Exception)
            {
                throw;
            }
        }


        //Final Review
        public object CheckUserExist(long userId, UserModel User)
        {
            try
            {
                return iuserRL.CheckUserExist(userId, User);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
