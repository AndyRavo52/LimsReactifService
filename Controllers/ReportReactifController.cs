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
    [Route("api/report-reactif")]
    public class ReportReactifController : ControllerBase
    {
        private readonly IReportReactifService _reportReactifService;

        // Injection du service via le constructeur
        public ReportReactifController(IReportReactifService reportReactifService)
        {
            _reportReactifService = reportReactifService;
        }

        // Récupère le nombre total de rapports de réactifs
        [HttpGet("total")]
        public async Task<ActionResult<ApiResponse>> GetTotalReportReactifs()
        {
            int total = await _reportReactifService.CountReportReactifsAsync();
            return Ok(new ApiResponse
            {
                Data = total,
                ViewBag = null,
                IsSuccess = true,
                Message = "Total des rapports de réactifs récupéré avec succès.",
                StatusCode = 200
            });
        }

        // Récupère une liste paginée de rapports de réactifs
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetReportReactifs(int position = 1, int pageSize = 10)
        {
            if (position < 1) position = 1;
            if (pageSize < 1) pageSize = 10;

            var reportReactifs = await _reportReactifService.GetReportReactifsAsync(position, pageSize);
            int total = await _reportReactifService.CountReportReactifsAsync();
            var viewBag = new Dictionary<string, object>
            {
                { "nbrPerPage", pageSize },
                { "TotalCount", total },
                { "nbrLinks", (int)Math.Ceiling((double)total / pageSize) },
                { "position", position }
            };

            return Ok(new ApiResponse
            {
                Data = reportReactifs,
                ViewBag = viewBag,
                IsSuccess = true,
                Message = "Rapports de réactifs récupérés avec succès.",
                StatusCode = 200
            });
        }

        // Récupère un rapport de réactif par son ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetReportReactif(int id)
        {
            try
            {
                var reportReactif = await _reportReactifService.GetReportReactifByIdAsync(id);
                return Ok(new ApiResponse
                {
                    Data = reportReactif,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Rapport de réactif récupéré avec succès.",
                    StatusCode = 200
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = 404
                });
            }
        }

        // Crée un nouveau rapport de réactif
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateReportReactif([FromBody] ReportReactifDto reportReactifDto)
        {
            var createdReportReactif = await _reportReactifService.CreateReportReactifAsync(reportReactifDto);
            return CreatedAtAction(nameof(GetReportReactif), new { id = createdReportReactif.IdReportReactif }, new ApiResponse
            {
                Data = createdReportReactif,
                ViewBag = null,
                IsSuccess = true,
                Message = "Rapport de réactif créé avec succès.",
                StatusCode = 201
            });
        }

    }
}