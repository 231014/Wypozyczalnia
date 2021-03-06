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
    public partial class MovieModification : Form
    {
        Movie selectedMovie;
        Movie newMovie;
        bool firstLoad = true;

        public MovieModification(ref Movie movie)
        {
            InitializeComponent();
            selectedMovie = movie;
            ShallowCopyOfMovie();
            RefreshData();
        }

        private void ShallowCopyOfMovie()
        {
            newMovie = new Movie(selectedMovie.Name, selectedMovie.Price);
            foreach (Category cat in selectedMovie.Categories)
                newMovie.Categories.Add(cat);
        }

        private void RefreshData()
        {
            txtNazwa.Text = newMovie.Name;
            txtPrice.Text = newMovie.Price.ToString();
            listCategories.Items.Clear();
            foreach (Category cat in Data.Categories)
                listCategories.Items.Add(cat);
            foreach (Category cat in newMovie.Categories)
                listCategories.SetSelected(listCategories.Items.IndexOf(cat), true);
            firstLoad = !firstLoad;
        }

        private void listCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (firstLoad)
                return;

            newMovie.Categories.Clear();

            foreach (Category cat in listCategories.SelectedItems)
            {
                newMovie.Categories.Add(cat);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                selectedMovie.Price = decimal.Parse(txtPrice.Text);
            }
            catch (Exception)
            {
                txtPrice.Text = "Zła wartość!";
                return;
            }

            foreach (Category cat in Data.Categories)
                cat.ReturnListOfMovies().Remove(selectedMovie);

            selectedMovie.Name = txtNazwa.Text;
            selectedMovie.Categories.Clear();

            foreach (Category cat in newMovie.Categories)
                cat.AddMovie(selectedMovie);

            this.Close();
        }
    }
}
