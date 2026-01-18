using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

namespace ECommerceAPI.Application.Abstractions.Storage
{
    public interface IStorage
    {
        //Öyle bir şekilde inşa etmeliyiz ki local,aws,azure gibi servislerde de ayağa kaldırabilelim.

        Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName, List<IFormFile> files);

        Task DeleteAsync(string pathOrContainerName, string fileName);
        List<string> GetFiles(string pathOrContainerName);
        bool HasFile(string pathOrContainerName, string fileName);

    }
}
