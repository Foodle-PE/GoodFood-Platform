
using appweb_back.iam.Domain.Model.Aggregates;
namespace appweb_back.iam.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> ValidateToken(string token);
}