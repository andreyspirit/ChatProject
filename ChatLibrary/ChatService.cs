
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChatLibrary
{
    public class ChatService
    {
        static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public void addNewContact(Contact contact)
        {
            using (var db = new ChatContext())
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
            }
        }

        public void removeContact(Contact contact)
        {
            using (var db = new ChatContext())
            {
                db.Contacts.Attach(contact);
                db.Contacts.Remove(contact);
                db.SaveChanges();
            }
        }

        public bool contactExists(Contact contact)
        {
            using (var db = new ChatContext())
            {
                if (db.Contacts.Any(c => c.ID == contact.ID))
                {
                    return true;
                }
            }
            return false;
        }

        public bool contactPassword(string username, string password)
        {
            Contact desired;
            using (var db = new ChatContext())
            {
                desired = db.Contacts.SingleOrDefault(c => c.Username == username);
            }
            if (desired == null)
                return false;
            if (desired.Password != password)
                return false;
            return true;
        }

        public void addNewMessage(Message message)
        {
            using (var db = new ChatContext())
            {
                db.Messages.Attach(message);
                db.Messages.Add(message);
                db.SaveChanges();
            }
        }

        public void removeMessage(Message message)
        {
            using (var db = new ChatContext())
            {
                db.Messages.Attach(message);
                db.Messages.Remove(message);
                db.SaveChanges();
            }
        }

        public IList<Contact> getContacts()
        {
            using (var db = new ChatContext())
            {
                return new List<Contact>(db.Contacts);
            }
        }

        public Contact getContactByUsername(string username)
        {
            using (var db = new ChatContext())
            {
                return db.Contacts.SingleOrDefault(c => c.Username == username);
            }
        }

        public IList<Message> getMessagesBySenderReceiver(int sender_id,int receiver_id)
        {
            using (var db = new ChatContext())
            {
                return new List<Message>(db.Messages.
                    Where(c => c.Sender.ID == sender_id && c.Receiver.ID == receiver_id));
            }
        }

        public IList<Message> getMessages()
        {
            using (var db = new ChatContext())
            {
                return new List<Message>(db.Messages);
            }
        }
    }
}