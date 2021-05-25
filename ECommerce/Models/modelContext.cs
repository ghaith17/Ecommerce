namespace ECommerce.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class modelContext : DbContext
    {
        // Your context has been configured to use a 'modelContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ECommerce.Models.modelContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'modelContext' 
        // connection string in the application configuration file.
        private static modelContext instance ;
        private static Object Lock= new Object();

        static modelContext() //: base("name=modelContext")
        {
            DbContext dbContext= new DbContext("name=modelContext");
            getInstance();
        }
        //private modelContext()
        //    : base("name=modelContext")
        //{

        //}

        public static modelContext getInstance()
        {
            if (instance == null)
            {
                lock(Lock) {
                    if (instance == null)
                    {
                        instance = new modelContext();
                    }
                }
            }
            return instance;
        }
    
        public DbSet<Bill> Bills { get; set; }
        public DbSet<DeliverySupplier> DeliverySuppliers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<VirtualWallet> VirtualWallets { get; set; }
        public DbSet<FeedBack> feedBacks { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}