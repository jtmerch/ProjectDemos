using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace TSYSWasm.Authentication
{
    public static class JwtParser
    {
        public static IEnumerable<Claim> ParseClaimsFromJwT(string jwt) //pass in "jwt" get back IEnumerable of a lot of claims
        {
            var claims = new List<Claim>(); //claims allows you to get information we are using claims specificly for roles information.

            var payLoad = jwt.Split('.')[1]; //here we're only getting the value at the second position (this is called the "payload" in JWT.

            var jsonBytes = ParseBase64WithoutPadding(payLoad);

            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes); //break down jsonBytes and deserialize into name value pairs.


            ExtractRolesFromJWT(claims, keyValuePairs);

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()))); //Here we are adding any claims to our claims that are in the keyvaluepairs.

            return claims; //claims allows you to get information we are using claims specificly for roles information.


        }

        private static void ExtractRolesFromJWT(List<Claim> claims, Dictionary<string, object> keyValuePairs)
        {
            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);
          
            if (roles != null) //if we found
            {
                var parsedRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(",");

                if (parsedRoles.Length > 1) //if there is more than one role
                {
                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole.Trim('"')));
                    }
                }
                else //if there is only one role
                {
                    claims.Add(new Claim(ClaimTypes.Role, parsedRoles[0]));
                }

                keyValuePairs.Remove(ClaimTypes.Role); //take out the role from the list so it's not double processed
            }
        }
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
                {
                // % (mod) explanation:
                // 4/2 = 0 (no remainder)
                // 5/2 = 1 (remainder 1)
                case 2:
                    base64 += "==";
                break;
                case 3:
                    base64 += "=";
                break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
