using ContacsAPI.Data;
using ContacsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContacsAPI.Controllers
{   // tell the controller that it is an API controller and not mvc
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {   // we can use the readonly dbContext to talk to our inmemory database
        private readonly ContactsAPIDbContext dbContext;
        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        { 
            return Ok(await dbContext.Contacts.ToListAsync()); 
        }
        

        [HttpPost]
        // in the below code, we have async because we have await in the await dbContext.Contacts.AddAsync(contact);
        // and because we have async, we have to have Task<>
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest) {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Adress = addContactRequest.Adress,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone
            };
            // here we are adding this object to the database, then save changes
            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpPut("{id:guid}")]
 
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if(contact != null)
            {
                contact.FullName= updateContactRequest.FullName;
                contact.Phone= updateContactRequest.Phone;
                contact.Email= updateContactRequest.Email;
                contact.Adress= updateContactRequest.Adress;

                await dbContext.SaveChangesAsync();
                return Ok(contact); 
            }
            return NotFound();
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if(id != null)
            {
                dbContext.Contacts.Remove(contact);
                dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        
    }
}
