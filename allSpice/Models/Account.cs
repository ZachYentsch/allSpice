namespace allSpice.Models
{
    public class Account : Profile
    {
        public string Id { get; set; }
        public new string Name { get; set; }
        public string Email { get; set; }
        public new string Picture { get; set; }
    }

    public class Profile
    {
        public string Name { get; set; }
        public string Picture { get; set; }
    }
}