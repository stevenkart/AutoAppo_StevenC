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
    public partial class OptionsPage : ContentPage
    {
        public OptionsPage()
        {
            InitializeComponent();
            UserName.Text = GlobalObjects.LocalUser.Nombre;

            if (GlobalObjects.LocalUser.IdRol != 1)
            {
                BtnServicesManagment.IsVisible = false;
                BtnScheduleManagment.IsVisible = false;
            }
            else
            {
                BtnServicesManagment.IsVisible = true;
                BtnScheduleManagment.IsVisible = true;
                BtnUserManagment.IsVisible = true;
                BtnAppoManagment.IsVisible = true;
            }
        }
    }
}