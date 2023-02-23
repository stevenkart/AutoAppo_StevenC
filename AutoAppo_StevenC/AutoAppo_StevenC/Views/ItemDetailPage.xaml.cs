using AutoAppo_StevenC.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AutoAppo_StevenC.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}