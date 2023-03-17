using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoAppo_StevenC.Models;


namespace AutoAppo_StevenC.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        //VM gestiona los cambios que ocurren entre M y V.

        public UserRole MyUserRole { get; set; }
        public UserStatus MyUserStatus { get; set; }
        public User MyUser { get; set; }

        public UserDTO MyUserDTO { get; set; }

        public UserViewModel()
        {
            MyUser = new User();
            MyUserRole = new UserRole();
            MyUserStatus = new UserStatus();
            MyUserDTO = new UserDTO();
        }

        //FUNCIONALIDAD principal del VM

        public async Task<UserDTO> GetUserData(string pEmail)
        {
            if (IsBusy)
            {
                return null;
            }
            else
            {
                IsBusy = true;
            }

            try
            {
                UserDTO user = new UserDTO();

                user = await MyUserDTO.GetUserData(pEmail);

                if (user == null)
                {
                    return null;
                }
                else
                {
                    return user;
                }
            }
            catch (Exception)
            {
                return null;

                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }


        public async Task<bool> UserAccessValidation(string pEmail, string pPassword)
        {
            if (IsBusy)
            {
                return false;
            }
            else
            {
                IsBusy = true;
            }

            try
            {
                MyUser.Email = pEmail;
                MyUser.LoginPassword = pPassword;

                bool R = await MyUser.ValidateLogin();

                return R;

            }
            catch (Exception)
            {
                return false;

                throw;
            }
            finally 
            { 
                IsBusy = false; 
            }
        }

        //carga lista de roles de usuario
        public async Task<List<UserRole>> GetUserRoles()
        {
            try
            {
                List<UserRole> roles = new List<UserRole>();
                
                roles = await MyUserRole.GetAllUserRoleList();

                if (roles == null)
                {
                    return null;
                }
                else
                {
                    return roles;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AddUser(string pEmail, 
                                        string pPassword, 
                                        string pName, 
                                        string pIDNumber, 
                                        string pPhoneNumber, 
                                        string pAddress, 
                                        int pUserRole,
                                        int pUserStatus = 3)
        {

            if (IsBusy)
            {
                return false;
            }
            else
            {
                IsBusy = true;
            }

            try
            {
                MyUser.Email = pEmail;
                MyUser.LoginPassword = pPassword;
                MyUser.Name = pName;
                MyUser.PhoneNumber = pPhoneNumber;
                MyUser.Address = pAddress;
                MyUser.CardId = pIDNumber;


                MyUser.UserRoleId = pUserRole;
                MyUser.UserStatusId = pUserStatus;

                bool R = await MyUser.AddUser();
        
                return R;

            }
            catch (Exception)
            {
                return false;

                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }







    }
}
