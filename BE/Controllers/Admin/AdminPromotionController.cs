﻿using AutoMapper;
using GoWheels_WebAPI.Models.DTOs;
using GoWheels_WebAPI.Models.Entities;
using GoWheels_WebAPI.Models.ViewModels;
using GoWheels_WebAPI.Service.Interface;
using GoWheels_WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoWheels_WebAPI.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdminPromotionController : ControllerBase
    {
        private readonly IAdminPromotionService _promotionService;
        private readonly IMapper _mapper;

        public AdminPromotionController(IAdminPromotionService promotionService, IMapper mapper)
        {
            _promotionService = promotionService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public ActionResult<OperationResult> GetAll()
        {
            try
            {
                var promotions = _promotionService.GetAll();
                var promotionVMs = _mapper.Map<List<PromotionVM>>(promotions);
                return new OperationResult(true, statusCode: StatusCodes.Status200OK, data: promotionVMs);
            }
            catch (NullReferenceException aEx)
            {
                return new OperationResult(false, aEx.Message, StatusCodes.Status204NoContent);
            }
            catch (AutoMapperMappingException mapperEx)
            {
                return new OperationResult(false, mapperEx.Message, StatusCodes.Status422UnprocessableEntity);
            }
            catch (Exception ex)
            {
                var exMessage = ex.Message ?? "An error occurred while updating the database.";
                return new OperationResult(false, exMessage, StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet("GetAllAdminPromotion")]
        [Authorize(Roles = "Admin")]// Admin lấy ds promotion của admin
        public ActionResult<OperationResult> GetAllAdminPromotions()
        {
            try
            {
                var promotions = _promotionService.GetAllAdminPromotions();
                var promotionVMs = _mapper.Map<List<PromotionVM>>(promotions);
                return new OperationResult(true, statusCode: StatusCodes.Status200OK, data: promotionVMs);
            }
            catch (NullReferenceException aEx)
            {
                return new OperationResult(false, aEx.Message, StatusCodes.Status204NoContent);
            }
            catch (AutoMapperMappingException mapperEx)
            {
                return new OperationResult(false, mapperEx.Message, StatusCodes.Status422UnprocessableEntity);
            }
            catch (Exception ex)
            {
                var exMessage = ex.Message ?? "An error occurred while updating the database.";
                return new OperationResult(false, exMessage, StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet("GetAllAdminPromotionByUserId")]// User lấy admin promotion trong chi tiết bài đăng
        [Authorize]
        public ActionResult<OperationResult> GetAllAdminPromotionsByUserId()
        {
            try
            {
                var promotions = _promotionService.GetAllAdminPromotionsByUserId();
                var promotionVMs = _mapper.Map<List<PromotionVM>>(promotions);
                return new OperationResult(true, statusCode: StatusCodes.Status200OK, data: promotionVMs);
            }
            catch (NullReferenceException aEx)
            {
                return new OperationResult(false, aEx.Message, StatusCodes.Status204NoContent);
            }
            catch (AutoMapperMappingException mapperEx)
            {
                return new OperationResult(false, mapperEx.Message, StatusCodes.Status422UnprocessableEntity);
            }
            catch (Exception ex)
            {
                var exMessage = ex.Message ?? "An error occurred while updating the database.";
                return new OperationResult(false, exMessage, StatusCodes.Status400BadRequest);
            }
        }

        [HttpGet("GetById/{id}")]
        public ActionResult<OperationResult> GetById(int id)
        {
            try
            {
                var promotion = _promotionService.GetById(id);
                var promotionVM = _mapper.Map<PromotionVM>(promotion);
                return new OperationResult(true, statusCode: StatusCodes.Status200OK, data: promotionVM);
            }
            catch (NullReferenceException aEx)
            {
                return new OperationResult(false, aEx.Message, StatusCodes.Status204NoContent);
            }
            catch (AutoMapperMappingException mapperEx)
            {
                return new OperationResult(false, mapperEx.Message, StatusCodes.Status422UnprocessableEntity);
            }
            catch (Exception ex)
            {
                var exMessage = ex.Message ?? "An error occurred while updating the database.";
                return new OperationResult(false, exMessage, StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]
        public ActionResult<OperationResult> Add([FromForm] PromotionDTO promotionDTO)
        {
            try
            {
                if (promotionDTO == null)
                {
                    return BadRequest("Promotion value is null");
                }
                if (ModelState.IsValid)
                {
                    if (promotionDTO.ExpiredDate > DateTime.Now)
                    {
                        var promotion = _mapper.Map<Promotion>(promotionDTO);
                        _promotionService.Add(promotion);
                        return new OperationResult(true, "Promotion add succesfully", StatusCodes.Status200OK);
                    }
                    return new OperationResult(false, "Expire date invalid", StatusCodes.Status400BadRequest);
                }
                return BadRequest("Promotion data invalid");
            }
            catch (UnauthorizedAccessException authEx)
            {
                return new OperationResult(false, authEx.Message, StatusCodes.Status401Unauthorized);
            }
            catch (DbUpdateException dbEx)
            {
                return new OperationResult(false, dbEx.Message, StatusCodes.Status500InternalServerError);
            }
            catch (InvalidOperationException operationEx)
            {
                return new OperationResult(false, operationEx.Message, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, StatusCodes.Status400BadRequest);
            }
        }

        [HttpPut("Update/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<OperationResult> Update(int id, [FromForm] PromotionDTO promotionDTO)
        {
            try
            {
                if (promotionDTO == null || id != promotionDTO.Id)
                {
                    return BadRequest("Invalid request");
                }
                if (ModelState.IsValid)
                {
                    if (promotionDTO.ExpiredDate > DateTime.Now)
                    {
                        var promotion = _mapper.Map<Promotion>(promotionDTO);
                        _promotionService.Update(id, promotion);
                        return new OperationResult(true, "Promotion update succesfully", StatusCodes.Status200OK);
                    }
                    return new OperationResult(false, "Expire date invalid", StatusCodes.Status400BadRequest);
                }
                return BadRequest("Promotion data invalid");
            }
            catch (DbUpdateException dbEx)
            {
                return new OperationResult(false, dbEx.Message, StatusCodes.Status500InternalServerError);
            }
            catch (InvalidOperationException operationEx)
            {
                return new OperationResult(false, operationEx.Message, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, StatusCodes.Status400BadRequest);
            }

        }

        [HttpPost("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<OperationResult> Delete(int id)
        {
            try
            {
                _promotionService.DeletedById(id);
                return new OperationResult(true, "Promotion deleted succesfully", StatusCodes.Status200OK);
            }
            catch (DbUpdateException dbEx)
            {
                return new OperationResult(false, dbEx.Message, StatusCodes.Status500InternalServerError);
            }
            catch (InvalidOperationException operationEx)
            {
                return new OperationResult(false, operationEx.Message, StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, StatusCodes.Status400BadRequest);
            }

        }
    }
}
