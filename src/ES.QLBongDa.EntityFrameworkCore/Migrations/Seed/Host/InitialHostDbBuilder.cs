using ES.QLBongDa.EntityFrameworkCore;

namespace ES.QLBongDa.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly QLBongDaDbContext _context;

        public InitialHostDbBuilder(QLBongDaDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
