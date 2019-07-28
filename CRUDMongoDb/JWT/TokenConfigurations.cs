
namespace CRUDMongoDb.JWT
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }

    public class User
    {
        public string UserID { get; set; }
        public string AccessKey { get; set; }
    }
}
