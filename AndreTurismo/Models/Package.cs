namespace AndreTurismo.Models
{
    public class Package
    {
        #region[Properties]
        public int Id { get; set; }
        public Hotel Hotel { get; set; }
        public Ticket Ticket { get; set; }
        public DateOnly RegisterDate { get; set; }
        public double Price { get; set; }
        public Customer Customer { get; set; }
        #endregion

        public override string ToString()
        {
            return $"ID:                {Id}\n" +
                   $"Hotel:             {Hotel}\n" +
                   $"Passagem:          {Ticket.Id}\n" +
                   $"Data_Registro:     {RegisterDate}\n" +
                   $"Preço:             {Price}\n" +
                   $"Cliente:           {Customer.Name}\n\n";
        }
    }
}
