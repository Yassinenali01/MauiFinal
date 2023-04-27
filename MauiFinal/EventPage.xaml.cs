
using MauiFinal.Models;

namespace MauiFinal;

public partial class EventPage : ContentPage
{
	public EventPage()
	{
		InitializeComponent();
		DBConnect _dbconnect = new DBConnect();

		var events = _dbconnect.populateEvents();
        eventsListView.ItemsSource = events;


    }


	private async void OnDeleteClicked(object sender, EventArgs e)
	{



        int idToDelete = ((Events)((Button)sender).BindingContext).Id;

       


        DBConnect _dbconnect = new DBConnect();


        bool isDeleteConfirmed = await DisplayAlert("Confirmation", "Are you sure you want to delete this event?", "Yes", "No");

        if (isDeleteConfirmed)

        {
            try
            {
                _dbconnect.DeleteEvent(idToDelete);
                
                
                var events = _dbconnect.populateEvents();
                eventsListView.ItemsSource = events; 

                await DisplayAlert("Success", "Event Deleted", "Ok");
                
            }
            
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to delete. {ex.Message} Try again", "OK");
            }
        }

        else
        {
            await DisplayAlert("Alert", "Event Not Deleted", "OK");
        }
        

       

















    }


	







}