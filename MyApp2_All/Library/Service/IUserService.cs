using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyApp2.Models;
using BCrypt.Net;
using MyApp2.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace MyApp2.Services
{
    /// <summary>
    /// Interface defining user-related operations such as authentication,
    /// adding, deleting, and retrieving users.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Validates the username and password combination.
        /// </summary>
        /// <param name="username">Username to validate.</param>
        /// <param name="password">Plain text password to verify.</param>
        /// <returns>True if credentials are valid; otherwise false.</returns>
        Task<bool> ValidateLoginAsync(string username, string password);

        /// <summary>
        /// Adds a new user with the specified username and password.
        /// Password is securely hashed before storage.
        /// </summary>
        /// <param name="username">Username of the new user.</param>
        /// <param name="password">Plain text password for the new user.</param>
        /// <returns>True if the user was added successfully; false if username already exists.</returns>
        Task<bool> AddUserAsync(string username, string password);

        /// <summary>
        /// Deletes an existing user identified by username.
        /// </summary>
        /// <param name="username">Username of the user to delete.</param>
        /// <returns>True if deletion was successful; false if user not found.</returns>
        Task<bool> DeleteUserAsync(string username);

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>List of all user entities.</returns>
        Task<List<User>> GetAllUsersAsync();
    }

    /// <summary>
    /// Concrete implementation of IUserService using Entity Framework Core for data access
    /// and BCrypt for password hashing and verification.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        /// <summary>
        /// Constructs the UserService with the provided database context.
        /// </summary>
        /// <param name="db">Database context for accessing user data.</param>
        public UserService(AppDbContext db)
        {
            _db = db;
        }

        /// <inheritdoc />
        public async Task<bool> ValidateLoginAsync(string username, string password)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.Username == username);

            // Return false if user not found or password doesn't match
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return false;

            // Update last login time
            user.LastLoginTime = DateTime.UtcNow;

            // Save the change
            await _db.SaveChangesAsync();

            return true;
        }


        /// <inheritdoc />
        public async Task<bool> AddUserAsync(string username, string password)
        {
            // Check if username already exists in the database.
            if (await _db.Users.AnyAsync(u => u.Username == username))
                return false;

            // Hash the password securely using BCrypt.
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            // Add the new user record.
            _db.Users.Add(new User { Username = username, PasswordHash = hash });
            await _db.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc />
        public async Task<List<User>> GetAllUsersAsync()
        {
            // Retrieve and return all user records from the database.
            return await _db.Users.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<bool> DeleteUserAsync(string username)
        {
            // Find user by username.
            var user = await _db.Users.SingleOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return false;

            // Remove the user and save changes.
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
