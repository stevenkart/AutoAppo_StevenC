using AutoAppo_StevenC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AutoAppo_StevenC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
            UserName.Text = GlobalObjects.LocalUser.Nombre;
            UserRolAs.Text = "User logged as: " + GlobalObjects.LocalUser.RolDescripcion;
        }



        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AppoLoginPage());
        }

        private void BtnContinue_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new OptionsPage());
        }
    }
}