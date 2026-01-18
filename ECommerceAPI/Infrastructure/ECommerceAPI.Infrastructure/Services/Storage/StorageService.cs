using ECommerceAPI.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Infrastructure.Services.Storage
{
    //LocalStorage mimari de servisin(azure,aws...) kullandığı storage olacak.
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName { get => _storage.GetType().Name; }

        //Hangi servis kullanılacaksa o servis program.cs üzerinden belirlenecek. 

        public async Task DeleteAsync(string pathOrContainerName, string fileName)
        => await _storage.DeleteAsync(pathOrContainerName, fileName);

        public List<string> GetFiles(string pathOrContainerName)
        => _storage.GetFiles(pathOrContainerName); 

        public bool HasFile(string pathOrContainerName, string fileName)
        => _storage.HasFile(pathOrContainerName, fileName);

        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, List<IFormFile> files)
        => _storage.UploadAsync(pathOrContainerName, files);
    }
}
