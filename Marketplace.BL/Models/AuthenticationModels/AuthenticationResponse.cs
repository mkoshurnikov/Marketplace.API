﻿
namespace MarketplaceBL.Models.AuthenticationModels
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
