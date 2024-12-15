using Org.BouncyCastle.Crypto.Generators;
using SoftverskaRealizacijaBackend.Dto;
using SoftverskaRealizacijaBackend.Models;
namespace SoftverskaRealizacijaBackend.Helpers
{
    public class ClientHelper
    {
        public static string HashPassword(string password)
        {
            return crypto.Security.ComputeHash(password, "aaa");
        }

        public static void UpdateClientFields(Client Client, ClientDto ClientDto)
        {
            Client.TipKorisnika = ClientDto.TipKorisnika;
            Client.Email = ClientDto.Email;
            Client.Password = HashPassword(ClientDto.Password);
            Client.Name = ClientDto.Name;
            
        }
    }
}
