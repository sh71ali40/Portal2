using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Portal.DataLayer.Model.Entities;

#nullable disable

namespace Portal.DataLayer.Model
{
    public partial class PortalContext : DbContext,IUnitOfWork
    {
        public PortalContext()
        {
        }

        public PortalContext(DbContextOptions<PortalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<ModuleDef> ModuleDefs { get; set; }
        public virtual DbSet<ModuleDefRole> ModuleDefRoles { get; set; }
        public virtual DbSet<ModuleDefSetting> ModuleDefSettings { get; set; }
        public virtual DbSet<ModuleRole> ModuleRoles { get; set; }
        public virtual DbSet<ModuleSetting> ModuleSettings { get; set; }
        public virtual DbSet<ObjLog> ObjLogs { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<PageModule> PageModules { get; set; }
        public virtual DbSet<PageRole> PageRoles { get; set; }
        public virtual DbSet<PermissionRoleBase> PermissionRoleBases { get; set; }
        public virtual DbSet<PermissionRoleModuleDef> PermissionRoleModuleDefs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Arabic_CI_AS");

            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.HasKey(e => e.EventId)
                    .HasName("PK_t_ErrorLog");

                entity.Property(e => e.LogDateTime).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<ModuleDef>(entity =>
            {
                entity.Property(e => e.ModuleDefId).ValueGeneratedNever();

                entity.Property(e => e.HomeController).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<ModuleDefRole>(entity =>
            {
                entity.HasOne(d => d.ModuleDef)
                    .WithMany(p => p.ModuleDefRoles)
                    .HasForeignKey(d => d.ModuleDefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuleDefRole_ModuleDefRole");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.ModuleDefRoles)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuleDefRole_PermissionRoleBase");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ModuleDefRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuleDefRole_Role");
            });

            modelBuilder.Entity<ModuleDefSetting>(entity =>
            {
                entity.Property(e => e.SettingId).ValueGeneratedNever();

                entity.HasOne(d => d.ModuleDef)
                    .WithMany(p => p.ModuleDefSettings)
                    .HasForeignKey(d => d.ModuleDefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuleDefSetting_ModuleDef1");
            });

            modelBuilder.Entity<ModuleRole>(entity =>
            {
                entity.HasOne(d => d.Module)
                    .WithMany(p => p.ModuleRoles)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuleRole_PageModule");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.ModuleRoles)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuleRole_PermissionRoleBase");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.ModuleRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuleRole_Role");
            });

            modelBuilder.Entity<ModuleSetting>(entity =>
            {
                entity.HasOne(d => d.Module)
                    .WithMany(p => p.ModuleSettings)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuleSetting_PageModule");

                entity.HasOne(d => d.Setting)
                    .WithMany(p => p.ModuleSettings)
                    .HasForeignKey(d => d.SettingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuleSetting_ModuleDefSetting");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.Property(e => e.TemplateName).IsUnicode(false);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Pages_Pages");
            });

            modelBuilder.Entity<PageModule>(entity =>
            {
                entity.Property(e => e.PaneName).IsUnicode(false);

                entity.HasOne(d => d.ModuleDef)
                    .WithMany(p => p.PageModules)
                    .HasForeignKey(d => d.ModuleDefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PageModule_ModuleDef");

                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageModules)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PageModule_Pages");
            });

            modelBuilder.Entity<PageRole>(entity =>
            {
                entity.HasOne(d => d.Page)
                    .WithMany(p => p.PageRoles)
                    .HasForeignKey(d => d.PageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PageRole_Page");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.PageRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PageRole_Role");
            });

            modelBuilder.Entity<PermissionRoleBase>(entity =>
            {
                entity.Property(e => e.PermissionId).ValueGeneratedNever();
            });

            modelBuilder.Entity<PermissionRoleModuleDef>(entity =>
            {
                entity.HasOne(d => d.ModuleDef)
                    .WithMany(p => p.PermissionRoleModuleDefs)
                    .HasForeignKey(d => d.ModuleDefId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionRoleModuleDef_ModuleDef");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.PermissionRoleModuleDefs)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionRoleModuleDef_PermissionRoleBase");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.JoinDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.JoinIp).IsUnicode(false);

                entity.Property(e => e.LastIp).IsUnicode(false);

                entity.Property(e => e.Mobile).IsUnicode(false);

                entity.Property(e => e.SentCode).IsUnicode(false);

                entity.Property(e => e.UserName).IsUnicode(false);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        #region IUnitOfWork

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().AddRange(entities);
        }

        public T GetShadowPropertyValue<T>(object entity, string propertyName) where T : IConvertible
        {
            var value = this.Entry(entity).Property(propertyName).CurrentValue;
            return value != null
                ? (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture)
                : default;
        }

        public object GetShadowPropertyValue(object entity, string propertyName)
        {
            return this.Entry(entity).Property(propertyName).CurrentValue;
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Update(entity);
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            Set<TEntity>().RemoveRange(entities);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.DetectChanges();

            BeforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled =
                false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            BeforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled =
                false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChanges();
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            BeforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled =
                false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChangesAsync(cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();

            BeforeSaveTriggers();

            ChangeTracker.AutoDetectChangesEnabled =
                false; // for performance reasons, to avoid calling DetectChanges() again.
            var result = base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }

        private void BeforeSaveTriggers()
        {
        }

        public int SaveAllChanges()
        {
            return base.SaveChanges();
        }

        public async Task<int> SaveAllChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        public void Connection()
        {
            Database.OpenConnection();
        }

        public DbCommand DbCommands()
        {
            DbCommand cmd = Database.GetDbConnection().CreateCommand();
            return cmd;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await Database.BeginTransactionAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }

        public PropertyInfo[] GetKey<T>()
        {
            var keyNames = base.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties.Select(x => x.Name).ToList();
            PropertyInfo[] propInfo = new PropertyInfo[keyNames.Count];
            for (int i = 0; i < keyNames.Count; i++)
            {
                propInfo[i] = typeof(T).GetProperty(keyNames.ElementAt(i));
            }
            return propInfo;
        }

        public string GetKeyName<T>()
        {
            return base.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();
        }

        public IEnumerable<EntityEntry> ChangeTrackerEntries()
        {
            return base.ChangeTracker.Entries();
        }

        public DatabaseFacade GetDatabase()
        {
            return Database;
        }

        public void DetachAllEntities()
        {
            var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }


        #endregion

    }
}
