using Assessment_1.Entities;
using Assessment_1.ViewModels;

namespace Assessment_1.Mappers
{
    public static class DriverMapper
    {
        public static Driver ConvertDriverRegisterRequestViewModelToDriver(DriverRegisterRequestViewModel driverRegisterRequestViewModel)
        {
            return new Driver(driverRegisterRequestViewModel.Name, driverRegisterRequestViewModel.Email, driverRegisterRequestViewModel.Phone, driverRegisterRequestViewModel.Password, driverRegisterRequestViewModel.VehicleNumber, driverRegisterRequestViewModel.LicenceNumber);
        }
    }
}
