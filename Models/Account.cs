namespace MainProject.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public byte[] Image { get; set; }
        public virtual Employee AccountEmployee { get; set; }
    }
}
