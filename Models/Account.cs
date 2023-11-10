namespace MainProject
{
    public class Account
    {
        public int AccountID { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }

        public virtual Employee AccountEmloyee { get; set; }
    }
}
