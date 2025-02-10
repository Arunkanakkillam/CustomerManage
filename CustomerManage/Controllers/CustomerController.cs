using CustomerManage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CustomerManage.Controllers
{
    public class CustomerController
    {
        private readonly CustomerDbContext _context;

        public CustomerController(CustomerDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.Include(c => c.EmploymentDetail).ToListAsync();
        }

    
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.Include(c => c.EmploymentDetail).FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
            {
                return new Customer();
            }

            return customer;
        }

    
        [HttpPost("addcustomer")]
        public async Task<ActionResult<string>> PostCustomer(CustomerDto customer)
        {
            if (customer==null)
            {
                return "please fill all the details";
            }

            _context.Customers.Add(new Customer
            {
                FullName = customer.FullName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Id = new int()
            });
            await _context.SaveChangesAsync();

            return $"new customer added fulll name is : {customer.FullName}";
        }

       
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return false;
            }

            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

       
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return false;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
