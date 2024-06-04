namespace Assessment_1.ViewModels
{
    public class DriverLoginRequestViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public DriverLoginRequestViewModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
