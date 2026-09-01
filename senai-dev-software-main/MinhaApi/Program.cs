<<<<<<< HEAD
=======

>>>>>>> 8734f2800096dd820fb80d63284ea20c84bb6caf
using MinhaApi.Repositories;
using MinhaApi.Services;

var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

=======
// Adiciona os serviços ao container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Registra o Repository
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

// Registra o Service
>>>>>>> 8734f2800096dd820fb80d63284ea20c84bb6caf
builder.Services.AddScoped<IProdutoService, ProdutoService>();

var app = builder.Build();

<<<<<<< HEAD
=======
// Configura o pipeline HTTP
>>>>>>> 8734f2800096dd820fb80d63284ea20c84bb6caf
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
<<<<<<< HEAD
app.Run();
=======

app.Run();

>>>>>>> 8734f2800096dd820fb80d63284ea20c84bb6caf
