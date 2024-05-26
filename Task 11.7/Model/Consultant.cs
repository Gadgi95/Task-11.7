using System.IO;
using Task_11._7.Model.Base;

namespace Task_11._7.Model
{
    public class Consultant : BaseUser
    {
        public Consultant() { }

        public override void ReadInfoClients(long phone)
        {
            Client client = new Client();

            client.SearchClientForPhone(phone);

            string clientInfo = GetClientInfo(client.Phone);

        }

        public override void SetPhoneNumber(long phone)
        {
            Client client = new Client();
            client.SearchClientForPhone(phone);
            SaveChangedInfo(ChangeInfoClient(client.Phone.ToString(), phone.ToString()));
            client.Phone = phone;
            Console.WriteLine("Номер телефона успешно изменен");

        }

        public override string ChangeInfoClient(string whatChanged, string typeOfChanged)
        {
            DateTime dateTime = DateTime.Now;

            string whoChangedIt = "user";

            return dateTime.ToString() + " " + whatChanged + " " + typeOfChanged + " " + whoChangedIt;
        }

        public override void SaveChangedInfo(string changedInfo)
        {
            string path = @"List changed info of clients.txt";

            if (!File.Exists(path))
            {

                File.WriteAllText(path, changedInfo);
            }
            else
            {
                string readFile = File.ReadAllText(path); // Путь к фаилу

                string[] arrayReadFile = readFile.Split(Environment.NewLine); //Создание и заполнение массива строками из фаила

                string[] newFile = new string[arrayReadFile.Length + 1];

                foreach (string file in arrayReadFile)
                {
                    newFile[0] = file;
                }
                newFile[arrayReadFile.Length - 1] = changedInfo;

                File.WriteAllLines(path, newFile);

            }
        }

        public override string GetClientInfo(long phone)
        {
            Client client = new Client();

            client.SearchClientForPhone(phone);

            return $"Фамилия {client.LastName}"
                          + $"#Имя {client.FirstName}"
                          + $"#Отчество {client.Patronymic}"
                          + $"#Телефон {client.Phone} "
                          + $"#Серия и номер паспорта {GetSeriesAndNumberPasportFromeUser(client)}";
        }

        public string GetSeriesAndNumberPasportFromeUser(Client client)
        {
            return System.Text.RegularExpressions.Regex.Replace(client.SeriesAndNumberPasport, @"\d", "*");
        }
    }
}
