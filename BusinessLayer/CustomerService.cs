using System;
using System.Collections.Generic;
using LayeredArchitecture.PersistenceLayer;
using LayeredArchitecture.Models;

namespace LayeredArchitecture.BusinessLayer.Services
    {
    public class CustomerService
        {
        private readonly CustomerRepository _repo;

        public CustomerService(CustomerRepository repo)
            {
            _repo = repo;
            }

        public void CreateCustomer(Customer customer)
            {
            if (string.IsNullOrWhiteSpace(customer.Name))
                throw new ArgumentException("Name cannot be empty!");
            if (string.IsNullOrWhiteSpace(customer.Email))
                throw new ArgumentException("Email cannot be empty!");

            _repo.Insert(customer);
            }

        public List<Customer> GetAllCustomers()
            {
            return _repo.GetAll();
            }

        public Customer GetCustomerById(string id)
            {
            return _repo.GetById(id);
            }

        public void UpdateCustomer(Customer customer)
            {
            _repo.Update(customer);
            }

        public void DeleteCustomer(string id)
            {
            _repo.Delete(id);
            }
        }
    }
