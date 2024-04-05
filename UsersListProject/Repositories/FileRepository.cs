using FilozopLab04.UsersListProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace FilozopLab04.UsersListProject.Repositories
{
    internal class FileRepository
    {
        private static readonly string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CSKMAStorage");

        private readonly string[] names = new string[]
        {
            "John", "Emma", "Michael", "Olivia", "William", "Ava", "James", "Sophia", "Benjamin", "Isabella",
            "Lucas", "Mia", "Henry", "Charlotte", "Alexander", "Amelia", "Daniel", "Evelyn", "Matthew", "Harper",
            "Elijah", "Abigail", "Jack", "Emily", "Samuel", "Elizabeth", "David", "Sofia", "Joseph", "Ella",
            "Owen", "Madison", "Jackson", "Scarlett", "Sebastian", "Luna", "Leo", "Grace", "Carter", "Chloe",
            "Julian", "Penelope", "Wyatt", "Layla", "Gabriel", "Riley", "Isaac", "Zoey", "Lincoln", "Nora"
        };

        private readonly string[] surnames = new string[]
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
            "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin",
            "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson",
            "Walker", "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores", "Green",
            "Adams", "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts", "Gomez"
        };

        public FileRepository()
        {
            if (!Directory.Exists(BaseFolder))
            {
                Directory.CreateDirectory(BaseFolder);

                Random ran = new Random();
                for (int i = 0; i < 50; i++)
                {
                    int nameIndex = ran.Next(names.Length);
                    int surnameIndex = ran.Next(surnames.Length);

                    string firstName = names[nameIndex];
                    string lastName = surnames[surnameIndex];

                    _ = AddOrUpdate(new DBUser(Guid.NewGuid(), firstName, lastName, $"{firstName}.{lastName}@example.com",
                        new DateTime(ran.Next(1890, 2023), ran.Next(1, 13), ran.Next(1, 29))));
                }
            }
        }

        public async Task AddOrUpdate(DBUser person)
        {
            string stringObj = JsonSerializer.Serialize(person);

            using (StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, person.Guid.ToString()), false))
            {
                await sw.WriteAsync(stringObj);
            }
        }

        public async Task<DBUser> Get(Guid guid)
        {
            string stringObj = null;
            string filePath = Path.Combine(BaseFolder, guid.ToString());

            if (!File.Exists(filePath))
                return null;

            using (StreamReader sw = new StreamReader(filePath))
            {
                stringObj = await sw.ReadToEndAsync();
            }

            return JsonSerializer.Deserialize<DBUser>(stringObj);
        }

        public List<DBUser> GetAll()
        {
            var res = new List<DBUser>();

            foreach (var file in Directory.EnumerateFiles(BaseFolder))
            {
                string stringObj = null;

                using (StreamReader sw = new StreamReader(file))
                {
                    stringObj = sw.ReadToEnd();
                }

                res.Add(JsonSerializer.Deserialize<DBUser>(stringObj));
            }

            return res;
        }

        public void Delete(Guid guid)
        {
            string filePath = Path.Combine(BaseFolder, guid.ToString());
            if (!File.Exists(filePath))
                return;
            File.Delete(filePath);
        }
    }

}