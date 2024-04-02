using AexTest.Models;

namespace AexTest.Common;

public interface IInfoService
{
    Task<List<CustomerViewModel>> GetInfoAsync(); // GET All Info
}
