namespace ECommerceAPI.Domain.Contracts
{
    public interface IAuditableEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
