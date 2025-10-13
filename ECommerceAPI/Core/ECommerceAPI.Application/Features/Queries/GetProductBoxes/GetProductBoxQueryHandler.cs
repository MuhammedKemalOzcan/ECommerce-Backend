using ECommerceAPI.Application.Dtos.Products;
using ECommerceAPI.Application.Repositories.ProductBoxes;
using MediatR;

namespace ECommerceAPI.Application.Features.Queries.GetProductBoxes
{
    public class GetProductBoxQueryHandler : IRequestHandler<GetProductBoxQueryRequest, GetProductBoxQueryResponse>
    {
        private readonly IProductBoxReadRepository _readRepo;

        public GetProductBoxQueryHandler(IProductBoxReadRepository readRepo)
        {
            _readRepo = readRepo;
        }
        
        public async Task<GetProductBoxQueryResponse> Handle(GetProductBoxQueryRequest request, CancellationToken cancellationToken)
        {
            

            var productBox = await _readRepo.GetAllAsync(null,predicate: b => b.ProductId == request.ProductId,true,cancellationToken);

            var productBoxDto = productBox.Select(b => new ProductBoxDto
            {
                Name = b.Name,
                Quantity = b.Quantity
            }).ToList();

            return new GetProductBoxQueryResponse
            {
                ProductBox = productBoxDto
            };




        }
    }
}
