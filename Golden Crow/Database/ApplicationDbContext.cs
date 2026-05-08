using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Golden_Crow.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Golden_Crow.Database
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option): base(option)
        {
        
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            var userEntity = modelBuilder.Entity<User>()
            .ToTable("users");
            userEntity.HasKey(x => x.Id);
            userEntity.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn();
            userEntity.Property(x => x.Login)
                .HasColumnName("login")
                .IsRequired();
            userEntity.Property(x => x.Name)
                .HasColumnName("name")
                .IsRequired();
            userEntity.Property(x=> x.Password)
                .HasColumnName("password")
                .IsRequired();

            SeedUserData(userEntity);

            var accountEntity = modelBuilder.Entity<Account>()
                .ToTable("accounts");
            accountEntity.HasKey(x => x.Id);
            accountEntity.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn();
            accountEntity.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();
            accountEntity.Property(x => x.Balance)
                .HasColumnName("balance")
                .HasPrecision(18, 2)
                .IsRequired();
            accountEntity.Property(x => x.Currency)
                .HasColumnName("currency")
                .IsRequired()
                .HasDefaultValue(Currency.USD);
            accountEntity.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId);

            var sessionEntry = modelBuilder.Entity<Session>()
                .ToTable("sessions");
            sessionEntry.HasKey(x => x.UserId);
            sessionEntry.Property(x => x.UserId)
                .HasColumnName("user_id")
                .IsRequired();
            sessionEntry.Property(x => x.Token)
                .HasColumnName("token")
                .IsRequired();
            sessionEntry.Property(x => x.ExpiresAt)
                .HasColumnName("expires_at")
                .IsRequired();
            sessionEntry.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId);

            var transactionEntity = modelBuilder.Entity<Transaction>()
                .ToTable("transactions");
            transactionEntity.HasKey(x => x.Id);
            transactionEntity.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityColumn();
            transactionEntity.Property(x => x.SenderAccountId)
                .HasColumnName("sender_account_id")
                .IsRequired();
            transactionEntity.Property(x => x.ReceiverAccountId)
                .HasColumnName("receiver_account_id")
                .IsRequired();
            transactionEntity.Property(x => x.CreatedAt)
                .HasColumnName ("created_at")
                .IsRequired();
            transactionEntity.Property(x => x.Currency)
                .HasColumnName("currency")
                .IsRequired()
                .HasDefaultValue(Currency.USD);
            transactionEntity.Property(x => x.Amount)
                .HasColumnName("amount")
                .HasPrecision(18,2)
                .IsRequired();
            transactionEntity.HasOne<Account>()
                .WithMany()
                .HasForeignKey(x => x.SenderAccountId)
                .OnDelete(DeleteBehavior.NoAction);
            transactionEntity.HasOne<Account>()
                .WithMany()
                .HasForeignKey(x => x.ReceiverAccountId)
                .OnDelete(DeleteBehavior.NoAction); ;




        }

        private void SeedUserData(EntityTypeBuilder<User> userEntity)
        {
            userEntity.HasData
             (
                new User
                {   Id = 1,
                    Login = "admin",
                    Name = "Administrator",
                    Password = "admin"
                },


                new User
                {
                    Id = 2,
                    Login = "user",
                    Name = "Regular User",
                    Password = "user"
                }

             );
        }
    }
}
