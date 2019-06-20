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
    /// Interaction logic for OpenWorkout.xaml
    /// </summary>
    public partial class OpenWorkout : Window
    {
        public static User user1;
        public static Workout ComCircuit;
        public OpenWorkout(Workout workout, User user)
        {
            InitializeComponent();
           this.DataContext = workout;
            ComCircuit = workout;
            user1 = user;
        }

        private void CompletedCircuit1_Click(object sender, RoutedEventArgs e)
        {
            CompletCircuit completCircuit = Utils.CreateCompletedCircuit(user1.Username, ComCircuit.Circuit1, "Circuit 1");
            Database.InsertCompletedWorkout(completCircuit);
            MessageBox.Show("Workout Completed!!");
        }

        private void CompletedCircuit2_Click(object sender, RoutedEventArgs e)
        {
            CompletCircuit completCircuit = Utils.CreateCompletedCircuit(user1.Username, ComCircuit.Circuit2, "Circuit 2");
            Database.InsertCompletedWorkout(completCircuit);
            MessageBox.Show("Workout Completed!!");
        }

        private void CompletedCircuit3_Click(object sender, RoutedEventArgs e)
        {
            CompletCircuit completCircuit = Utils.CreateCompletedCircuit(user1.Username, ComCircuit.Circuit3, "Circuit 3");
            Database.InsertCompletedWorkout(completCircuit);
            MessageBox.Show("Workout Completed!!");
        }
    }
}
