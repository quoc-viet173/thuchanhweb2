using WebAPI.Models.Domain;

namespace WebAPI.Services
{
    public interface ILibraryService
    {
        

        Task<List<Publishers>> GetPublishersAsync();
        Task<Publishers> GetPublisherAsync(int id);
        Task<Publishers> AddPublisherAsync(Publishers publisher);
        Task<Publishers> UpdatePublisherAsync(Publishers publisher);
        Task<bool> DeletePublisherAsync(int id);
    }
}
