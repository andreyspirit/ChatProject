﻿using ChatLibrary;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
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

        public Contact Self { get; set; }
        public Contact Receiver { get; set; }
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
                var result = c.GetAsync("http://localhost:40433/api/contact").Result;
                string resultContent = result.Content.ReadAsStringAsync().Result;
                List<Contact> contacts = new List<Contact>( JsonConvert.DeserializeObject<IList<Contact>>(resultContent));
                Contacts.Clear();
                foreach (var contact in contacts)
                {
                    if (contact.Username != Self.Username)
                        Contacts.Add(contact);
                }
            }
        }

        private void btn2_send_Click(object sender, RoutedEventArgs e)
        {
            using (var c = new HttpClient())
            {
                ////var serialized = JsonConvert.SerializeObject(new { username = Username, password = txt1_password.Password });
                ////var content = new StringContent(serialized, Encoding.UTF8, "application/json");

                //Message msg = new Message();
                //msg.Body = "body";
                //msg.Date = new DateTime();
                //msg.Sender = 
                //var result = c.PostAsync("http://localhost:40433/api/send", ).Result;
                //if (result.IsSuccessStatusCode)
                //    MessageBox.Show("You are now logged in!");
                //else
                //{
                //    MessageBox.Show("Login failed!");
                //    return;
                //}
            }
        }
    }
}
