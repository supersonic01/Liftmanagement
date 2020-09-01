using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Helper
{
    public static class TestData
    {

        public static List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer
            {
                CustomerId = 1,
                CompanyName = "SEMEX-EngCon GmbH",
                ContactPerson = "Steffan Winterhut",
                Address = "Carl-Metz-Straße 26",
                Postcode = "76275",
                City = "Ettlingen",
                PhoneWork = "072435148252",
                Mobile = "0172435148252"
            });

            customers.Add(new Customer
            {
                CustomerId = 2,
                CompanyName = "Sit SteuerungsTechnik GmbH",
                ContactPerson = "Dieter Fischer",
                Address = "Einsteinstraße 26-32",
                Postcode = "76275",
                City = "Ettlingen",
                PhoneWork = "07243561710",
                Mobile = "017243561710"
            });

            customers.Add(new Customer
            {
                CustomerId = 3,
                CompanyName = "ISCAR Germany GmbH",
                ContactPerson = "Hans Hahn",
                Address = "Eisenstockstraße 14",
                Postcode = "76275",
                City = "Ettlingen",
                PhoneWork = "0724399080",
                Mobile = "01724399080"
            });
            customers.Add(new Customer
            {
                CustomerId = 4,
                CompanyName = "VBE Kamm GmbH",
                ContactPerson = "Josef Jäger",
                Address = "Am Erlengraben 2",
                Postcode = "76275",
                City = "Ettlingen",
                PhoneWork = "0724357770",
                Mobile = "01724357770"
            });


            return customers;
        }

        public static List<Location> GetLocations()
        {
            List<Location> locations = new List<Location>();
            locations.Add(new Location
            {
                CustomerId = 1,
                LocationId = 1,
                ContactPerson = "Anna Cremer",
                Address = "Carl-Metz-Straße 26",
                Postcode = "76275",
                City = "Ettlingen",
                PhoneWork = "072435148252",
                Mobile = "0172435148252",
                AdditionalInfo = "Bitte alle Bewohner informieren, wenn Aufzug gewartete wird"
            });


            locations.Add(new Location
            {
                CustomerId = 2,
                LocationId = 2,
                ContactPerson = "Brigitte Esser",
                Address = "Einsteinstraße 26-32",
                Postcode = "76275",
                City = "Ettlingen",
                PhoneWork = "07243561710",
                Mobile = "017243561710",
                AdditionalInfo = "Code für Eingang = 2585"
            });

            locations.Add(new Location
            {
                CustomerId = 4,
                LocationId = 4,
                ContactPerson = "Elisabeth Hoffmann",
                Address = "Am Erlengraben 2",
                Postcode = "76275",
                City = "Ettlingen",
                PhoneWork = "0724357770",
                Mobile = "01724357770",
                AdditionalInfo = "Bewohner im 3. Stock (Herr Walter) informieren"
            });

            locations.Add(new Location
            {
                CustomerId = 4,
                LocationId = 5,
                ContactPerson = "Manfred Becker",
                Address = "Amselweg 2",
                Postcode = "76467",
                City = "Bietigheim",
                PhoneWork = "0724357770",
                Mobile = "01724357770",
                AdditionalInfo = "Kellereingang ist ums Haus"
            });



            return locations;
        }
    }
}