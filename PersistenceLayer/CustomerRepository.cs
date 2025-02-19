using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using LayeredArchitecture.Models;

namespace LayeredArchitecture.PersistenceLayer
    {
    public class CustomerRepository
        {
        private readonly CustomerContext _context;

        public CustomerRepository(CustomerContext context)
            {
            _context = context;
            }

        public void Insert(Customer customer)
            {
            _context.CustomersCollection.InsertOne(customer);
            }

        public List<Customer> GetAll()
            {
            return _context.CustomersCollection.Find(_ => true).ToList();
            }

        public Customer GetById(string id)
            {
            return _context.CustomersCollection.Find(c => c.Id == id).FirstOrDefault();
            }

        public void Update(Customer customer)
            {
            _context.CustomersCollection.ReplaceOne(c => c.Id == customer.Id, customer);
            }

        public void Delete(string id)
            {
            _context.CustomersCollection.DeleteOne(c => c.Id == id);
            }
        }
    }
