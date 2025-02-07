namespace KoperasiTenteraDAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ICNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Pin { get; set; }
        public bool PrivacyPolicyEnabled { get; set; }
        public bool BiometricEnabled { get; set; }
    }
}