namespace Unictive.Model
{
    public class Member
    {
        public string Id { get; set; }
        public string Nama { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Hobby> ListHobby { get; set; }
    }

    public class Hobby
    {
        public string Id { get; set; }
        public string Nama { get; set; }
    }
}
