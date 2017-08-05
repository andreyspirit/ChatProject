using System;
using System.Data.Entity;

namespace ChatLibrary
{
    public class ChatContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
    public class Message
    {
        public Message() {  }
        public int ID { get; set; }
        public virtual Contact Sender { get; set; }
        public virtual Contact Receiver { get; set; }
        public DateTime Date { get; set; }
        public string Body { get; set; }
    }


    public class Contact
    {
        public Contact() { } 
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

    }
 
}

