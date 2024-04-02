using AexTest.Models;

namespace AexTest.Common;

public interface IManagerService
{
    // Managers Services
    Task<List<Manager>> GetManagersAsync(); // GET All Managers
    Task<Manager> GetManagerAsync(int id); // GET Single Managers
    Task<Manager> AddManagerAsync(Manager manager); // POST New Managers
    Task<Manager> UpdateManagerAsync(Manager manager); // PUT Managers
    Task<(bool, string)> DeleteManagerAsync(Manager manager); // DELETE Managers
}
