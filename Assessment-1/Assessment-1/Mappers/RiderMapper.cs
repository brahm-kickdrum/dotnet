using Assessment_1.Entities;
using Assessment_1.ViewModels;

namespace Assessment_1.Mappers
{
    public static class RiderMapper
    {
        public static Rider ConvertRiderRegisterRequestViewModelToRider(RiderRegisterRequestViewModel riderRegisterRequestViewModel)
        {
            return new Rider(riderRegisterRequestViewModel.Name, riderRegisterRequestViewModel.Email, riderRegisterRequestViewModel.Phone, riderRegisterRequestViewModel.Password);
        }
    }
}
