namespace AndreTurismo.Models
{
    public class Ticket
    {
        #region[Properties]
        public int Id { get; set; }
        public Address?  Home { get; set; }
        public Address? Destiny { get; set; }        
        public Customer? Customer { get; set; }
        public DateTime? Date { get; set; }
        public double Price { get; set; }
        #endregion

        public override string ToString()
        {
            return $"ID_Passagem:           {Id}\n" +
                   $"Cliente:               {Customer.Name}\n" +
                   $"Origem:                {Home.City.Description}\n" +
                   $"Destino:               {Destiny.City.Description}\n" +                  
                   $"Data:                  {Date}\n" +
                   $"Valor:                 {Price}\n\n";
        }
    }
}
