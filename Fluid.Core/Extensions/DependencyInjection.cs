﻿using Fluid.Core.Features.Masters;
using Fluid.Core.Persistence;
using Fluid.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fluid.Core.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, string connectionStringKey)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(connectionStringKey)));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
        services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<AppDbContext>();
        return services;
    }

    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddTransient<KeyboardMasterService>();
        services.AddTransient<MouseMasterService>();
        return services;
    }
}
