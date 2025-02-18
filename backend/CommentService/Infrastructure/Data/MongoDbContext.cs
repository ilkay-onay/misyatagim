using MongoDB.Driver;
using CommentService.Domain.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson; // Eklenen using direktifi

namespace CommentService.Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDB"));
        _database = client.GetDatabase("CommentServiceDb");
        TestConnection(); // Bağlantı test fonksiyonunu çağır
    }

    public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("Comments");
     private void TestConnection() {
        try
        {
            // MongoDB'ye basit bir komut göndererek bağlantıyı test et
            _database.RunCommand((Command<BsonDocument>)"{ping:1}");
             Console.WriteLine("MongoDB bağlantısı başarılı"); // Konsola yazdır
        }
        catch(Exception ex)
        {
            Console.WriteLine("MongoDB bağlantı hatası: "+ ex.Message);
        }
    }
}