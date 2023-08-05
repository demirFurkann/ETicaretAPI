using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Context
{
    public class ETicaretAPIDBContext : DbContext
    {
        public ETicaretAPIDBContext(DbContextOptions options) : base(options)
        {

        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker: Entityler üzerinde yapılan değişikliklerin yada yeni eklenen verinin yakalanmasını sağlayan propertydir.Update operasyonlarında Track edilen verileri yakalayip elde etmemizi sağlar..
            //Entries() buda gelen girdleri yakalamaya yariyor

            var datas = ChangeTracker
                    .Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdateDate = DateTime.UtcNow,
                }; 
            }
            
            return await base.SaveChangesAsync(cancellationToken);
        }


        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }


    }
}
