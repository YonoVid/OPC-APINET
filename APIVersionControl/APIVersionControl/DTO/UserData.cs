namespace APIVersionControl.DTO
{
    public class User
    {
        public String? id { get; set; }
        public String? title { get; set; }
        public String? firstName { get; set; }
        public String? lastName { get; set; }
        public String? picture { get; set; }
    }
    public class UserResponseData
    {
        public User[]? data { get; set; }
        public int total { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }

}
