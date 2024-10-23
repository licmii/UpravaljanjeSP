using MongoDB.Entities;

namespace RentalCar.Domain.Entities;

[Collection("users")]
public class User : Entity
{
    [Field("firstName")]
    public string FirstName { get; set; }
    [Field("lastName")]
    public string LastName { get; set; }
    [Field("email")]
    public string Email { get; set; }
    [Field("yearOfBirth")]
    public int YearOfBirth { get; set; }
    
    [Field("rentals")]
    public Many<Rental> Rentals { get; set; }
    
    [OwnerSide]
    public Many<Car> Cars { get; set; }

    public User()
    {
        this.InitOneToMany((() => Rentals));
        this.InitManyToMany(() => Cars, car => car.OtherUsers);
    }
    
    
}