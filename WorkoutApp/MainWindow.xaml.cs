using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GithubUpdater;
using GithubUpdater.GitHub;

namespace WorkoutApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User LoggedInUser = null;
        public MainWindow()
        {
            InitializeComponent();
            Updater();
            SignIn();
            LoadWorkoutData();
        }

        public void Updater()
        {
           var updater = new GitHubUpdater();

            updater.SetRepo("jcaillon", "GithubUpdater");
            updater.UseCancellationToken(new CancellationTokenSource(3000).Token);
            updater.UseMaxNumberOfReleasesToFetch(10);

            var currentVersion = UpdaterHelper.StringToVersion(FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion);
            Console.WriteLine($"Our current version is: {currentVersion}.");

            var releases = updater.FetchNewReleases(currentVersion);
            Console.WriteLine($"We found {releases.Count} new releases on github.");
            Console.WriteLine($"The latest release if {releases[0].Name}.");

            Console.WriteLine($"Downloading the latest release asset: {releases[0].Assets[0].BrowserDownloadUrl}.");
            var tempFilePath = updater.DownloadToTempFile(releases[0].Assets[0].BrowserDownloadUrl, progress => {
                Console.WriteLine($"Downloading... {progress.NumberOfBytesDoneTotal} / {progress.NumberOfBytesTotal} bytes.");
            });

            var fileUpdater = SimpleFileUpdater.Instance;
            Console.WriteLine("We will replace this .exe with the one on the github release after this program has exited.");
            fileUpdater.AddFileToMove(tempFilePath, Assembly.GetExecutingAssembly().Location);
            fileUpdater.Start();
        }

        public void SignIn()
        {
            Login login = new Login();

            login.ShowDialog();
            if (login.DialogResult == false)
            {
                this.Close();
            }

            LoggedInUser = login.LoggedInUser;
            UsernameTextblock.Text = "Username: "+LoggedInUser.Username;
        }

        public void LoadWorkoutData()
        {
            workoutDataGrid.ItemsSource = Database.GetWorkoutsByUser(LoggedInUser);
        }

        public void DeleteWorkout()
        {
            if (workoutDataGrid.SelectedIndex == -1) return;

            Workout workout = workoutDataGrid.SelectedItem as Workout;
            Database.DeleteWorkout(workout.Id);
            LoadWorkoutData();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWorkout addWorkout = new AddWorkout(LoggedInUser);
            addWorkout.Closed += Window_Closed;
            addWorkout.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            LoadWorkoutData();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteWorkout();
        }

        private void StartWorkout_Click(object sender, RoutedEventArgs e)
        {
            if (workoutDataGrid.SelectedIndex == -1) return;

            Workout workout = workoutDataGrid.SelectedItem as Workout;

            OpenWorkout openWorkout = new OpenWorkout(workout, LoggedInUser);
            openWorkout.Show();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Database.DeleteWorkouts();
            LoadWorkoutData();
        }

        private void VeiwWorkoutsCom_Click(object sender, RoutedEventArgs e)
        {
            VeiwCompletedWorkputs veiwCompletedWorkputs = new VeiwCompletedWorkputs();
            veiwCompletedWorkputs.Show();
        }

        private void AutomaticUpdater_UpdateAvailable(object sender, EventArgs e)
        {
           
        }
    }
}
