using CommonLayer.Model;
using CommonLayer.UserModel;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {

        public UserEntity UserRegistration(UserRegistrationModel registrationModel);
        public object GetAllUsers();
        public bool UserUpdate(long UserId, UserUpdateModel update);
        public bool UserDelete(long UserId);
        public string UserLogin(UserLoginModel user);
        public bool CheckEmailExists(string email);
        public ForgotPasswordModel ForgotPassword(string email);
        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel);
        public object GetById(long UserId);
        public int CountOfUsers();

        //Review
        public bool UpdateUser(long UserId, UpdateUserModel update);
        public object UserDetails(string userName);
        public int UsersCount();

        public UserDetailsModel UserLoginMethod(UserLoginModel login);

        //Final Review
        public object CheckUserExist(long userId, UserModel User);
    }
}
