namespace AndreTurismo.Models
{
    public class Ticket
    {
        #region[Properties]
        public int Id { get; set; }
        public Address  Home { get; set; }
        public Address Destiny { get; set; }
        public Customer Customer { get; set; }
        public DateOnly Date { get; set; }
        public double Price { get; set; }
        #endregion
    }
}
