using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Entities;
using RentalCar.Application.common.Interfaces;
using RentalCar.Domain.Entities;
using RentalCar.Infrastructure.Auth;
using RentalCar.Infrastructure.Configuration;

namespace RentalCar.Infrastructure;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {

        var mongoDbConfiguration = new MongoDbConfiguration();
        configuration.GetSection("MongoDbConfiguration")
            .Bind(mongoDbConfiguration);
        

        Task.Run(async () =>
        {
            await DB.InitAsync(mongoDbConfiguration.DatabaseName!,
                MongoClientSettings.FromConnectionString(mongoDbConfiguration.ConnectionString));
        }).GetAwaiter().GetResult();
        
        //serviceCollection.AddScoped<RoleManager<ApplicationRole>>(); 
        //serviceCollection.AddScoped<UserManager<ApplicationUser>>();
        
        serviceCollection.AddScoped<IAuthService, AuthService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        
        return serviceCollection;
    }

}