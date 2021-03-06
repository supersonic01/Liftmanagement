﻿using Liftmanagement.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liftmanagement.Data;
using Liftmanagement.Models;

namespace Liftmanagement.ViewModels
{
    public class CustomerViewModel : ViewModel
    {
        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { SetField(ref myVar, value); }
        }


        private List<LocationDetailView> locationDetailViews = new List<LocationDetailView>();

        public List<LocationDetailView> LocationDetailViews
        {
            get { return locationDetailViews; }
            set { SetField(ref locationDetailViews, value); }
        }

        private Customer customerSelected = new Customer();

        public Customer CustomerSelected
        {
            get { return customerSelected; }
            set
            {
                SetField(ref customerSelected, value);
            }
        }

        public Customer CustomerSelectedLast { get; set; }

        private ContactPartner administratorContactPerson = new ContactPartner();

        public ContactPartner AdministratorContactPerson
        {
            get { return administratorContactPerson; }
            set { SetField(ref administratorContactPerson, value); }
        }

        public SQLQueryResult<Customer> Add()
        {
            customerSelected.ReadOnly = false;
            if (CustomerSelected.Id > 0)
            {
                return MySQLDataAccess.UpdateCustomer(CustomerSelected);
            }
            else
            {
                return MySQLDataAccess.AddCustomer(CustomerSelected);
            }

        }

        public SQLQueryResult<Customer> EditCustomer()
        {
          var result= MySQLDataAccess.GetCustomerForEdit(CustomerSelected.Id);
          if (!result.IsReadOnly)
          {
              CustomerSelected = result.DBRecords.FirstOrDefault() as Customer;
          }

          return result;
        }


        public SQLQueryResult<Customer> ForceEditing()
        {
            return MySQLDataAccess.ForceToEditCustomer(CustomerSelected.Id);
        }


        public void ReleaseEditing()
        {
            Task.Factory.StartNew(() =>
            {
                MySQLDataAccess.ReleaseEditingCustomer(CustomerSelected.Id);
            });
        }

        public SQLQueryResult<Customer> MarkForDeleteCustomer()
        {
            return MySQLDataAccess.MarkForDeleteCustomer(CustomerSelected);
        }
    }
}
