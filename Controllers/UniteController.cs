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
    [Route("api/unites")]
    public class UnitesController : ControllerBase
    {
        private readonly IUniteService _uniteService;

        // Injection du service via le constructeur
        public UnitesController(IUniteService uniteService)
        {
            _uniteService = uniteService;
        }

        // Récupère le nombre total d'unités
        [HttpGet("total")]
        public async Task<ActionResult<ApiResponse>> GetTotalUnites()
        {
            int total = await _uniteService.CountUnitesAsync();
            return Ok(new ApiResponse
            {
                Data = total,
                ViewBag = null,
                IsSuccess = true,
                Message = "Total des unités récupéré avec succès.",
                StatusCode = 200
            });
        }

        // Récupère une liste paginée d'unités
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetUnites(int position = 1, int pageSize = 10)
        {
            if (position < 1) position = 1;
            if (pageSize < 1) pageSize = 10;

            var unites = await _uniteService.GetUnitesAsync(position, pageSize);

            int total = await _uniteService.CountUnitesAsync();
            var viewBag = new Dictionary<string, object>
            {
                { "nbrPerPage", pageSize },
                { "TotalCount", total },
                { "nbrLinks", (int)Math.Ceiling((double)total / pageSize) },
                { "position", position }
            };

            return Ok(new ApiResponse
            {
                Data = unites,
                ViewBag = viewBag,
                IsSuccess = true,
                Message = "Unités récupérées avec succès.",
                StatusCode = 200
            });
        }

        // Récupère une unité par son ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetUnite(int id)
        {
            var unite = await _uniteService.GetUniteByIdAsync(id);
            if (unite == null)
            {
                return NotFound(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = "Unité non trouvée.",
                    StatusCode = 404
                });
            }

            return Ok(new ApiResponse
            {
                Data = unite,
                ViewBag = null,
                IsSuccess = true,
                Message = "Unité récupérée avec succès.",
                StatusCode = 200
            });
        }

        // Crée une nouvelle unité
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateUnite([FromBody] UniteDto uniteDto)
        {
            var createdUnite = await _uniteService.CreateUniteAsync(uniteDto);
            return CreatedAtAction(nameof(GetUnite), new { id = createdUnite.IdUnite }, new ApiResponse
            {
                Data = createdUnite,
                ViewBag = null,
                IsSuccess = true,
                Message = "Unité créée avec succès.",
                StatusCode = 201
            });
        }

        // Met à jour une unité existante
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateUnite(int id, [FromBody] UniteDto uniteDto)
        {
            if (id != uniteDto.IdUnite)
            {
                return BadRequest(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = "ID non correspondants.",
                    StatusCode = 400
                });
            }

            var updatedUnite = await _uniteService.UpdateUniteAsync(id, uniteDto);
            return Ok(new ApiResponse
            {
                Data = updatedUnite,
                ViewBag = null,
                IsSuccess = true,
                Message = "Unité mise à jour avec succès.",
                StatusCode = 200
            });
        }

        // Supprime une unité
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteUnite(int id)
        {
            bool result = await _uniteService.DeleteUniteAsync(id);
            if (!result)
            {
                return NotFound(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = "Unité non trouvée.",
                    StatusCode = 404
                });
            }

            return NoContent();
        }
    }
}
