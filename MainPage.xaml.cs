using AlcalaTFG.models;
using AlcalaTFG.services;

namespace AlcalaTFG
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            APIService.ExecuteRequest();
            
        }
    }

}
