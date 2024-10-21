﻿using GoWheels_WebAPI.Data;
using GoWheels_WebAPI.Models.Entities;
using GoWheels_WebAPI.Models.ViewModels;
using GoWheels_WebAPI.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoWheels_WebAPI.Repositories
{
    public class PostPromotionReposity : IGenericRepository<PostPromotion>
    {
        private readonly ApplicationDbContext _context;

        public PostPromotionReposity(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PostPromotion postPromotion)
        {
            await _context.AddAsync(postPromotion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PostPromotion postPromotion)
        {
            _context.Entry(postPromotion).State = EntityState.Modified;
            postPromotion.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<List<PostPromotion>> GetAllAsync()
            => await _context.PostPromotions.ToListAsync();

        public async Task<PostPromotion> GetByIdAsync(int id)
            => await _context.PostPromotions
                        .FirstOrDefaultAsync(p => p.Id == id)
                        ?? throw new NullReferenceException("Post promotion not found");

        public async Task UpdateAsync(PostPromotion postPromotion)
        {
            var existingPostPromotion = _context.ChangeTracker.Entries<Amenity>()
                                                 .FirstOrDefault(e => e.Entity.Id == postPromotion.Id);

            //detached same id obj
            if (existingPostPromotion != null)
            {
                _context.Entry(existingPostPromotion.Entity).State = EntityState.Detached;
            }

            _context.Entry(postPromotion).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //detached tracking obj after modified
            _context.Entry(postPromotion).State = EntityState.Detached;
        }
    }
}
