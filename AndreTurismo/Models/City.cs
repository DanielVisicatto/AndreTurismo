namespace AndreTurismo.Models
{
    public class City
    {
        #region[Properties]
        public static readonly string INSERT = "INSERT INTO [City] (Description) VALUES (@Description);" +
                                        "SELECT CAST (scope_identity() AS int)";
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime RegisterDate { get; set; }
        #endregion

        public override string ToString()
        {
            return $"ID_cidade:             {Id}\n" +
                   $"Descrição_Cidade:      {Description}\n" +
                   $"Data do Registro:      {RegisterDate}\n";
        }
    }
}
