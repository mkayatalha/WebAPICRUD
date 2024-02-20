using AlbarakaTestAPI.Models;
using Bogus;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
public class CreateUser
{
    static void Main()
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-KQ2634Q\ALBARAKATEST;Initial Catalog=master;Integrated Security=True;TrustServerCertificate=True");
        sqlConnection.Open();
        
        List<Customer> customers = GenerateCustomers(10);

        foreach (var customer in customers)
        {
            string insert_query = "INSERT INTO Customers" +
            "(CustomerID,Name,Surname,Age,TCKNO,PhoneNumber)" +
            $"VALUES({customer.CustomerID},'{customer.Name}','{customer.Surname}',{customer.Age},'{customer.TCKNO}','{customer.PhoneNumber}')";
            SqlCommand insertCommand = new SqlCommand(insert_query, sqlConnection);
            insertCommand.ExecuteNonQuery();
        }
        sqlConnection.Close();
    }

    static List<Customer> GenerateCustomers(int count)
    {
        Random random = new Random();
        List<Customer> customers = new List<Customer>();

        for (int i = 1; i <= count; i++)
        {
            Customer customer = new Customer
            {
                CustomerID = i,
                Name = GenerateFakeNameSurname().Split('-')[0],
                Surname = GenerateFakeNameSurname().Split('-')[1],
                Age = random.Next(18, 100),
                TCKNO = GenerateRandomTCKNO(),
                PhoneNumber = GenerateRandomPhoneNumber()
            };

            customers.Add(customer);
        }

        return customers;
    }

    static string GenerateRandomTCKNO()
    {
        Random random = new Random();

        // İlk hanesi 1-9 arasında bir rakam olmalıdır.
        int firstDigit = random.Next(1, 10);

        // İlk 9 hane için rastgele değerler oluştur
        int[] digits = new int[8];
        for (int i = 0; i < 8; i++)
        {
            digits[i] = random.Next(0, 10);
        }

        // 1, 3, 5, 7, 9 basamaklarının toplamının 7 katından, 2, 4, 6, 8 basamaklarının toplamını çıkartma
        int sumOdd = firstDigit + digits[1] + digits[3] + digits[5] + digits[7];
        int sumEven = digits[0] + digits[2] + digits[4] + digits[6];
        int tenthDigit = (7 * sumOdd - sumEven) % 10;

        // İlk 10 hanenin toplamından elde edilen sonucun 10’a bölümünden kalan sayı (MOD10)
        int sumAll = firstDigit + digits.Sum();
        int eleventhDigit = sumAll % 10;

        // TCKNO'yu oluştur
        string tckno = $"{firstDigit}{string.Join("", digits)}{tenthDigit}{eleventhDigit}";

        return tckno;
    }

    static string GenerateRandomPhoneNumber()
    {
        Random random = new Random();
        return $"555-{random.Next(100, 999)}-{random.Next(10, 99)}-{random.Next(10, 99)}";
    }
    static string GenerateFakeNameSurname()
    {
        var faker = new Faker("tr");

        string fakeNameSurname = $"{faker.Name.FirstName()}-{faker.Name.LastName()}";

        return fakeNameSurname;
    }
}