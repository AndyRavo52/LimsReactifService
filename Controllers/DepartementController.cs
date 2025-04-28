using LimsReactifService.Dtos;
using LimsReactifService.Services;
using LimsUtils.Api;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LimsReactifService.Controllers
{
    [ApiController]
    [Route("api/departements")]
    public class DepartementController : ControllerBase
    {
        private readonly IDepartementService _departementService;

        // Injection du service via le constructeur
        public DepartementController(IDepartementService departementService)
        {
            _departementService = departementService;
        }

        // Récupère le nombre total de départements
        [HttpGet("total")]
        public async Task<ActionResult<ApiResponse>> GetTotalDepartements()
        {
            int total = await _departementService.CountDepartementsAsync();
            return Ok(new ApiResponse
            {
                Data = total,
                ViewBag = null,
                IsSuccess = true,
                Message = "Total des départements récupéré avec succès.",
                StatusCode = 200
            });
        }

        // Récupère une liste paginée de départements
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetDepartements(int position = 1, int pageSize = 10)
        {
            if (position < 1) position = 1;
            if (pageSize < 1) pageSize = 10;

            var departements = await _departementService.GetDepartementsAsync(position, pageSize);
            int total = await _departementService.CountDepartementsAsync();
            var viewBag = new Dictionary<string, object>
            {
                { "nbrPerPage", pageSize },
                { "TotalCount", total },
                { "nbrLinks", (int)Math.Ceiling((double)total / pageSize) },
                { "position", position }
            };

            return Ok(new ApiResponse
            {
                Data = departements,
                ViewBag = viewBag,
                IsSuccess = true,
                Message = "Départements récupérés avec succès.",
                StatusCode = 200
            });
        }

        // Récupère un département par son ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetDepartement(int id)
        {
            try
            {
                var departement = await _departementService.GetDepartementByIdAsync(id);
                return Ok(new ApiResponse
                {
                    Data = departement,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Département récupéré avec succès.",
                    StatusCode = 200
                });
            }
            catch (Exception)
            {
                return NotFound(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = "Département non trouvé.",
                    StatusCode = 404
                });
            }
        }
    }
}