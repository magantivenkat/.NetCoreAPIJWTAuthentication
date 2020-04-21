using System.ComponentModel.DataAnnotations;

namespace JwtTokenDemo.Model.Responses
{
    public class UserDataResponse {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
}