using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AutoAppo_StevenC.ViewModels;
using AutoAppo_StevenC.Models;

namespace AutoAppo_StevenC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingUpPage : ContentPage
    {

        UserViewModel viewModel;


        public SingUpPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new UserViewModel();


            //cargar la lista de roles en el picker
            LoadUserRolesList();
        }


        private async void LoadUserRolesList()
        {
            PickerUserRole.ItemsSource = await viewModel.GetUserRoles();
            PickerUserRole.FontSize = 16;
        }

        private async void BtnCancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void BtnApply_Clicked(object sender, EventArgs e)
        {

            //CAPTURAR el valor del id del role seleccionado en el picker

            UserRole selectedUserRole = PickerUserRole.SelectedItem as UserRole;

            bool R = await viewModel.AddUser(
                TxtEmail.Text.Trim(),
                TxtPassword.Text.Trim(),
                TxtName.Text.Trim(),
                TxtIDNumber.Text.Trim(),
                TxtPhone.Text.Trim(),
                TxtAddress.Text.Trim(),
                selectedUserRole.UserRoleId);

            if (R)
            {

                await DisplayAlert(":)", "User Added Successfully!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert(":(", "Somenthing went wrong!", "OK");
            }
            
            
            
            
            
        }
    }
}