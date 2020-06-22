using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WypozyczalniaDane;

namespace Wypozyczalnia
{
    public partial class NewRental : Form
    {
        Client selectedClient;
        Rental rental;

        public NewRental(Client data)
        {
            InitializeComponent();
            selectedClient = data;
            rental = new Rental(DateTime.Today, 1);
            LoadClientData();
            RefreshMovieList();
        }

        private void RefreshMovieList()
        {
            listMovies.Items.Clear();

            foreach (Movie movie in Data.Movies)
            {
                listMovies.Items.Add(movie);
            }
        }

        private void DateChange(object sender, EventArgs e)
        {
            rental.RentDate = dateTimePickerRentDate.Value.Date;
            rental.ReturnDate = dateTimePickerReturnDate.Value.Date;

            txtPrice.Text = rental.CalculatePrice().ToString();
            txtDays.Text = rental.CalculateDays().ToString();
        }


        private void LoadClientData()
        {
            txtImie.Text = selectedClient.PersonalData.Name;
            txtNazwisko.Text = selectedClient.PersonalData.Surname;
        }

        private void listMovies_SelectedIndexChanged(object sender, EventArgs e)
        {
            rental.MoviesList.Clear();
            foreach (Movie movie in listMovies.SelectedItems)
            {
                rental.MoviesList.Add(movie);
            }
            txtMovies.Text = listMovies.SelectedItems.Count.ToString();
            txtPrice.Text = rental.CalculatePrice().ToString();
        }

        private void btnAddRental_Click(object sender, EventArgs e)
        {
            if (rental.ReturnDate < rental.RentDate)
            {
                txtDays.Text = "Zła wartość!";
                return;
            }

            selectedClient.RentalList.Add(rental);
            this.Close();
        }

        private void TxtPrice_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
