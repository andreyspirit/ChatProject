
using ChatLibrary;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        const bool REGVIEW = false;
        const bool LOGVIEW = true;
        const bool DEFAULTVIEW = LOGVIEW;

        Contact self;

        public string Username { get; set; }
        public string Fullname { get; set; } 
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = this;
            SizeToContent = SizeToContent.WidthAndHeight;
            changeView(DEFAULTVIEW);
            setClickables();
            //using (var c = new HttpClient())
            //{
            //    var result = c.GetAsync("http://localhost:40433/api/cookie").Result;
            //    if (!result.IsSuccessStatusCode)
            //        return;
            //    string resultContent = result.Content.ReadAsStringAsync().Result;
            //    dynamic response = JsonConvert.DeserializeObject(resultContent);
            //    Username = response.username;
            //    txt1_password.Password = response.password;
            //    PerformClick(btn_login);
            //}
        }

        private void changeView(bool valview)
        {
            if (valview.Equals(REGVIEW))
            {
                txt1_fullname.Visibility = Visibility.Visible;
                btn_register.Visibility = Visibility.Visible;
                btn_login.Visibility = Visibility.Hidden;
                txt1_register.Visibility = Visibility.Hidden;
                txt1_login.Visibility = Visibility.Visible;

            }
            else
            {
                txt1_fullname.Visibility = Visibility.Hidden;
                btn_register.Visibility = Visibility.Hidden;
                btn_login.Visibility = Visibility.Visible;
                txt1_register.Visibility = Visibility.Visible;
                txt1_login.Visibility = Visibility.Hidden;
            }
        }

        public static void PerformClick(Button btnObject)

        {
            btnObject.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent,btnObject));
        }

        private void setClickables()
        {
            txt1_register.PreviewMouseDown += setViewREG;
            txt1_login.PreviewMouseDown += setViewLOG;
        }

        private void setViewREG(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            changeView(REGVIEW);
        }
        private void setViewLOG(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            changeView(LOGVIEW);
        }

        private void openChatDialog()
        {
            ChatWindow window;
            window = new ChatWindow(self);
            bool? nu = window.ShowDialog();
            if (nu ?? true)
            {

            }
        }

        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            using (var c = new HttpClient())
            {
                var serialized = JsonConvert.SerializeObject(new { username = Username, password = txt1_password.Password, fullname = Fullname });
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");

                var result = c.PostAsync("http://localhost:40433/api/registration", content).Result;
                if (!result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Registration failed!");
                    return;
                }
            }
            using (var c = new HttpClient())
            {
                var serialized = JsonConvert.SerializeObject(new { username = Username, password = txt1_password.Password, fullname = Fullname });
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");
                var result = c.PostAsync("http://localhost:40433/api/cookie", content).Result;
                if (!result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cookie creation failed!");
                    return; 
                }
            }
            MessageBox.Show("You are now registered!");
            PerformClick(btn_login);
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            using (var c = new HttpClient())
            {
                var serialized = JsonConvert.SerializeObject(new { username = Username, password = txt1_password.Password });
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");

                var result = c.PostAsync("http://localhost:40433/api/login", content).Result;
                if (result.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Debilad{Username}, you are now logged in!");
                    string resultContent = result.Content.ReadAsStringAsync().Result;
                    self = JsonConvert.DeserializeObject<Contact>(resultContent);
                }
                else
                {
                    MessageBox.Show("Login failed!");
                    return;
                }
            }

            openChatDialog();
        }
    }
}
