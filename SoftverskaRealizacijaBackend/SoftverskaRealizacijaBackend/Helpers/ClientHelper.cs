﻿using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using SoftverskaRealizacijaBackend.Dto;
using SoftverskaRealizacijaBackend.Models;

using static SoftverskaRealizacijaBackend.Models.Enumerations;

namespace SoftverskaRealizacijaBackend.Helpers
{
    public class ClientHelper
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static void UpdateClientFields(Client Client, ClientDto ClientDto)
        {
            Client.TipKorisnika = ClientDto.TipKorisnika;
            Client.Email = ClientDto.Email;
            Client.Password = HashPassword(ClientDto.Password);
            Client.fullName = ClientDto.FullName;
            
        }

        public static bool IsClientFieldsValid(ClientDto ClientDto)
        {
            if (string.IsNullOrEmpty(ClientDto.FullName))
                return false;
            if (string.IsNullOrEmpty(ClientDto.Email))
                return false;
            if (string.IsNullOrEmpty(ClientDto.Password))
                return false;
            
            

            return true;
        }
    }
}
