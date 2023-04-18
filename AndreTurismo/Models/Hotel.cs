namespace AndreTurismo.Models
{
    public class Hotel
    {
        #region[Properties]
        public int Id { get; set; }        
        public string? Name { get; set; }
        public Address? Address { get; set; }
        public DateTime RegisterDate { get; set; }
        public double Price { get; set; }
        #endregion

        public override string ToString()
        {
            return $"ID:             {Id}\n" +
                   $"Hotel:          {Name}\n" +
                   $"Endereço:       {Address}\n" +
                   $"Data_Registro:  {RegisterDate}\n" +
                   $"Valor_Diária:   {Price}\n\n";
        }
    }
}
