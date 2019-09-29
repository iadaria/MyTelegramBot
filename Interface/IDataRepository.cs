using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyTelegramBot.Models.Telegram;

namespace MyTelegramBot.Interface
{
    public interface IDataRepository
    {
        EntityEntry<T> Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveAllAsync();
        Task<IEnumerable<Product>> GetProducts(long userId);
        Task<Product> GetProduct(int id);
        Task<IEnumerable<Category>> GetCategories(string languageCode = "ru", int parent = 0);
        Task<IEnumerable<Category>> GetAllCategories(int parent = 0);
        Task<Category> GetCategory(int id);
        Task<bool> MessageExists(long messageId);
        Task<bool> CallbackExists(string callbackId);
    }
}