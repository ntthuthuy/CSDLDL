using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechLife.Data.Entities;

namespace TechLife.Data.Configurations
{
    public class OrderDetailConfigruation : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.DichVuId).IsRequired();
            builder.Property(x => x.OrderId).IsRequired();

        }
    }
}