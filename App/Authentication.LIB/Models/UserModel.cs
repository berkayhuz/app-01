namespace Authentication.LIB.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? ZipCode { get; set; }
        public string Address { get; set; }
    }

}
