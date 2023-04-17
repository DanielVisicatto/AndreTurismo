namespace AndreTurismo.Models
{
    public class Package
    {
        #region[Properties]
        public int Id { get; set; }
        public Hotel Hotel { get; set; }
        public Ticket Ticket { get; set; }
        public DateOnly MyProperty { get; set; }
        public double Price { get; set; }
        public Customer Customer { get; set; }
        #endregion
    }
}
