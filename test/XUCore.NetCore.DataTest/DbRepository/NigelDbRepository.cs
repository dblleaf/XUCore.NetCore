﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Collections.Generic;
using System.Text;
using XUCore.NetCore.Data.BulkExtensions;
using XUCore.NetCore.Data.DbService;

namespace XUCore.NetCore.DataTest.DbRepository
{
    public static partial class ServiceCollectionExtensions
    {
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder =>
            {
#if DEBUG
                builder.AddConsole();
#endif
            });

        public static IServiceCollection AddNigelDbContext(this IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetService<IConfiguration>();

            services.AddDbContext<NigelDbEntityContext>(options =>
            {
                options.UseSqlServer(
                    connectionString: config.GetConnectionString("NigelDB_Connection"),
                    sqlServerOptionsAction: options =>
                        {
                            options.EnableRetryOnFailure();
                            //options.ExecutionStrategy(c => new MySqlRetryingExecutionStrategy(c.CurrentContext.Context));
                            //options.ExecutionStrategy(c => new SqlServerRetryingExecutionStrategy(c.CurrentContext.Context));
                        }
                    )
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

                //options.UseLoggerFactory(MyLoggerFactory);
            });

            services.AddScoped(typeof(INigelDbRepository<>), typeof(NigelDbRepository<>));
            services.AddScoped(typeof(INigelDbEntityContext), typeof(NigelDbEntityContext));

            return services;
        }
    }

    public interface INigelDbEntityContext : IDbContext { }
    public class NigelDbEntityContext : BaseRepositoryFactory, INigelDbEntityContext
    {
        public NigelDbEntityContext(DbContextOptions<NigelDbEntityContext> options)
            : base(typeof(NigelDbEntityContext), options, DbServer.SqlServer, $"XUCore.NetCore.DataTest.Mapping") { }
    }

    public interface INigelDbRepository<TEntity> : IMsSqlRepository<TEntity> where TEntity : class, new() { }
    public class NigelDbRepository<TEntity> : MsSqlRepository<TEntity>, INigelDbRepository<TEntity> where TEntity : class, new()
    {
        public NigelDbRepository(INigelDbEntityContext context) : base(context) { }
    }
}
