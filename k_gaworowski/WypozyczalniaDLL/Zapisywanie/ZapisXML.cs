﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace WypozyczalniaDane
{
    public class ZapisXML : ZapisInterfejs
    {
        private string sciezkapliku;
        private XDocument document;
        private XElement root;

        public XDocument Document
        {
            get { return document; }
        }
        //identyfikatory w xmlu
        //dictionary uzywa sie w celu przyporzadkowania par np czesc-hello, dwie zlaczone ze soba listy
        //dictionary przypisuje identyfikatory

        private Dictionary<int, Category> Categories;
        private Dictionary<int, Movie> Movies;
        private Dictionary<int, Rental> Rentals;
        private Dictionary<int, Client> Clients;
        private Dictionary<int, PersonalData> Personals;

        public ZapisXML(string sciezkapliku)
        {
            this.sciezkapliku = sciezkapliku;
            document = new XDocument();
            Categories = new Dictionary<int, Category>();
            Movies = new Dictionary<int, Movie>();
            Rentals = new Dictionary<int, Rental>();
            Clients = new Dictionary<int, Client>();
            Personals = new Dictionary<int, PersonalData>();
        }

        public bool Save(List<Client> clients, List<Movie> movies, List<Category> categories)
        {
            try
            {
                ResetDictionaries();
                CreateStructureOfXml();
                LoadCategoriesDictionary(categories);
                LoadMoviesDictionary(movies);
                LoadClientsDictionary(clients);
                foreach (Client cl in clients)
                {
                    Personals.Add(Personals.Keys.Count, cl.PersonalData);
                    LoadRentalsDictionary(cl.RentalList);
                }

                SavePersonalDatas();
                SaveMovies();
                SaveCategories();
                SaveRentals();
                SaveClients();

                document.Save(sciezkapliku);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool Load(out List<Client> clients, out List<Movie> movies, out List<Category> categories)
        {
            try
            {
                ResetDictionaries();
                LoadFile();
                LoadPersonals();
                LoadMovies();
                LoadCategories();
                LoadRentals();
                LoadClients();
            }
            catch (Exception)
            {
                throw;
            }
            clients = Clients.Values.ToList();
            movies = Movies.Values.ToList();
            categories = Categories.Values.ToList();
            return true;
        }
        //elementy xmla(etykietki)
        private void CreateStructureOfXml()
        {
            XElement root = new XElement("Wypozyczalnia");
            XElement movies = new XElement("Movies");
            XElement categories = new XElement("Categories");
            XElement rentals = new XElement("Rentals");
            XElement personalDatas = new XElement("PersonalDatas");
            XElement clients = new XElement("Clients");

            root.Add(movies);
            root.Add(categories);
            root.Add(rentals);
            root.Add(personalDatas);
            root.Add(clients);
            document.Add(root);
            this.root = document.Element("Wypozyczalnia");
        }
        private void LoadFile()
        {
            document = XDocument.Load(sciezkapliku);
            root = document.Element("Wypozyczalnia");
        }
        private void ResetDictionaries()
        {
            Categories.Clear();
            Movies.Clear();
            Rentals.Clear();
            Clients.Clear();
            Personals.Clear();
        }

        private void LoadCategoriesDictionary(List<Category> categories)
        {
            for (int i = 0; i < categories.Count; i++)//tworza slowniki z identyfikatorami(do kazdego obiektu przypisuje identyfikator) dla kazdej klasy osobno
                Categories.Add(i, categories[i]);
        }
        private void LoadMoviesDictionary(List<Movie> movies)
        {
            for (int i = 0; i < movies.Count; i++)
                Movies.Add(i, movies[i]);
        }
        private void LoadRentalsDictionary(List<Rental> rentals)
        {
            for (int i = 0; i < rentals.Count; i++)
                Rentals.Add(Rentals.Keys.Count, rentals[i]); //i + rentals.Keys.Count
        }
        private void LoadClientsDictionary(List<Client> clients)
        {
            for (int i = 0; i < clients.Count; i++)
                Clients.Add(i, clients[i]);
        }

        private void SavePersonalDatas() //tworzy strukturre xmla
        {
            foreach (KeyValuePair<int, PersonalData> pers in Personals)
            {
                XElement personalsDatas = root.Element("PersonalDatas");
                XElement singlePersonal = new XElement("Personal");

                XElement name = new XElement("Name");
                name.Value = pers.Value.Name;

                XElement surname = new XElement("Surname");
                surname.Value = pers.Value.Surname;

                XElement dateBirth = new XElement("BirthDate");
                //dateBirth.Value = pers.Value.BirthDate.ToString();
                dateBirth.Value = pers.Value.BirthDate.ToString("dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                singlePersonal.SetAttributeValue("id", pers.Key); //tworzy pojedynczego personala i na koncu przypisuje go jako obiekt do listy personali
                singlePersonal.Add(name);
                singlePersonal.Add(surname);
                singlePersonal.Add(dateBirth);

                personalsDatas.Add(singlePersonal);
            }
        }
        private void SaveMovies()
        {
            foreach (KeyValuePair<int, Movie> movie in Movies)
            {
                XElement moviesElement = root.Element("Movies");
                XElement singleMovie = new XElement("Movie");

                XElement name = new XElement("Name");
                name.Value = movie.Value.Name;

                XElement price = new XElement("Price");
                price.Value = movie.Value.Price.ToString(System.Globalization.CultureInfo.InvariantCulture);

                singleMovie.SetAttributeValue("id", movie.Key);
                singleMovie.Add(name);
                singleMovie.Add(price);

                moviesElement.Add(singleMovie);
            }
        }
        private void SaveCategories()
        {
            foreach (KeyValuePair<int, Category> cat in Categories)
            {
                XElement categoryElement = root.Element("Categories");
                XElement singleCategory = new XElement("Category");

                XElement name = new XElement("Name");
                name.Value = cat.Value.ReturnCategoryName();

                XElement movies = new XElement("Movies");

                foreach (Movie movie in cat.Value.ReturnListOfMovies())
                {
                    XElement movieElement = new XElement("Movie");
                    foreach (KeyValuePair<int, Movie> moviePair in Movies)
                    {
                        if (moviePair.Value == movie)
                        {
                            movieElement.Value = moviePair.Key.ToString();
                            break;
                        }
                    }
                    movies.Add(movieElement);
                }

                singleCategory.SetAttributeValue("id", cat.Key);
                singleCategory.Add(name);
                singleCategory.Add(movies);

                categoryElement.Add(singleCategory);
            }
        }
        private void SaveRentals()
        {
            foreach (KeyValuePair<int, Rental> rent in Rentals)
            {
                XElement rentalElement = root.Element("Rentals");
                XElement singleRental = new XElement("Rental");
                
                XElement rentDate = new XElement("RentDate");
                rentDate.Value = rent.Value.RentDate.ToString();

                XElement returnDate = new XElement("ReturnDate");
                returnDate.Value = rent.Value.ReturnDate.ToString();

                XElement movies = new XElement("Movies");

                foreach (Movie movie in rent.Value.MoviesList)
                {
                    XElement movieElement = new XElement("Movie");
                    foreach (KeyValuePair<int, Movie> moviePair in Movies)
                    {
                        if (moviePair.Value == movie)
                        {
                            movieElement.Value = moviePair.Key.ToString();
                            break;
                        }
                    }
                    movies.Add(movieElement);
                }

                singleRental.SetAttributeValue("id", rent.Key);
                singleRental.Add(rentDate);
                singleRental.Add(returnDate);
                singleRental.Add(movies);

                rentalElement.Add(singleRental);
            }
        }
        private void SaveClients()
        {
            foreach (KeyValuePair<int, Client> clientPair in Clients)
            {
                XElement clientsElement = root.Element("Clients");
                XElement singleClient = new XElement("Client");

                XElement personalData = new XElement("PersonalData");

                foreach (KeyValuePair<int, PersonalData> pers in Personals)
                {
                    if (pers.Value == clientPair.Value.PersonalData)
                    {
                        personalData.Value = pers.Key.ToString();
                        break;
                    }
                }

                XElement rentals = new XElement("Rentnals");

                foreach (Rental rental in clientPair.Value.RentalList)
                {
                    XElement rentalElement = new XElement("Rental");
                    foreach (KeyValuePair<int, Rental> rentalPair in Rentals)
                    {
                        if (rentalPair.Value == rental)
                        {
                            rentalElement.Value = rentalPair.Key.ToString();
                            break;
                        }
                    }
                    rentals.Add(rentalElement);
                }

                singleClient.SetAttributeValue("id", clientPair.Key);
                singleClient.Add(personalData);
                singleClient.Add(rentals);

                clientsElement.Add(singleClient);
            }
        }

        private void LoadMovies() //zeby wczytac filmy, zrobiłem liste filmow w roocie
        {
            XElement moviesElement = root.Element("Movies");

            foreach (XElement singleMovie in moviesElement.Elements())
            {
                int id = int.Parse(singleMovie.Attribute("id").Value);//sczytywanie z pliku xml
                string name = singleMovie.Element("Name").Value;
                decimal price = decimal.Parse(singleMovie.Element("Price").Value, System.Globalization.CultureInfo.InvariantCulture);
                Movie newMovie = new Movie(name, price);//odtwarza


                Movies.Add(id, newMovie);
            }
        }
        private void LoadPersonals()
        {
            XElement personalsElement = root.Element("PersonalDatas");

            foreach (XElement singlePersonal in personalsElement.Elements())
            {
                int id = int.Parse(singlePersonal.Attribute("id").Value);
                string name = singlePersonal.Element("Name").Value;
                string surname = singlePersonal.Element("Surname").Value;
                DateTime birthDate = DateTime.ParseExact(singlePersonal.Element("BirthDate").Value, "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                //DateTime birthDate = DateTime.Parse(singlePersonal.Element("BirthDate").Value, System.Globalization.CultureInfo.InvariantCulture);
                PersonalData newPersonal = new PersonalData(name, surname, birthDate);

                Personals.Add(id, newPersonal);
            }
        }
        private void LoadCategories()
        {
            XElement categoriesElement = root.Element("Categories");

            foreach (XElement singleCategory in categoriesElement.Elements())
            {
                int id = int.Parse(singleCategory.Attribute("id").Value);
                string name = singleCategory.Element("Name").Value;
                Category category = null;
                switch (name)
                {
                    case "Child":   category = CategoryChild.Instance; break;
                    case "Normal": category = CategoryNormal.Instance; break;
                    case "New": category = CategoryNew.Instance; break;
                    case "Western": category = CategoryWestern.Instance; break;
                }


                category.ReturnListOfMovies().Clear();

                foreach (XElement movie in singleCategory.Element("Movies").Elements())
                {
                    int movieId = int.Parse(movie.Value);
                    Movie foundMovie = Movies[movieId];
                    category.AddMovie(foundMovie);
                }

                Categories.Add(id, category);
            }
        }
        private void LoadRentals()
        {
            XElement rentalsElement = root.Element("Rentals");

            foreach (XElement singleRental in rentalsElement.Elements())
            {
                int id = int.Parse(singleRental.Attribute("id").Value);
                DateTime rentDate = DateTime.ParseExact(singleRental.Element("RentDate").Value, "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                DateTime returnDate = DateTime.ParseExact(singleRental.Element("ReturnDate").Value, "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);


                Rental rental = new Rental(rentDate, returnDate);

                foreach (XElement movie in singleRental.Element("Movies").Elements())
                {
                    int movieId = int.Parse(movie.Value);
                    Movie foundMovie = Movies[movieId];
                    rental.MoviesList.Add(foundMovie);
                }

                Rentals.Add(id, rental);
            }
        }
        private void LoadClients()
        {
            XElement clientsElement = root.Element("Clients");

            foreach (XElement singleClient in clientsElement.Elements())
            {
                int id = int.Parse(singleClient.Attribute("id").Value);
                int personalId = int.Parse(singleClient.Element("PersonalData").Value);

                Client client = new Client(Personals[personalId]);

                foreach (XElement rental in singleClient.Element("Rentnals").Elements())
                {
                    int rentalId = int.Parse(rental.Value);
                    Rental foundRental = Rentals[rentalId];
                    client.RentalList.Add(foundRental);
                }

                Clients.Add(id, client);
            }
        }
    }
}
