using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaDLL
{
    public class PersonalData
    {
        private string name;
        private string surname;
        private DateTime birthDate;

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        //konstruktor mowi ze ta funkcja przyjmuje dane w nawiasie i mowi ze danej osobie przypisuje odpowiednie dane
        public PersonalData(string name, string surname, DateTime birthDate) //zeby stworzyc obiekt 
        {
            this.name = name;
            this.surname = surname;
            this.birthDate = birthDate;
        }

        public override string ToString()
        {
            return String.Format("Imie: {0}, Nazwisko: {1}, Data Urodzenia: {2}", name, surname, birthDate.ToShortDateString());// 0 1 2 w nawiasach oznacza ze w to miejsce podstawia sie wartosc
        }    
    }
}
