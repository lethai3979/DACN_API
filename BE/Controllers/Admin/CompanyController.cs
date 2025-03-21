﻿using AutoMapper;
using GoWheels_WebAPI.Models.DTOs;
using GoWheels_WebAPI.Models.Entities;
using GoWheels_WebAPI.Models.ViewModels;
using GoWheels_WebAPI.Service;
using GoWheels_WebAPI.Service.Interface;
using GoWheels_WebAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoWheels_WebAPI.Controllers
{
    [Area("Admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public ActionResult<OperationResult> GetAll()
        {
            try
            {
                var companies = _companyService.GetAll();
                var companiesVMs = _mapper.Map<List<CompanyVM>>(companies);
                return new OperationResult(true, statusCode: StatusCodes.Status200OK, data: companiesVMs);
            }
            catch (NullReferenceException nullEx)
            {
                return new OperationResult(false, nullEx.Message, StatusCodes.Status204NoContent);
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
                var company = _companyService.GetById(id);
                var companyVM = _mapper.Map<CompanyVM>(company);
                return new OperationResult(true, statusCode: StatusCodes.Status200OK, data: companyVM);
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

        [Authorize(Roles = "Admin")]
        [HttpPost("Add")]
        public ActionResult<OperationResult> Add([FromForm] CompanyDTO companyDTO)
        {
            try
            {
                if (companyDTO == null)
                {
                    return BadRequest("Company value is null");
                }
                if (ModelState.IsValid)
                {
                    var company = _mapper.Map<Company>(companyDTO);
                    _companyService.Add(company, companyDTO.CarTypeIds, companyDTO.IconImage!);
                    return new OperationResult(true, "Company add succesfully", StatusCodes.Status200OK);
                }
                return BadRequest("Company value invalid");
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

        [HttpPost("DeleteById/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<OperationResult> Delete(int id)
        {
            try
            {
                _companyService.DeleteById(id);
                return new OperationResult(true, "Company deleted succesfully", StatusCodes.Status200OK);
            }
            catch (NullReferenceException nullEx)
            {
                return new OperationResult(false, nullEx.Message, StatusCodes.Status204NoContent);
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
        public ActionResult<OperationResult> Update(int id, [FromForm] CompanyDTO companyDTO)
        {
            try
            {
                if (companyDTO == null || id != companyDTO.Id)
                {
                    return BadRequest("Invalid request");
                }
                if (ModelState.IsValid)
                {
                    var company = _mapper.Map<Company>(companyDTO);
                    _companyService.Update(id, company, companyDTO.CarTypeIds, companyDTO.IconImage!);
                    return new OperationResult(true, "Company update succesfully", StatusCodes.Status200OK);
                }
                return BadRequest("Company value invalid");
            }
            catch (NullReferenceException nullEx)
            {
                return new OperationResult(false, nullEx.Message, StatusCodes.Status204NoContent);
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
