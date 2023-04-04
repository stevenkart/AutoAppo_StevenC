using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoAppo_StevenC.Models;
using Xamarin.Essentials;
using Email = AutoAppo_StevenC.Models.Email;

namespace AutoAppo_StevenC.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        //VM gestiona los cambios que ocurren entre M y V.

        public Email MyEmail { get; set; }
        public RecoveryCode MyRecoveryCode { get; set; }    

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
            MyEmail = new Email();
            MyRecoveryCode = new RecoveryCode ();
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

        public async Task<bool> AddRecoveryCode(string pEmail)
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
                MyRecoveryCode.Email = pEmail;

                Random rand = new Random();
                
                char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'Ñ', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
                char[] numbers = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' }; 
                string tempCode = null;

                for (int i = 0; i < 6; i++)
                {
                    if (i<3)
                    {
                        tempCode += letters[rand.Next(0, 26)];
                    }
                    else
                    {
                        tempCode += numbers[rand.Next(0, 9)];
                    }
                    
                }

                //TAREA: GENERAR UN CODIGO ALEATORIO DE 6 DIGITOS ENTRE LETRAS MAYUSCULAS Y NUMEWROS 
                //EJEMPLO: QWE456, AOI123, API123

                MyRecoveryCode.RecoveryCode1 = tempCode;
                MyRecoveryCode.RecoveryCodeId = 0;

                bool R = await MyRecoveryCode.AddRecoveryCode();

                //UNA VEZ QUE SE HAYA GUARDADO CORRECTAMENTE EL RECOVERYCODE, SE ENVIA EL EMAIL
                if (R)
                {
                    MyEmail.SendTo = pEmail;
                    MyEmail.Subject = "AutoAPPO Password Recovery Code";
                    MyEmail.Message = string.Format("Your recovery code is: {0}", tempCode);

                    R = (MyEmail.SendEmail());

                }
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


        public async Task<bool> RecoveryCodeValidation(string pEmail, string pRecoveryCode)
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
                MyRecoveryCode.Email = pEmail;
                MyRecoveryCode.RecoveryCode1 = pRecoveryCode;

                bool R = await MyRecoveryCode.ValidateRecoveryCode();

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
