using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;

namespace RentalCar.Domain.Entities;

[Collection("cars")]
public class Car : Entity
{
    [Field("carModel")]
    public string CarModel { get; set; }
    
    [Field("yearProduction")]
    public int YearProduction { get; set; }

    [Field("engineDisplacement ")]
    public double EngineDisplacement  { get; set; }
    
    [Field("vendor")]
    public One<Vendor> Vendor { get; set; }
    
    [InverseSide]
    public Many<User> OtherUsers { get; set; }

    public Car()
    {
        this.InitManyToMany(() => OtherUsers, user => user.Cars);
    }
    
    public Car Addvendor(One<Vendor> vendor)
    {
        Vendor = vendor;
        return this;
    }
}