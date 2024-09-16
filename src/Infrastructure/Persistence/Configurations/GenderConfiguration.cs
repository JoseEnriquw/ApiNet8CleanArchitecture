using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Enums;

namespace Infrastructure.Persistence.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Description).HasMaxLength(100).IsRequired();

            builder.HasData(
                [
                new Gender(){
                    Id = (int)EGender.Male,
                    Description=EGender.Male.ToString()
                },
                new Gender(){
                    Id= (int)EGender.Female,
                    Description=EGender.Female.ToString()
                }
                ]);
        }
    }
}
