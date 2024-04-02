using AexTest.Common;
using AexTest.Data;
using AexTest.Models;
using Microsoft.EntityFrameworkCore;

namespace AexTest.Service;

public class ManagerService : IManagerService
{
    private readonly AppDbContext _db;

    public ManagerService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Manager> AddManagerAsync(Manager manager)
    {
        try
        {
            await _db.Managers.AddAsync(manager);
            await _db.SaveChangesAsync();
            return await _db.Managers.FindAsync(manager.ID); // Auto ID from DB
        }
        catch (Exception ex)
        {
            return null; // An error occured
        }
    }

    public async Task<(bool, string)> DeleteManagerAsync(Manager manager)
    {
        try
        {
            var dbManager = await _db.Managers.FindAsync(manager.ID);

            if (dbManager == null)
            {
                return (false, "Manager could not be found");
            }

            _db.Managers.Remove(manager);
            await _db.SaveChangesAsync();

            return (true, "Manager got deleted.");
        }
        catch (Exception ex)
        {
            return (false, $"An error occured. Error Message: {ex.Message}");
        }
    }

    public async Task<Manager> GetManagerAsync(int id)
    {
        try
        {
            return await _db.Managers.FindAsync(id);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<Manager>> GetManagersAsync()
    {
        try
        {
            return await _db.Managers.ToListAsync();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<Manager> UpdateManagerAsync(Manager manager)
    {
        try
        {
            _db.Entry(manager).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            return manager;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
