﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdminPart.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TMS1Entities : DbContext
    {
        public TMS1Entities()
            : base("name=TMS1Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Advertiser> Advertisers { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
