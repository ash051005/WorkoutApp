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
    /// Interaction logic for VeiwCompletedWorkputs.xaml
    /// </summary>
    public partial class VeiwCompletedWorkputs : Window
    {
        public VeiwCompletedWorkputs()
        {
            InitializeComponent();
            LoadWorkoutData();
        }
        public void LoadWorkoutData()
        {
            workoutDataGrid.ItemsSource = Database.GetCompletedCircuits();
        }
        private void DeleatButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteCompletedCircuit();
            LoadWorkoutData();
        }
        public void DeleteCompletedCircuit()
        {
            if (workoutDataGrid.SelectedIndex == -1) return;

            CompletCircuit workout = workoutDataGrid.SelectedItem as CompletCircuit;
            Database.DeleteCompletedCircuit(workout.Id);
            LoadWorkoutData();
        }
    }
}
