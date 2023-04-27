using MauiFinal.Models;

namespace MauiFinal;

public partial class MenuPage : ContentPage
{
    public MenuPage()
    {
        InitializeComponent();
        DBConnect _dbconnect = new DBConnect();
        var dishes = _dbconnect.PopulateDishes();
        dishesListView.ItemsSource = dishes;

    }

   

    private async void Update_Clicked(object sender, EventArgs e)
    {
        DBConnect _dbconnect = new DBConnect();

        var dish = new Dishes
        {
            Id = int.Parse(dishIdEntry.Text),
            Name = dishNameEntry.Text,
            Description = dishDescriptionEntry.Text,
            Price = decimal.Parse(dishPriceEntry.Text)
        };


        


        bool idMatch = _dbconnect.checkIdMatches(dish.Id);

        if (idMatch)
        {

            bool isUpdateConfirmed = await DisplayAlert("Confirmation", "Are you sure you want to update this dish?", "Yes", "No");

            if (isUpdateConfirmed)
            {
                try
                {
                    _dbconnect.UpdateDish(dish);
                    var UpdatedDishes = _dbconnect.PopulateDishes();
                    dishesListView.ItemsSource = UpdatedDishes;
                    await DisplayAlert("Success", "Dish updated successfully!", "OK");


                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"Failed to update dish. {ex.Message}", "OK");
                }

                
            }
        }
        else 
        { 
            await DisplayAlert("Error", "Id is not the same", "OK"); 
        }
    }







}