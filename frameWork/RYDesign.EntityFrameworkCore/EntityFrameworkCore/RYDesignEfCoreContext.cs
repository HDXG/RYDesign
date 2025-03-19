using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace RYDesign.EntityFrameworkCore.EntityFrameworkCore;

public interface IRYDesignEfCoreContext : IAbpEfCoreDbContext;

public abstract class RYDesignEfCoreContext<TDbContext>(
    DbContextOptions<TDbContext> options)
        : AbpDbContext<TDbContext>(options), IRYDesignEfCoreContext
        where TDbContext : DbContext, IRYDesignEfCoreContext
{

}