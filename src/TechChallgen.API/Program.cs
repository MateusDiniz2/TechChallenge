using TechChallenge.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Como voc� tem Razor Pages e API, vamos adicionar ambos
builder.Services.AddRazorPages();
builder.Services.AddControllers();  // necess�rio para controllers da API

// Configura��o do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuracao de dependencias dos projetos
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechChallenge API V1");
        c.RoutePrefix = string.Empty; // Swagger na raiz: http://localhost:5000/
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Mapear endpoints
app.MapRazorPages();
app.MapControllers();  // importante para rotas API funcionarem

app.Run();
