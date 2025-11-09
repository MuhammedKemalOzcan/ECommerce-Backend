using ECommerceAPI.Application.Repositories.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories.File
{
    public class FileWriteRepository : WriteRepository<Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(ECommerceAPI.Persistence.Contexts.ECommerceAPIDbContext context) : base(context)
        {
        }
    
    }
}
