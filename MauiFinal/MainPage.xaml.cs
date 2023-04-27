using MauiFinal.Models;

namespace MauiFinal;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
        DBConnect _dbconnect = new DBConnect();
        InitializeComponent();

        var employees = _dbconnect.PopulateEmployee();
        employeeList.ItemsSource = employees;
    }


   

    private async void OnAddClicked(object sender, EventArgs e)
    {
        DBConnect _dbconnect = new DBConnect();
        

        var employee = new Employee
        {
           Id = int.Parse(employeeIdEntry.Text),
           Name = employeeNameEntry.Text,
           Email = employeeEmailEntry.Text,
        };
        
        _dbconnect.InsertEmployee(employee);
        
        bool isAddConfirmed = await DisplayAlert("Confirmation", "Are you sure you want to add an employee ?", "Yes", "No");
        if (isAddConfirmed)
        {
            var employees = _dbconnect.PopulateEmployee();
            employeeList.ItemsSource = employees;
            await DisplayAlert("Success", "Employee added successfully!", "OK");
        }

        else
        {
            await DisplayAlert("Unseccess", $"employee was not added.", "OK");
        }
    }


}

