using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WypozyczalniaDane
{
    public class Client
    {
        private PersonalData personalData;
        private List<Rental> rentalList;

        public List<Rental> RentalList
        {
            get { return rentalList; }
        }

        public PersonalData PersonalData
        {
            get { return personalData; }
        }

        public Client(PersonalData personalData)
        {
            this.personalData = personalData;
            rentalList = new List<Rental>();
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", personalData.Name, personalData.Surname);
        }
    }
}
