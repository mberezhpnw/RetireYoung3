using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RetireYoung3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GreetingPage : ContentPage
    {
       
        public GreetingPage()
        {
            InitializeComponent();
            Title = "RetireYoung";
            
            
        }
        

        //private async void OnNextPage_ClickedAsync(object sender, EventArgs e)
        //{
        //    //DisplayAlert("Under Construction", "Doesn't work yet", "Ok");
        //    //await Navigation.PushAsync(new GraphViewPage());


        //}
    }
}
