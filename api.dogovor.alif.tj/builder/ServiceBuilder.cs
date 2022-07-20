using ConnectionProvider.Context;
using IDropDownListService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.ArchievumRepositories;
using Repository.Email;
using Service;
using Service.ContractServices;
using Service.IDropDownListService;
using Service.UserService;
using System.Text;

namespace api.dogovor.alif.tj.builder
{
    public static class ServiceBuilder
    {
        public static void DbConnection(this IServiceCollection Services, string connectionString)
        {
            Services.AddDbContext<AppDbСontext>(x => x.UseSqlServer(connectionString, b => b.MigrationsAssembly("ConnectionProvider")).UseLazyLoadingProxies());
        }
        public static void InjectedServices(this IServiceCollection Services)
        {
            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<ICategoryAndSubCategoryRepository, CategoryAndSubCategoryRepository>();
            Services.AddScoped<ICategoryAndSubCategoryServices, CategoryAndSubCategoryServices>();
            Services.AddScoped<IDropDownLists, DropDownLists>();
            Services.AddScoped<IDropDowns, DropDowns>();
            Services.AddScoped<IArchiveRepository, ArchiveRepository>();
            Services.AddScoped<IArchiveService, ArchiveService>();
            Services.AddScoped<IMailService, MailService>();
            Services.AddScoped<MailSettings>();
        }
        public static void AddAuthentication(this IServiceCollection Services, WebApplicationBuilder builder)
        {
            Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(cfg =>
                {
                    cfg.SaveToken = true;
                    cfg.RequireHttpsMetadata = false;

                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:ValidateAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                    };
                });
        }
    }
}
