using Caliburn.Micro;
using Newtonsoft.Json;
using SemenCommanderApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WpfExam.Model
{
    public class UserSingleton : PropertyChangedBase
    {
        // Singleton
        static UserSingleton instance;
        private UserSingleton()
        {
            LoadUser();
        }
        public static UserSingleton Instance => instance ?? (instance = new UserSingleton());
        public static UserSingleton getInstance()
        {
            if (instance == null)
                instance = new UserSingleton();
            return instance;
        }


        private string userId = "Accent Color";
        public string UserId
        {
            set
            {
                userId = value;
                NotifyOfPropertyChange();
            }
            get => userId;
        }

        private string password = "Accent Color";
        public string Password
        {
            set
            {
                password = value;
                NotifyOfPropertyChange();
            }
            get => password;
        }

        public void GenerateUser()
        {
            UserId = Guid.NewGuid().ToString();
            Password = (new Random().Next() % 100000).ToString();
            
            RegisterUser();
            SaveUser();
        }

        private async void RegisterUser()
        {
            var client = new HttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(new UserWe() { UserId = Guid.Parse(this.UserId), Password = this.Password }));

            var response = await client.PostAsync("https://localhost:44340/user", content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
        }

        public void RestoreUser()
        {
            if (File.Exists("nativeUser.txt"))
            {
                string[] userLoad = File.ReadAllText("nativeUser.txt").Split(';');
                UserId = userLoad[0];
                Password = userLoad[1];
            }
        }

        public void SaveUser()
        {
            File.Delete("currentUser.txt");
            using (StreamWriter sw = new StreamWriter("currentUser.txt", true))
            {
                sw.Write($"{userId};{Password}");
            }

        }

        private void LoadUser()
        {
            if (File.Exists("currentUser.txt"))
            {
                string[] userLoad = File.ReadAllText("nativeUser.txt").Split(';');
                UserId = userLoad[0];
                Password = userLoad[1];
            }
            else
            {
                if (File.Exists("nativeUser.txt"))
                {
                    string[] userLoad = File.ReadAllText("nativeUser.txt").Split(';');
                    UserId = userLoad[0];
                    Password = userLoad[1];
                }
                
                GenerateUser();

                using (StreamWriter sw = new StreamWriter("nativeUser.txt", true))
                {
                    sw.Write($"{userId};{Password}");
                }
            }
        }
    }
}
