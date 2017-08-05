using ChatLibrary;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {

        public ObservableCollection<Message> Messages { get; set; }
        public ObservableCollection<Contact> Contacts { get; set; }

        public Message Composed { get; set; }
        public ChatWindow(Contact self)
        {
            InitializeComponent();
            DataContext = this;
            Contacts = new ObservableCollection<Contact>();
            Composed = new Message();
            Composed.Sender = self;
            getContacts();


        }

        private void getContacts()
        {
            using (HttpClient c = new HttpClient())
            {
                var result = c.GetAsync("http://localhost:40433/api/contacts").Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                List<Contact> contacts = new List<Contact>( JsonConvert.DeserializeObject<IList<Contact>>(resultContent));
                Contacts.Clear();
                foreach (var contact in contacts)
                {
                    if (contact.Username != Composed.Sender.Username)
                        Contacts.Add(contact);
                }
            }
        }

        private void getMessagesBySenderRecevier(Contact sender, Contact receiver)
        {
            using (HttpClient c = new HttpClient())
            {
                var serialized = JsonConvert.SerializeObject(new { sender = sender.ID, receiver = receiver.ID });
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");

                var result = c.PostAsync("http://localhost:40433/api/messages",content).Result;
                if (result.IsSuccessStatusCode)
                {
                    string resultContent = result.Content.ReadAsStringAsync().Result;
                    List<Message> messages = new List<Message>(JsonConvert.DeserializeObject<IList<Message>>(resultContent));
                    Messages.Clear();
                    foreach (var message in messages)
                        Messages.Add(message);
                }
            }
        }

        private void btn2_send_Click(object sender, RoutedEventArgs e)
        {
            if (Composed == null || Composed.Sender == null || Composed.Receiver == null || string.IsNullOrEmpty(Composed.Body))
                return;
            Composed.Date = new System.DateTime();
            using (var c = new HttpClient())
            {
                var serialized = JsonConvert.SerializeObject(Composed);
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");
                var result = c.PutAsync("http://localhost:40433/api/messages", content).Result;
                if (result.IsSuccessStatusCode)
                {

                }
            }
        }
    }
}
