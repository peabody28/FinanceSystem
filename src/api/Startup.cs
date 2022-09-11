using auth;
using core;
using entities;
using entities.Interfaces;
using FluentValidation.AspNetCore;
using logger.Interfaces;
using logger.Operations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using operations.Interfaces;
using operations.Operations;
using repositories;
using repositories.Interfaces;
using repositories.Repositories;
using System.Web.Http;
using validations.Interfaces;
using validations.Validations;

namespace api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

   
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<AuthOptions, AuthOptions>();

            services.AddScoped<Bank, Bank>();
            
            services.AddScoped<IUserOperation, UserOperation>();
            services.AddScoped<IFinanceOperationOperation, FinanceOperationOperation>();

            services.AddScoped<IFinanceOperationRepository, FinanceOperationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IChainRepository, ChainRepository>();

            services.AddScoped<IUserValidation, UserValidation>();

            services.AddScoped<IUser, UserEntity>();
            services.AddScoped<IProfile, ProfileEntity>();
            services.AddScoped<IChain, ChainEntity>();
            services.AddScoped<IFinanceOperation, FinanceOperationEntity>();

            services.AddScoped<ILoggingOperation, LoggingOperation>();

            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(ValidatorActionFilter));
                opt.EnableEndpointRouting = false;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.RequireHttpsMetadata = false;
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuer = true,
                          ValidIssuer = AuthOptions.ISSUER,

                          ValidateAudience = true,
                          ValidAudience = AuthOptions.AUDIENCE,
                          ValidateLifetime = true,

                          IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                          ValidateIssuerSigningKey = true,
                      };
                  });

            services.AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
