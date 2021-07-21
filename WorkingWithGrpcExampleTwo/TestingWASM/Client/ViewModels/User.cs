using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace TestingWASM.Models
{
    public class User
    {
        public User()
        {
            Claims = new List<ClaimLite>();
        }

        public string Id { get; set; }
        public string FullName { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        ///[JsonConverter(typeof(IdentityServer4.Stores.Serialization.ClaimConverter))]
        public List<ClaimLite> Claims { get; set; }

        public List<Claim> EmitClaims()
        {
            return Claims.Select(a => new Claim(a.Type, a.Value, a.ValueType)).ToList();
        }

        public void FillUser(ClaimsPrincipal user)
        {
            //    if (user.HasClaim(a => a.Type == ClaimTypes.WindowsAccountName))
            //        UserName = user.FindFirstValue(ClaimTypes.WindowsAccountName);

            //    if (user.HasClaim(a => a.Type == ClaimTypes.Name))
            //        FullName = user.FindFirstValue(ClaimTypes.Name);

            //    if (user.HasClaim(a => a.Type == ClaimTypes.Email))
            //        EmailAddress = user.FindFirstValue(ClaimTypes.Email);

            //    Claims = user.Claims.Where(a => string.IsNullOrEmpty(a.Value) == false).Select(a => new ClaimLite() { Type = a.Type, Value = a.Value, ValueType = a.ValueType}).ToList();
        }
    }

    public class ClaimLite
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }
    }

    public class UserRole
    {
        public UserRole(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; set; }
        public string Value { get; set; }
    }
}