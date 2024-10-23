using MongoDB.Entities;

namespace RentalCar.Domain.Entities;

[Collection("rentals")]
public class Rental : Entity , ICreatedOn
{
    [Field("createdOn")]
    public DateTime CreatedOn { get; set; }
    [Field("active")]
    public bool Active { get; set; }
    [Field("carId")]
    public string CarId { get; set; }
    
}