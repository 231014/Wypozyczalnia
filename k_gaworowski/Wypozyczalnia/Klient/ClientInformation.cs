﻿using System;
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
    public partial class ClientInformation : Form
    {
        private Client selectedClient;

        public ClientInformation(Client client)
        {
            InitializeComponent();
            selectedClient = client;
        }

        private void ClientInformation_Load(object sender, EventArgs e)
        {
            RefreshClientDetails();
            RefreshRentalList();
        }

        private void btnAddRental_Click(object sender, EventArgs e)
        {
            NewRental form = new NewRental(selectedClient);
            form.ShowDialog();
            RefreshRentalList();
        }

        private void btnRentalInformations_Click(object sender, EventArgs e)
        {
            Rental selectedRental;
            try
            {
                selectedRental = dataGridView1.SelectedRows[0].Cells["Rental"].Value as Rental;
            }
            catch (Exception)
            {
                return;
            }
            if (selectedRental != null)
            {
                RentalInformations form = new RentalInformations(selectedRental);
                form.ShowDialog();
            }
        }

        private void btnRemoveRental_Click(object sender, EventArgs e)
        {
            Rental selectedRental;
            try
            {
                selectedRental = dataGridView1.SelectedRows[0].Cells["Rental"].Value as Rental;
            }
            catch (Exception)
            {
                return;
            }
            if (selectedRental != null)
            {
                selectedClient.RentalList.Remove(selectedRental);
            }
            RefreshRentalList();
            RefreshClientDetails();
        }

        private void RefreshRentalList()
        {
            dataGridView1.Rows.Clear();
            foreach (Rental rental in selectedClient.RentalList)
            {
                DateTime rentDate = rental.RentDate;
                DateTime returnDate = rental.ReturnDate;
                int numberOfMovies = rental.MoviesList.Count;
                decimal price = rental.CalculatePrice();

                dataGridView1.Rows.Add(rentDate.ToShortDateString(), returnDate.ToShortDateString(), numberOfMovies, price, rental);
            }
        }

        private void RefreshClientDetails()
        {
            txtImie.Text = selectedClient.PersonalData.Name;
            txtNazwisko.Text = selectedClient.PersonalData.Surname;
            dateTimePicker1.Value = selectedClient.PersonalData.BirthDate;
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            RefreshClientDetails();
            RefreshRentalList();
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }
    }
}
