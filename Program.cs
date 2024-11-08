using Hangfire;
using Hangfire.MySql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Hangfire to use MySQL
builder.Services.AddHangfire((sp, config) =>
{
    var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection");
    var storageOptions = new MySqlStorageOptions
    {
        // You can configure options here if needed, e.g.:
        // QueuePollInterval = TimeSpan.FromSeconds(15),
        // TransactionIsolationLevel = IsolationLevel.ReadCommitted,
        // etc.
    };
    config.UseStorage(new MySqlStorage(connectionString, storageOptions));
});

// Add Hangfire server
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Configure Hangfire dashboard (optional)

// app.UseHangfireDashboard();
app.UseHangfireDashboard("/test/job-dashboard", new DashboardOptions{

    DashboardTitle="Hangfire Job Application",
    DarkModeEnabled=true,
});
app.Run();