using Companies.Core.Models;

namespace Companies.Core.Interfaces;

public interface ICompanyQueryService
{
    Task<Company?> GetByIdAsync(int companyId);
}
