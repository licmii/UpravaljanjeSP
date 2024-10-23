using MongoDB.Bson;
using MongoDB.Entities;

namespace RentalCar.Domain.Entities;

[Collection("vendors")]
public class Vendor : Entity
{
    [Field("vendorName")]
    public string Name { get; set; }
    

    public Vendor(string name)
    {
        Name = name;
    }
    
}