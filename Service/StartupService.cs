﻿using AutoMapper;
using GoWheels_WebAPI.Models.Entities;
using GoWheels_WebAPI.Models.ViewModels;

namespace GoWheels_WebAPI.Service
{
    public class StartupService
    {
        private readonly BookingService _bookingService;
        private readonly PostService _postService;
        private readonly IMapper _mapper;

        public StartupService(BookingService bookingService, PostService postService, IMapper mapper)
        {
            _bookingService = bookingService;
            _postService = postService;
            _mapper = mapper;
        }

        public async Task UpdateBookingsOnStartup()
        {
            await _bookingService.UpdateBookingStatus();
        }

        public async Task UpdatePostOnStartup()
        {
            var result = await _bookingService.GetAllAsync();
            var bookings = _mapper.Map<List<Booking>>((List<BookingVM>)result.Data!);
            await _postService.UpdateRideNumberAsync(bookings);
        }
    }
}
