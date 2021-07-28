﻿using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Persistence.Interfaces
{
    public interface IAdvertoDbContext
    {
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ViewedAd> ViewedAds { get; set; }
        public DbSet<DisabledAd> DisabledAds { get; set; }
    }
}