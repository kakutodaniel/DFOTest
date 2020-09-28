using System.Threading.Tasks;
using DFO.Test.Application.Context;

namespace DFO.Test.Application.Services
{
    public abstract class BaseService
    {
        private readonly ApiContext _context;

        protected BaseService(ApiContext context)
        {
            _context = context;
        }

        protected async Task<(bool, string)> SaveChanges()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                return (false, ex.ToString());
            }

            return (true, "");
        }

    }
}
