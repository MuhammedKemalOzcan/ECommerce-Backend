using ECommerceAPI.Infrastructure.Services.Operations;

namespace ECommerceAPI.Infrastructure.Services.Storage
{
    public class Storage
    {
        protected delegate bool HasFile(string pathOrContainerName, string fileName);
        protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod)
        {
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            string regulatedFileName = NameOperation.CharacterRegulatory(oldName);
            string newFileName = $"{regulatedFileName}{extension}";

            string fullPath = Path.Combine(pathOrContainerName, newFileName);
            int iteration = 1;
            while (hasFileMethod(pathOrContainerName,newFileName))
            {
                newFileName = $"{regulatedFileName}-{iteration}{extension}";
                fullPath = Path.Combine(pathOrContainerName, newFileName);
                iteration++;
            }

            return newFileName;
        }
    }
}
