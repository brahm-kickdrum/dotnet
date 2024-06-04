namespace Assessment_1.ViewModels
{
    public class RiderLoginRequestViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public RiderLoginRequestViewModel(string email, string password) 
        {
            Email = email;
            Password = password;
        }
    }
}
