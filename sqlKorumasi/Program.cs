using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlKorumasi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string baglantiStr = "Host=localhost;Port=5402;Username=postgres;Password=abc123;Database=sqli_test;";

            Console.Write("Kullanici ID: ");
            string _userID = Console.ReadLine();
            Console.Write("Kullanici Adi: ");
            string _Username = Console.ReadLine();
            Console.Write("Sifre: ");
            string _Password = Console.ReadLine();
            int userID;
            string username;
            string password;
            try
            {
                userID = int.Parse(_userID);  //sqli korumasi
                string sqlSorgu = "SELECT username, passwd FROM hesaplar where userid = " + userID;
                using (NpgsqlConnection baglanti = new NpgsqlConnection(baglantiStr))
                {
                    try
                    {
                        baglanti.Open();
                        using (NpgsqlCommand cmd = new NpgsqlCommand(sqlSorgu, baglanti))
                        { 
                            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    username = reader["username"].ToString();
                                    password = reader["passwd"].ToString();

                                    Console.WriteLine($"Username: {username}, Password: {password}");

                                    if (username == _Username && password == _Password)
                                    {
                                        Console.WriteLine("Giris Basarili...");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Kullanici Adi veya Sifre yanlis");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Kullanıcı bulunamadı.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Hata: " + ex.Message);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Doğru id gir!");
            }
            Console.ReadLine();    
        }
    }
}
