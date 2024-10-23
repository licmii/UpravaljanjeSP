using MongoDB.Entities;

namespace RentalCar.Domain.Entities;

[Collection("roles")]
public class Role : Entity
{
    [Field("roleName")]
    public string RoleName { get; set; }
    
    [Field("users")]
    public Many<User> Users { get; set; }

    public Role()
    {
        this.InitOneToMany(() => Users);
    }
}