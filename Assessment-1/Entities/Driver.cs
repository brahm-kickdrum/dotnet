using Assessment_1.Enums;
using Assessment_1.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assessment_1.Entities
{
    public class Driver
    {
        [Key]
        public Guid DriverId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(15)]
        public string PlateNumber { get; set; }

        [Required]
        public VehicleType VehicleType { get; set; }

        [Required]
        [StringLength(16)]
        public string LicenseNumber { get; set; }

        [Required]
        public DriverAvailability Availability { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public Driver(Guid userId, string plateNumber, VehicleType type, string licenseNumber, DriverAvailability availability)
        {
            DriverId = new Guid();
            UserId = userId;
            PlateNumber = plateNumber;
            VehicleType = type;
            LicenseNumber = licenseNumber;
            Availability = availability;
        }

        public Driver() { }
    }
}
