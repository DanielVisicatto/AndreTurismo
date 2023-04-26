namespace AndreTurismo.Models
{
    public class Ticket
    {
        #region[Properties]
        public int Id { get; set; }
        public Address  Home { get; set; }
        public Address Destiny { get; set; }        
        public DateTime Date { get; set; }
        public double Price { get; set; }
        #endregion

        public override string ToString()
        {
            return $"ID_Passagem:           {Id}\n" +
                   $"Origem:                {Home}\n" +
                   $"Destino:               {Destiny}\n" +                  
                   $"Data:                  {Date}\n" +
                   $"Valor:                 {Price}\n\n";
        }
    }
}
