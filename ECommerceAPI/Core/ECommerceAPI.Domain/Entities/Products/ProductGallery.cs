using ECommerceAPI.Domain.Exceptions;

namespace ECommerceAPI.Domain.Entities.Products
{

    public class ProductGallery
    {
        //Boş ctor'un amacı EFCore nesnenin bir instance'ını oluşturmak ister sonrasında ise parametreli Ctor ile bu boşlukları doldurur.
        private ProductGallery() { }

        internal ProductGallery(ImageId imageId, string fileName, string path, string storage, ProductId productId, bool isPrimary)
        {
            if (string.IsNullOrEmpty(fileName)) throw new DomainException("File name cannot be null");
            if (string.IsNullOrEmpty(path)) throw new DomainException("Path name cannot be null");
            if (string.IsNullOrEmpty(storage)) throw new DomainException("Storage name cannot be null");

            Id = imageId;
            FileName = fileName;
            Path = path;
            Storage = storage;
            ProductId = productId;
            IsPrimary = isPrimary;
        }

        public ImageId Id { get; set; }
        public string FileName { get; private set; }
        public string Path { get; private set; }
        public string Storage { get; private set; }
        public ProductId ProductId { get; private set; }
        public bool IsPrimary { get; private set; }

        internal void SetPrimaryImage()
        {
            IsPrimary = true;
        }

        internal void SetNonPrimary()
        {
            IsPrimary = false;
        }

    }

}

public record ImageId(Guid Value);
