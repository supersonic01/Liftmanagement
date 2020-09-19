﻿using Google.Protobuf.Reflection;
using Liftmanagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Liftmanagement.Helper
{
    public static class TestData
    {

        public static List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();

            var customer = new Customer
            {
                CompanyName = "SEMEX-EngCon GmbH",
                Address = "Carl-Metz-Straße 26",
                Postcode = "76275",
                City = "Ettlingen",
                AdditionalInfo = "Keine Info vorhanden, bitte was schreiben",
                GoogleDriveFolderName = "GoogleFolder",
                GoogleDriveLink = "https://dict.leo.org/",
                CreatedPersonName = "ich",
                ModifiedPersonName = "Du",
                ReadOnly = true,
                UsedBy = "ich weiß nicht"
            };

            customer.ContactPerson = new ContactPartner(customer) { Name = "Steffan Winterhut", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" };
            customer.Administrator = new AdministratorCompany(customer)
            {
                Name = "Stahl Immobilien"
            };

            customer.Administrator.ContactPersons = new List<ContactPartner> {
                    new ContactPartner (customer.Administrator){ Name = "Steffan0 Winterhut", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" },
                    new ContactPartner(customer.Administrator) { Name = "Steffan1 Winterhut", PhoneWork = "07243514825255", Mobile = "0172435148252", EMail = "halo@web.de" },
                    new ContactPartner(customer.Administrator) { Name = "Steffan2 Winterhut", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" }};


            customers.Add(customer);
            
            customer = new Customer
            {
                CompanyName = "Sit SteuerungsTechnik GmbH",
                Address = "Einsteinstraße 26-32",
                Postcode = "76275",
                City = "Ettlingen",
                AdditionalInfo = "Keine Info vorhanden, bitte was schreiben",
                GoogleDriveFolderName = "GoogleFolder",
                GoogleDriveLink = "https://dict.leo.org/",
                CreatedPersonName = "ich",
                ModifiedPersonName = "Du",
                ReadOnly = true,
                UsedBy = "ich weiß nicht"
            };

            customer.ContactPerson = new ContactPartner(customer) { Name = "Dieter Fischer", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" };
            customer.Administrator = new AdministratorCompany(customer)
            {
                Name = "PODOMO"
            };

            customer.Administrator.ContactPersons = new List<ContactPartner> {
                new ContactPartner (customer.Administrator){ Name = "Dieter Fischer1", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" },
                new ContactPartner(customer.Administrator) { Name = "Dieter Fischer2", PhoneWork = "07243514825255", Mobile = "0172435148252", EMail = "halo@web.de" },
                new ContactPartner(customer.Administrator) { Name = "Dieter Fischer3", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" }};


            customers.Add(customer);

            customer = new Customer
            {
                CompanyName = "ISCAR Germany GmbH",
                Address = "Eisenstockstraße 14",
                Postcode = "76275",
                City = "Ettlingen",
                AdditionalInfo = "Keine Info vorhanden, bitte was schreiben",
                GoogleDriveFolderName = "GoogleFolder",
                GoogleDriveLink = "https://dict.leo.org/",
                CreatedPersonName = "ich",
                ModifiedPersonName = "Du",
                ReadOnly = true,
                UsedBy = "ich weiß nicht"
            };

            customer.ContactPerson = new ContactPartner(customer) { Name = "Hans Hahn", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" };
            customer.Administrator = new AdministratorCompany(customer)
            {
                Name = "DOMICILIA"
            };

            customer.Administrator.ContactPersons = new List<ContactPartner> {
                new ContactPartner (customer.Administrator){ Name = "Hans Hahn1", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" },
                new ContactPartner(customer.Administrator) { Name = "Hans Hahn2", PhoneWork = "07243514825255", Mobile = "0172435148252", EMail = "halo@web.de" },
                new ContactPartner(customer.Administrator) { Name = "Hans Hahn3", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" },
                new ContactPartner(customer.Administrator) { Name = "Hans Hahn4", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" }};
    

            customers.Add(customer);


            customer = new Customer
            {
                CompanyName = "VBE Kamm GmbH",
                Address = "Am Erlengraben 2",
                Postcode = "76275",
                AdditionalInfo = "Keine Info vorhanden, bitte was schreiben",
                GoogleDriveFolderName = "GoogleFolder",
                GoogleDriveLink = "https://dict.leo.org/",
                CreatedPersonName = "ich",
                ModifiedPersonName = "Du",
                ReadOnly = true,
                UsedBy = "ich weiß nicht"
            };

            customer.ContactPerson = new ContactPartner(customer) { Name = "Josef Jäger", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" };
            customer.Administrator = new AdministratorCompany(customer)
            {
                Name = "FONCIA"
            };

            customer.Administrator.ContactPersons = new List<ContactPartner> {
                new ContactPartner (customer.Administrator){ Name = "Josef Jäger1", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" },
                new ContactPartner(customer.Administrator) { Name = "Josef Jäger2", PhoneWork = "07243514825255", Mobile = "0172435148252", EMail = "halo@web.de" },
                new ContactPartner(customer.Administrator) { Name = "Josef Jäger3", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" },
                new ContactPartner (customer.Administrator){ Name = "Josef Jäger4", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" },
                new ContactPartner(customer.Administrator) { Name = "Josef Jäger5", PhoneWork = "07243514825255", Mobile = "0172435148252", EMail = "halo@web.de" },
                new ContactPartner(customer.Administrator) { Name = "Josef Jäger6", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" }
            };


            customers.Add(customer);


            return customers;
        }

        internal static List<MachineInformation> GetMachineInformations()
        {
            List<MachineInformation> machineInformations = new List<MachineInformation>();

            var machine = new MachineInformation
            {
                CustomerId = -1,
                LocationId = -1,
                Name = "SHERPA",
                YearOfConstruction = new DateTime(2002, 09, 11),
                SerialNumber = "224",
                HoldingPositions = 6,
                Entrances = 6,
                Description = "Gütern bis zu 3.000 kg. Mit freistehenden Schachtkonstruktionen und Regalsysteme",
                Payload = 400,
                AdditionalInfo = "Keine Info vorhanden, bitte was schreiben",

            };

            machine.ContactPerson = new ContactPartner(machine) { Name = "Steffan2 Winterhut2", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" };
            machineInformations.Add(machine);

            //machineInformations.Add(new MachineInformation
            //{
            //    CustomerId = 2,
            //    LocationId = 2,
            //    Id = 2,
            //    Name = "ESCORTA",
            //    YearOfConstruction = new DateTime(2012, 02, 12),
            //    SerialNumber = "244",
            //    HoldingPositions = 10,
            //    Entrances = 10,
            //    Description = "Gütern bis zu 3.000 kg, bis zu 18 Meter Höhe. Mit freistehenden Schachtkonstruktionen, Regalsysteme und Doppelkreisbremssystem",
            //});
            //machineInformations.Add(new MachineInformation
            //{
            //    CustomerId = 3,
            //    LocationId = 3,
            //    Id = 3,
            //    Name = "ELEGANCA",
            //    YearOfConstruction = new DateTime(2018, 06, 25),
            //    SerialNumber = "264",
            //    HoldingPositions = 12,
            //    Entrances = 12,
            //    Description = "Geräuscharm (konform zur VDI 2566-2),Energieklasse A, Max. Personen 8, Max. Förderhöhe 	45 m,Nutzlast 1.000 kg, Max. Anzahl Haltestellen 21",
            //});
            //machineInformations.Add(new MachineInformation
            //{
            //    CustomerId = 4,
            //    LocationId = 4,
            //    Id = 4,
            //    Name = "VERTIC",
            //    YearOfConstruction = new DateTime(1996, 08, 09),
            //    SerialNumber = "284",
            //    HoldingPositions = 4,
            //    Entrances = 4,
            //    Description = "Max. Tragfähigkeit 500kg, Plattformmaße(B x L) 1000 x1500mm, Schachtmaße(B x L) 1360x1520, 	Einbaumaße (B x L) 1400 x 1630, Türbreite 900 m",
            //});
            //machineInformations.Add(new MachineInformation
            //{
            //    CustomerId = 4,
            //    LocationId = 5,
            //    Id = 5,
            //    Name = "PLANTINO",
            //    YearOfConstruction = new DateTime(2020, 02, 05),
            //    SerialNumber = "314",
            //    HoldingPositions = 4,
            //    Entrances = 4,
            //    Description = "Tragfähigkeit 225/300 kg,Nenngeschwindigkeit 0,11 m/s,Neigungswinkel 0 - 47 Grad,Anzahl Haltestellen mehrere Geschosse möglich,Plattformbreite 800 mm, Plattformtiefe 	800 / 900 / 1000 mm,Platzbedarf (eingeklappt) 	250 mm,Anschlußspannung 	230 V (16 A),Akkubetrieb DC 24 Volts (4 wartungsfreie Akkus),Schallpegel 63 db ",
            //});
            //machineInformations.Add(new MachineInformation
            //{
            //    CustomerId = 4,
            //    LocationId = 5,
            //    Id = 6,
            //    Name = "PEGASOS",
            //    YearOfConstruction = new DateTime(2000, 02, 06),
            //    SerialNumber = "324",
            //    HoldingPositions = 8,
            //    Entrances = 8,
            //    Description = "Auto Lift,Förderhöhe max. 80 m, Nenngeschwindigkeit 0,15 m/s,Grubentiefe 150 mm,Nennlast 2.700 – 3.100 kg, Kabinenbreite 2.500 – 3.000 mm,Kabinentiefe 5.500 – 6.000 mm,Kabinenhöhe 2.100 mm ",
            //});
            return machineInformations;
        }

        public static List<Location> GetLocations()
        {
            List<Location> locations = new List<Location>();

            var location = new Location
            {
                CustomerId = -1,
                Id = 1,
                Address = "Carl-Metz-Straße 26",
                Postcode = "76275",
                City = "Ettlingen",
                AdditionalInfo = "Bitte alle Bewohner informieren, wenn Aufzug gewartete wird",
                ContactByDefect = true

            };

            location.ContactPerson = new ContactPartner(location) { Name = "Anna Cremer", PhoneWork = "072435148252", Mobile = "0172435148252", EMail = "halo@web.de" };
            locations.Add(location);

            /*
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

            */

            return locations;
        }

        public static List<MaintenanceAgreement> GetMaintenanceAgreements()
        {
            List<MaintenanceAgreement> maintenanceAgreement = new List<MaintenanceAgreement>();
            maintenanceAgreement.Add(new MaintenanceAgreement
            {
                CustomerId = -1,
                LocationId = -1,
                MachineInformationId = -1,
                Duration = new DateTime(2020, 12, 31),
                CanBeCancelled = "jährlich",
                ArreementCancelledBy = "Kunde",
                MaintenanceType = "Vollwartung",
                NoticeOfPeriod = 3,
                AgreementDate = new DateTime(2019, 12, 31),
                AdditionalInfo = "Türe-Türteile"
            });

            /*  maintenanceAgreement.Add(new MaintenanceAgreement
              {
                  CustomerId = 2,
                  LocationId = 2,
                  MachineInformationId = 2,
                  Id = 2,
                  Duration = new DateTime(2020, 12, 31),
                  AgreementDate = new DateTime(2019, 12, 31),
                  AdditionalInfo = "Türe-Türteile"
              });

              maintenanceAgreement.Add(new MaintenanceAgreement
              {
                  CustomerId = 3,
                  LocationId = 3,
                  MachineInformationId = 3,
                  Id = 3,
                  Duration = new DateTime(2020, 12, 31),
                  AgreementDate = new DateTime(2019, 12, 31),
                  AdditionalInfo = "Türe-Türteile"
              });

              maintenanceAgreement.Add(new MaintenanceAgreement
              {
                  CustomerId = 4,
                  LocationId = 4,
                  MachineInformationId = 4,
                  Id = 4,
                  Duration = new DateTime(2020, 12, 31),
                  AgreementDate = new DateTime(2019, 12, 31),
                  AdditionalInfo = "Türe-Türteile"
              });

              maintenanceAgreement.Add(new MaintenanceAgreement
              {
                  CustomerId = 4,
                  LocationId = 5,
                  MachineInformationId = 5,
                  Id = 4,
                  Duration = new DateTime(2020, 12, 31),
                  AgreementDate = new DateTime(2019, 12, 31),
                  AdditionalInfo = "Türe-Türteile"
              });

              maintenanceAgreement.Add(new MaintenanceAgreement
              {
                  CustomerId = 4,
                  LocationId = 5,
                  MachineInformationId = 6,
                  Id = 4,
                  Duration = new DateTime(2020, 12, 31),
                  AgreementDate = new DateTime(2019, 12, 31),
                  AdditionalInfo = "Türe-Türteile"
              });
             */
            return maintenanceAgreement;
        }

        public static ObservableCollection<OtherInformation> GetOtherInformations()
        {
            ObservableCollection<OtherInformation> oi = new ObservableCollection<OtherInformation>();

            oi.Add(new OtherInformation("Mit Auftragnehmer vereinbart: Aufträge bis 100 EUR netto können"));
            oi.Add(new OtherInformation("text"));
            return oi;
        }
    }
}