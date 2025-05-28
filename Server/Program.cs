using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var physicalFileProvider = new PhysicalFileProvider(
    Path.GetFullPath(app.Configuration.GetValue<string>("WwwRootStaticFiles") ?? "./wwwroot")
);

app.UseDefaultFiles(new DefaultFilesOptions() { FileProvider = physicalFileProvider });

app.UseStaticFiles(new StaticFileOptions() { FileProvider = physicalFileProvider });

app.MapControllers();

app.MapFallbackToFile("index.html", new StaticFileOptions { FileProvider = physicalFileProvider });

app.Run();


