using CustomerManage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly CustomerDbContext _context;

    public CustomerController(CustomerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
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
            return NotFound();
        }

        return customer;
    }
    [HttpPost]
    public async Task<ActionResult<Customer>> PostCustomer(CustomerDto customerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var customer = new Customer
        {
            FullName = customerDto.FullName,
            Email = customerDto.Email,
            PhoneNumber = customerDto.PhoneNumber,
            DateOfBirth = customerDto.DateOfBirth,
            EmploymentDetail = customerDto.EmploymentDetail != null ? new EmploymentDetail
            {
                CompanyName = customerDto.EmploymentDetail.CompanyName,
                JobTitle = customerDto.EmploymentDetail.JobTitle,
                Salary = customerDto.EmploymentDetail.Salary,
                CustomerId = customerDto.Id
            } : null
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, Customer customer)
    {
        if (id != customer.Id)
        {
            return BadRequest();
        }

        _context.Entry(customer).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
