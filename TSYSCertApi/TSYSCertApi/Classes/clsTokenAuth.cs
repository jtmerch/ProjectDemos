using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TSYSCertApi.Models;

namespace TSYSCertApi.Classes
{

    //https://www.c-sharpcorner.com/article/jwt-json-web-token-authentication-in-asp-net-core/

    public class clsTokenAuth
    {
        public string NewToken { get; set; }
        public DateTime Expires { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        private IConfiguration _config;

        public clsTokenAuth(IConfiguration config)
        {
            _config = config;
        }

        public DevUserModel AuthenticateUser(TokenModel userInput)
        {
            DevUserModel devuser = null;

            //Validate the User Credentials
            string grant_type = userInput.grant_type;
            //
            //Demo Purpose, I have Passed HardCoded User Information    
            if (userInput.LoginID == "jtmerch@gmail.com")
            {
                devuser = new DevUserModel {  
                    UniqueID = "123456789", 
                    UserName = "Joe Thompson",
                    UserType = "Dev",
                    Email = "jtmerch@gmail.com", 
                    UserHash = "4ew4a9f4ewa9fa4",
                    JoinDate = DateTime.Now };
            }
            return devuser;

        }


        public void GenerateJSONWebToken(DevUserModel userInput)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub, userInput.UserName),
         new Claim(JwtRegisteredClaimNames.Name, userInput.UserName),
               new Claim(JwtRegisteredClaimNames.AtHash, userInput.UserHash),
        new Claim(JwtRegisteredClaimNames.Email, userInput.Email),
          new Claim(JwtRegisteredClaimNames.NameId, userInput.UserType),
        new Claim("DateOfJoing", userInput.JoinDate.ToString("yyyy-MM-dd")),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
         new Claim(ClaimTypes.Role, "Admin")
    };

            DateTime tokenExpireDate = DateTime.Now.AddMinutes(20);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: tokenExpireDate,
                signingCredentials: credentials);

            this.NewToken = new JwtSecurityTokenHandler().WriteToken(token);
            this.Expires = tokenExpireDate;
            this.UserFirstName = "Joe1";
            this.UserLastName = "Thompson";
          

        }
    }
}
