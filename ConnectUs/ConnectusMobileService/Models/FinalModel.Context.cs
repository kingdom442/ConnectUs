﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConnectusMobileService.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ConnectusDBEntities : DbContext
    {
        public ConnectusDBEntities()
            : base("name=ConnectusDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<ConnectRequests> ConnectRequests { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<Networks> Networks { get; set; }
        public virtual DbSet<UserComparisons> UserComparisons { get; set; }
        public virtual DbSet<UserContacts> UserContacts { get; set; }
        public virtual DbSet<UserContexts> UserContexts { get; set; }
        public virtual DbSet<UserInfoDetails> UserInfoDetails { get; set; }
        public virtual DbSet<UserInfoes> UserInfoes { get; set; }
    }
}
