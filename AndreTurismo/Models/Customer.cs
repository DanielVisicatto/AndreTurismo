namespace AndreTurismo.Models
{
    public class Customer
    {
        #region[Properties]
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public string CellPhoneNumber { get; set; }
        public DateOnly RegisterDate { get; set; }
        #endregion
    }
}
