using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Eğer validator yoksa direkt devam et
            if (!_validators.Any())
            {
                return await next();
            }

            // Validation context oluştur
            var context = new ValidationContext<TRequest>(request);

            // Tüm validator'ları çalıştır
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))
            );

            // Hataları topla
            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            // Eğer hata varsa exception fırlat
            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            // Validation başarılı, devam et
            return await next();
        }
    }
}
