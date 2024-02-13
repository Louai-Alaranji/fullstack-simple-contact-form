namespace ContacsAPI.Models
{
    public class AddContactRequest
    {   // we want from the user only these, we dont want the user to give us an ID
        // which is why we have this class
        public string FullName { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Adress { get; set; }
    }
}
