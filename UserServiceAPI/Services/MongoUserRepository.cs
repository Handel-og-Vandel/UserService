using System.Security.Cryptography;
using System.Text;
using MongoDB.Driver;

namespace Services;

public class MongoUserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _userCollection;
    private readonly ILogger<MongoUserRepository> _logger;

    public MongoUserRepository(ILogger<MongoUserRepository> logger, IConfiguration configuration)
    {
        _logger = logger;

        // Add MongoDB settings to the configuration
        var connectionString = configuration["MongoConnectionString"];
        var databaseName = configuration["DatabaseName"];
        var collectionName = configuration["CollectionName"];

        _logger.LogInformation("Using: {connectionString}, {databaseName}, {collectionName}", connectionString, databaseName, collectionName);

        try
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _userCollection = database.GetCollection<User>(collectionName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error connecting to MongoDB");
            throw;
        }

        _logger.LogInformation("Connected to MongoDB");

    }

    public async Task<User> CreateUserAsync(User user)
    {
        if (user.LoginIdentifier != null)
        {
            var existingUser = await GetUserByLoginIdentifier(user.LoginIdentifier);
            if (existingUser != null)
            {
                throw new Exception("User with this login identifier already exists");
            }
            else
            {
                user.Salt = Guid.NewGuid().ToString();
                user.Password = ComputeSHA256Hash(user.Password + user.Salt);
                await _userCollection.InsertOneAsync(user);
            }
        }

        return user;
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        var filter = Builders<User>.Filter.Eq("_id", id);
        return await _userCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userCollection.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<User>> GetUsersByDepartmentAsync(string department)
    {
        var filter = Builders<User>.Filter.Eq("department", department);
        return await _userCollection.Find(filter).ToListAsync();
    }

    public async Task<User> UpdateUserAsync(string id, User user)
    {
        var filter = Builders<User>.Filter.Eq("_id", id);
        var update = Builders<User>.Update
            .Set("givenName", user.GivenName)
            .Set("familyName", user.FamilyName)
            .Set("department", user.Department)
            .Set("password", ComputeSHA256Hash(user.Password + user.Salt))
            .Set("salt", user.Salt)
            .Set("email", user.Email)
            .Set("loginIdentifier", user.LoginIdentifier);

        await _userCollection.UpdateOneAsync(filter, update);

        return user;
    }

    private string ComputeSHA256Hash(string rawData)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
    }

    public async Task<bool> DeleteUserAsync(string id)
    {
        var filter = Builders<User>.Filter.Eq("_id", id);
        var result = await _userCollection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }

    public async Task<User> GetUserByLoginIdentifier(string loginName)
    {
        var filter = Builders<User>.Filter.Eq("loginIdentifier", loginName);
        return await _userCollection.Find(filter).FirstOrDefaultAsync();
    }
}
