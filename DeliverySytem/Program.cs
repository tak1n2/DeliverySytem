using Azure.Data.Tables;
using Azure.Storage.Blobs;

namespace DeliverySytem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = "";
			string blobContainer = "shop";
            string tableName = "shop";

			//connect to Blob storage
			builder.Services.AddSingleton(options => {
				BlobServiceClient serviceClient = new BlobServiceClient(connectionString);
				BlobContainerClient containerClient = serviceClient.GetBlobContainerClient(blobContainer);
				containerClient.CreateIfNotExists();
				return containerClient;
			});

			//connect to Table storage
			builder.Services.AddSingleton(options => {
				TableServiceClient serviceClient = new TableServiceClient(connectionString);
				TableClient tableClient = serviceClient.GetTableClient(tableName);
				tableClient.CreateIfNotExists();
				return tableClient;
			});

			//Session to use only in code
			builder.Services.AddSession(options => {
				options.IdleTimeout = TimeSpan.FromMinutes(20);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});

			//Session to use on views
			builder.Services.AddHttpContextAccessor();


			// Add services to the container.
			builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

			app.UseSession();

			app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Register}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
