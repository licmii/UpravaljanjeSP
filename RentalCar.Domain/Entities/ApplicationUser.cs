using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Entities;
using MongoDbGenericRepository.Attributes;

namespace RentalCar.Domain.Entities;

[Collection("users")]
public class  ApplicationUser : MongoIdentityUser<Guid>
{
    [Field("firstName")]
    public string FirstName { get; set; }
    [Field("lastName")]
    public string LastName { get; set; }
    
    [Field("yearOfBirth")]
    public int YearOfBirth { get; set; }
}