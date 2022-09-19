namespace UniversityApiBackend.Model.DataModels
{
    public class UserTokens
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public TimeSpan? Validity { get; set; }
        public String RefreshToken { get; set; }
        public String EmailId { get; set; }
        public Guid GuidId { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}
