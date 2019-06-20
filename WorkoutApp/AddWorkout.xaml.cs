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
    /// Interaction logic for AddWorkout.xaml
    /// </summary>
    public partial class AddWorkout : Window
    {
        public static User user1;
        public AddWorkout(User user)
        {
            InitializeComponent();
            user1 = user;
        }
        public void Save_Click(object sender, RoutedEventArgs e)
        {
            Circuit circuit1 = Utils.CreateCircuit(Circuit1Station1.Text, Circuit1Station2.Text, Circuit1Station3.Text);
            Circuit circuit2 = Utils.CreateCircuit(Circuit2Station1.Text, Circuit2Station2.Text, Circuit2Station3.Text);
            Circuit circuit3 = Utils.CreateCircuit(Circuit3Station1.Text, Circuit3Station2.Text, Circuit3Station3.Text);
            Workout workout = Utils.CreateWorkout(WorkoutName.Text, circuit1, circuit2, circuit3, user1.Username);
            Database.InsertWorkout(workout);
            this.Close();
        }

    }
}
