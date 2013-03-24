using Nop.Core;
using Nop.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Nop.Plugin.Widgets.FloorPack.Data
{
    public class FloorPackRecordObjectContext : DbContext, IDbContext
    {
        public FloorPackRecordObjectContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FloorPackRecordMap());

            base.OnModelCreating(modelBuilder);
        }

        public string CreateDatabaseInstallationScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        public void Install()
        {
            Database.SetInitializer<FloorPackRecordObjectContext>(null);
            Database.ExecuteSqlCommand(CreateDatabaseInstallationScript());
            SaveChanges();
        }

        public void Uninstall()
        {
            var dbScript = "DROP TABLE FloorPack";
            Database.ExecuteSqlCommand(dbScript);
            SaveChanges();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText,
            params object[] parameters) where TEntity : BaseEntity, new()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, int? timeout = null, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
