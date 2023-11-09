using System.ComponentModel.DataAnnotations.Schema;

namespace MainProject
{
    public class Transaction
    {
        public int TransactionID { get; set; }

        public string TransactionDescription { get; set; }

        public double TransactionFee { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime TransactionTime { get; set; }

        public virtual Room TransactionRoom { get; set; }

    }
}
