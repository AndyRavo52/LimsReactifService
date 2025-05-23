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
    [Route("api/sortie-reactifs")]
    public class SortieReactifController : ControllerBase
    {
        private readonly ISortieReactifService _sortieReactifService;

        // Injection du service via le constructeur
        public SortieReactifController(ISortieReactifService sortieReactifService)
        {
            _sortieReactifService = sortieReactifService;
        }

        // Récupère le nombre total de sorties de réactifs
        [HttpGet("total")]
        public async Task<ActionResult<ApiResponse>> GetTotalSortieReactifs()
        {
            int total = await _sortieReactifService.CountSortieReactifsAsync();
            return Ok(new ApiResponse
            {
                Data = total,
                ViewBag = null,
                IsSuccess = true,
                Message = "Total des sorties de réactifs récupéré avec succès.",
                StatusCode = 200
            });
        }

        // Récupère une liste paginée de sorties de réactifs
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetSortieReactifs(int position = 1, int pageSize = 5)
        {
            if (position < 1) position = 1;
            if (pageSize < 1) pageSize = 5;

            var sortieReactifs = await _sortieReactifService.GetSortieReactifsAsync(position, pageSize);
            int total = await _sortieReactifService.CountSortieReactifsAsync();
            var viewBag = new Dictionary<string, object>
            {
                { "nbrPerPage", pageSize },
                { "TotalCount", total },
                { "nbrLinks", (int)Math.Ceiling((double)total / pageSize) },
                { "position", position }
            };

            return Ok(new ApiResponse
            {
                Data = sortieReactifs,
                ViewBag = viewBag,
                IsSuccess = true,
                Message = "Sorties de réactifs récupérées avec succès.",
                StatusCode = 200
            });
        }

        // Récupère une sortie de réactif par son ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetSortieReactif(int id)
        {
            try
            {
                var sortieReactif = await _sortieReactifService.GetSortieReactifByIdAsync(id);
                return Ok(new ApiResponse
                {
                    Data = sortieReactif,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Sortie de réactif récupérée avec succès.",
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
                    Message = "Sortie de réactif non trouvée.",
                    StatusCode = 404
                });
            }
        }

        // Crée une nouvelle sortie de réactif
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateSortieReactif([FromBody] SortieReactifDto sortieReactifDto)
        {
            try
            {
                var createdSortieReactif = await _sortieReactifService.CreateSortieReactifAsync(sortieReactifDto);
                return CreatedAtAction(nameof(GetSortieReactif), new { id = createdSortieReactif.IdSortie }, new ApiResponse
                {
                    Data = createdSortieReactif,
                    ViewBag = null,
                    IsSuccess = true,
                    Message = "Sortie de réactif créée avec succès.",
                    StatusCode = 201
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = 400
                });
            }
        }

        
    }
}