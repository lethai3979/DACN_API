﻿using GoWheels_WebAPI.Models.Entities;

namespace GoWheels_WebAPI.Service.Interface
{
    public interface IUserService
    {
        List<ApplicationUser> GetAllDriverSubmit();
        List<ApplicationUser> GetAllUser();
        Task<ApplicationUser> GetByUserIdAsync();
        Task<ApplicationUser> GetByUserIdAsync(string userId);
        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        Task UpdateUserInfoAsync(ApplicationUser user, IFormFile License, IFormFile CIC, IFormFile Image);
        Task UpdateUserReportPointAsync(string userId, int reportPoint);
        Task UpdateUserLockAccountAsync(string userId);
        Task UpdateDriverLocationAsync(string longitude, string latitude);
        Task SendDriverSubmitAsync();
        Task ConfirmDriverSubmit(string userId, bool isAccept);


    }
}
