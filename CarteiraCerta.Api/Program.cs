using CarteiraCerta.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// adicionando o dbcontext para conexão com o oracle
// A LINHA ABAIXO FOI COMENTADA TEMPORARIAMENTE PARA O TESTE
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura o HttpClientFactory para a API Finnhub
builder.Services.AddHttpClient("Finnhub", client =>
{
    client.BaseAddress = new Uri("https://finnhub.io/api/v1/");
    var apiKey = builder.Configuration["Finnhub:ApiKey"];
    client.DefaultRequestHeaders.Add("X-Finnhub-Token", apiKey);
});

// adicionando serviços controllers e configurando o json para nao ter loops
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// adicionando o swagger para documentação e interface web
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();