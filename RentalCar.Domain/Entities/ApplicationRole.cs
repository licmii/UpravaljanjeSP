using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Entities;
using MongoDbGenericRepository.Attributes;

namespace RentalCar.Domain.Entities;

[Collection("roles")]
public class ApplicationRole : MongoIdentityRole<Guid>
{
    
}