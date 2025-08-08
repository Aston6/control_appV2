using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp2.Services;
using MyApp2.Models;
using MyApp2.Data; // Replace with your actual DbContext namespace

public class UserServiceTests
{
    private async Task<AppDbContext> CreateDbContextAsync()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{System.Guid.NewGuid()}")
            .Options;

        var context = new AppDbContext(options);
        await context.Database.EnsureCreatedAsync();
        return context;
    }

    [Fact]
    public async Task ValidateLoginAsync_ReturnsTrue_WhenCredentialsAreValid()
    {
        // Arrange
        var context = await CreateDbContextAsync();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword("password");
        context.Users.Add(new User { Username = "testuser", PasswordHash = hashedPassword });
        await context.SaveChangesAsync();

        var service = new UserService(context);

        // Act
        var result = await service.ValidateLoginAsync("testuser", "password");

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task AddUserAsync_ReturnsTrue_WhenUserIsAddedSuccessfully()
    {
        // Arrange
        var context = await CreateDbContextAsync();
        var service = new UserService(context);

        // Act
        var result = await service.AddUserAsync("newuser", "password");

        // Assert
        Assert.True(result);
        Assert.NotNull(await context.Users.FirstOrDefaultAsync(u => u.Username == "newuser"));
    }

    [Fact]
    public async Task DeleteUserAsync_ReturnsTrue_WhenUserIsDeleted()
    {
        // Arrange
        var context = await CreateDbContextAsync();
        context.Users.Add(new User { Username = "existinguser", PasswordHash = "pass" });
        await context.SaveChangesAsync();

        var service = new UserService(context);

        // Act
        var result = await service.DeleteUserAsync("existinguser");

        // Assert
        Assert.True(result);
        Assert.Null(await context.Users.FirstOrDefaultAsync(u => u.Username == "existinguser"));
    }

    [Fact]
    public async Task GetAllUsersAsync_ReturnsListOfUsers()
    {
        // Arrange
        var context = await CreateDbContextAsync();
        context.Users.AddRange(
            new User { Username = "user1", PasswordHash = "p1" },
            new User { Username = "user2", PasswordHash = "p2" }
        );
        await context.SaveChangesAsync();

        var service = new UserService(context);

        // Act
        var result = await service.GetAllUsersAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("user1", result[0].Username);
    }
}
