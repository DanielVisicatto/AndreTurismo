namespace AndreTurismo.Models
{
    public class City
    {
        #region[Properties]
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime RegisterDate { get; set; }
        #endregion

        public override string ToString()
        {
            return $"ID_cidade:             {Id}\n" +
                   $"Descrição_Cidade:      {Description}\n" +
                   $"Restrada em:           {RegisterDate}\n\n";
        }
    }
}
