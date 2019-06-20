using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WorkoutApp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public User LoggedInUser = null;

        public Login()
        {
            InitializeComponent();
        }

        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            // Add user to database, set LoggedInUser to new User, and Dialog result to true.
            User user = Utils.CreatUser(UsernameTextbox.Text, PasswordTextbox.Password);
            LoggedInUser = Database.InsertUser(user);
            if (LoggedInUser == null)
            {
                DialogResult = false;
            }
            else
            {

            }

            // Insert user will return id, use that id to get the new user
        }

        private void Login1_Click(object sender, RoutedEventArgs e)
        {
            LoggedInUser = Database.GetUser(UsernameTextbox.Text, PasswordTextbox.Password);
            
            if(LoggedInUser == null)
            {
                // Display message "Username or password is incorrect. Please try again.
                MessageBox.Show("Username or password is incorrect. Please try again");
            }
            else
            {
                DialogResult = true;
            }
        }

        private void PasswordTextbox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void PasswordTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}
