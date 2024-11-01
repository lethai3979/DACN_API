﻿using GoWheels_WebAPI.Models.Entities;

namespace GoWheels_WebAPI.Models.ViewModels
{
    public class BookingVM : BaseModelVM
    {
        public decimal PrePayment { get; set; }
        public decimal Total { get; set; }
        public decimal FinalValue { get; set; }
        public DateTime RecieveOn { get; set; }
        public DateTime ReturnOn { get; set; }
        public string? Status { get; set; }
        public bool IsRequest { get; set; }
        public bool IsResponse { get; set; }
        public bool IsPay { get; set; }
        public string? UserId { get; set; }
        public PostVM Post { get; set; } = null!;
        public PromotionVM PromotionVM { get; set; } = null!;
    }
}
