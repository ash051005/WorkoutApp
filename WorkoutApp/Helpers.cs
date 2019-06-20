using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace WorkoutApp
{
    public static class Database
    {
        public static string GetConnectionString()
        {
            string baseString = @"%AppData%/WorkoutDatabase.db";
            return Environment.ExpandEnvironmentVariables(baseString);
        }

        public static void InsertWorkout(Workout newWorkout)
        {
            using (var db = new LiteDatabase(GetConnectionString()))
            {
                var workoutsCol = db.GetCollection<Workout>("workouts");
                workoutsCol.Insert(newWorkout);
            }
        }

        public static void InsertCompletedWorkout(CompletCircuit circuit)
        {
            using (var db = new LiteDatabase(GetConnectionString()))
            {
                var workoutsCol = db.GetCollection<CompletCircuit>("CompletedCircuit");
                workoutsCol.Insert(circuit);
            }
        }

        public static void DeleteWorkout(int id)
        {
            using (var db = new LiteDatabase(GetConnectionString()))
            {
                var workoutsCol = db.GetCollection<Workout>("workouts");
                workoutsCol.Delete(id);
            }
        }
        public static void DeleteCompletedCircuit(int id)
        {
            using (var db = new LiteDatabase(GetConnectionString()))
            {
                var workoutsCol = db.GetCollection<CompletCircuit>("CompletedCircuit");
                workoutsCol.Delete(id);
            }
        }

        public static void DeleteWorkouts()
        {
            using (var db = new LiteDatabase(GetConnectionString()))
            {
                var workoutsCol = db.GetCollection<Workout>("workouts");
                workoutsCol.Delete(Query.All());
            }
        }

        public static void UpdateWorkout(Workout workout)
        {
            using (var db = new LiteDatabase(GetConnectionString()))
            {
                var workoutsCol = db.GetCollection<Workout>("workouts");
                workoutsCol.Update(workout);
            }
        }


        public static Workout GetWorkout(int id)
        {
            using (var db = new LiteDatabase(GetConnectionString()))
            {
                var workoutsCol = db.GetCollection<Workout>("workouts");
                Workout workout = workoutsCol.FindById(id);
                return workout;
            }
        }
        public static IEnumerable<Workout> GetWorkoutsByUser(User user)
        {
            using (var db = new LiteDatabase(GetConnectionString()))
            {
                var WorkoutsCol = db.GetCollection<Workout>("workouts");
                IEnumerable<Workout> workouts = WorkoutsCol.Find(Query.EQ("Username", user.Username));
                return workouts;
            }
        }

        public static User GetUserById(int id)
        {
            using (var db = new LiteDatabase(GetConnectionString()))
            {
                var usersCol = db.GetCollection<User>("Users");
                User user = usersCol.FindById(id);
                return user;
            }
        }

        public static User GetUser(string username, string password)
        {
            using (var db = new LiteDatabase(GetConnectionString()))
            {
                var usersCol = db.GetCollection<User>("Users");
                User user = usersCol.FindOne(Query.And(Query.EQ("Username", username), Query.EQ("Password", password)));
                return user;
            }
        }

        public static IEnumerable<User> GetUsers()
        {
            using (var db = new LiteDatabase(GetConnectionString()))
            {
                var usersCol = db.GetCollection<User>("Users");
                IEnumerable<User> users = usersCol.FindAll();
                return users;
            }
        }

        public static User InsertUser(User newUser)
        {
            using (var db = new LiteDatabase(GetConnectionString()))
            {
                var usersCol = db.GetCollection<User>("Users");
                int id = usersCol.Insert(newUser);
                return usersCol.FindById(id);
            }
        }

        public static IEnumerable<CompletCircuit> GetCompletedCircuits()
        {
            using (var db = new LiteDatabase(GetConnectionString()))
            {
                var completedCircuitsCol = db.GetCollection<CompletCircuit>("CompletedCircuit");
                IEnumerable<CompletCircuit> completedCircuits = completedCircuitsCol.FindAll();
                return completedCircuits;
            }
        }

        public static IEnumerable<Workout> GetWorkouts()
        {
            using (var db = new LiteDatabase(GetConnectionString()))
            {
                var workoutsCol = db.GetCollection<Workout>("workouts");
                IEnumerable<Workout> workouts = workoutsCol.FindAll();
                return workouts;
            }
        }

        internal static void DeleteWorkout(IEnumerable<IndexInfo> enumerable)
        {
            throw new NotImplementedException();
        }
    }

    public static class Utils
    {
        public static Circuit CreateCircuit(string station1, string station2, string station3)
        {
            Circuit circuit = new Circuit
            {
                Station1 = station1,
                Station2 = station2,
                Station3 = station3
            };

            return circuit;
        }
        public static Workout CreateWorkout(string name, Circuit circuit1, Circuit circuit2, Circuit circuit3, string username)
        {
            Workout workout = new Workout
            {
                Name = name,
                Username = username,
                Circuit1 = circuit1,
                Circuit2 = circuit2,
                Circuit3 = circuit3
            };

            return workout;
        }
        public static CompletCircuit CreateCompletedCircuit(string name, Circuit circuit, string circuitName)
        {
            Circuit completedCircuit = CreateCircuit(circuit.Station1, circuit.Station2, circuit.Station3);

            CompletCircuit didWorkout = new CompletCircuit
            {
                Name = name,
                FinishedCircuit = completedCircuit,
                CircuitName = circuitName
            };
            return didWorkout;
        }
        public static User CreatUser(string username, string password)
        {
            User user = new User
            {
                Username = username,
                Password = password
            };

            return user;
        }
    }

    public class Circuit
    {
        public string Station1 { get; set; }
        public string Station2 { get; set; }
        public string Station3 { get; set; }
    }
    public class Workout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public Circuit Circuit1 { get; set; }
        public Circuit Circuit2 { get; set; }
        public Circuit Circuit3 { get; set; }
    }
    public class CompletCircuit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CircuitName { get; set; }
        public Circuit FinishedCircuit { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
