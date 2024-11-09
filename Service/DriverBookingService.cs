﻿using GoWheels_WebAPI.Models.Entities;
using GoWheels_WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GoWheels_WebAPI.Service
{
    public class DriverBookingService
    {
        private readonly DriverBookingRepository _driverBookingRepository;
        private readonly DriverService _driverService;
        private readonly InvoiceService _invoiceService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _userId;

        public DriverBookingService(DriverBookingRepository driverBookingRepository, 
                                    DriverService driverService,
                                    InvoiceService invoiceService,
                                    IHttpContextAccessor httpContextAccessor)
        {
            _driverBookingRepository = driverBookingRepository;
            _driverService = driverService;
            _invoiceService = invoiceService;
            _httpContextAccessor = httpContextAccessor;
            _userId = _httpContextAccessor.HttpContext?.User?
                        .FindFirstValue(ClaimTypes.NameIdentifier) ?? "UnknownUser";
        }

        public async Task<List<DriverBooking>> GetAllByUserIdAsync()
            => await _driverBookingRepository.GetAllByUserIdAsync(_userId);

        public async Task AddDriverBookingAsync(Booking booking)
        {
            try
            {
                var driver = await _driverService.GetByUserIdAsync(_userId);
                var driverBooking = new DriverBooking()
                {
                    CreatedById = _userId,
                    CreatedOn = DateTime.Now,
                    DriverId = driver.Id,
                    RecieveDate = booking.RecieveOn,
                    ReturnDate = booking.ReturnOn,
                    Total = driver.PricePerHour * (decimal)(booking.ReturnOn - booking.RecieveOn).TotalHours,
                };
                await _driverBookingRepository.AddAsync(driverBooking);
                await _invoiceService.AddDriverToInvocieAsync(booking, driverBooking);
            }
            catch (NullReferenceException nullEx)
            {
                throw new NullReferenceException(nullEx.Message);
            }
            catch (DbUpdateException dbEx)
            {
                throw new DbUpdateException(dbEx.Message);
            }
            catch (InvalidOperationException operationEx)
            {
                throw new InvalidOperationException(operationEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task CancelBookingAsync()
        {
            try
            {

            }
            catch (NullReferenceException nullEx)
            {
                throw new NullReferenceException(nullEx.Message);
            }
            catch (DbUpdateException dbEx)
            {
                throw new DbUpdateException(dbEx.Message);
            }
            catch (InvalidOperationException operationEx)
            {
                throw new InvalidOperationException(operationEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
