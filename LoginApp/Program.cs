using LoginApp.Services;
using LoginApp.ViewModels.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoginApp.ViewModels
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            // создаем хост приложения
            var host = Host.CreateDefaultBuilder()
                // внедряем сервисы
                .ConfigureServices(services =>
                {
                    services.AddSingleton<App>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<ApplicationViewModel>();
                    services.AddScoped<IUserService, UserService>();
                    services.AddScoped<IPasswordHasher, DefaultPasswordHasher>();
                    /*
                    var builder = new ConfigurationBuilder();
                    builder.SetBasePath(Directory.GetCurrentDirectory());
                    builder.AddJsonFile("appsettings.json");
                    IConfigurationRoot config = builder.Build();

                    string connectionString = config.GetConnectionString("DefaultConnection");
                     // Тут не уверен как дальше управлять конфигами так, чтобы они оказались в контейнере
                    // TODO: читать конфиги из файла
                    */



                    string connectionString = "Data Source=loginApp.db";

                    
                    services.AddDbContext<UserContext>(options =>
                        options.UseSqlite(connectionString, sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(Program).Assembly.GetName().Name);
                        }));
                })
                .Build();


            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<UserContext>()!;
                context.Database.Migrate();
            }

            // получаем сервис - объект класса App
            var app = host.Services.GetService<App>();
            // запускаем приложения
            app?.Run();
        }
    }
}
