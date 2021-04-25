namespace DocRecycle
{
    public static class ExternalService
    {
        public static ExternalServiceData GetData(string pnr)
        {
            return new ExternalServiceData("sltkval1@gmail.com", "+79154236700", "Алексей", "Сергеевич", "Слетков");
        }
    }

    public class ExternalServiceData
    {
        public ExternalServiceData(string email, string phone, string firstName, string middleName, string lastName)
        {
            Email = email;
            Phone = phone;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}