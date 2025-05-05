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
    [Route("api/entree-reactifs")]
    public class EntreeReactifController : ControllerBase
    {
        private readonly IEntreeReactifService _entreeReactifService;

        public EntreeReactifController(IEntreeReactifService entreeReactifService)
        {
            _entreeReactifService = entreeReactifService;
        }

        [HttpGet("total")]
        public async Task<ActionResult<ApiResponse>> GetTotalEntreeReactifs()
        {
            int total = await _entreeReactifService.CountEntreeReactifsAsync();
            return Ok(new ApiResponse
            {
                Data = total,
                ViewBag = null,
                IsSuccess = true,
                Message = "Total des entrées de réactifs récupéré avec succès.",
                StatusCode = 200
            });
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetEntreeReactifs(int position = 1, int pageSize = 5)
        {
            if (position < 1) position = 1;
            if (pageSize < 1) pageSize = 5;

            var entreeReactifs = await _entreeReactifService.GetEntreeReactifsAsync(position, pageSize);
            int total = await _entreeReactifService.CountEntreeReactifsAsync();
            var viewBag = new Dictionary<string, object>
            {
                { "nbrPerPage", pageSize },
                { "TotalCount", total },
                { "nbrLinks", (int)Math.Ceiling((double)total / pageSize) },
                { "position", position }
            };

            return Ok(new ApiResponse
            {
                Data = entreeReactifs,
                ViewBag = viewBag,
                IsSuccess = true,
                Message = "Entrées de réactifs récupérées avec succès.",
                StatusCode = 200
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetEntreeReactif(int id)
        {
            var entreeReactif = await _entreeReactifService.GetEntreeReactifByIdAsync(id);
            if (entreeReactif == null)
            {
                return NotFound(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = "Entrée de réactif non trouvée.",
                    StatusCode = 404
                });
            }

            return Ok(new ApiResponse
            {
                Data = entreeReactif,
                ViewBag = null,
                IsSuccess = true,
                Message = "Entrée de réactif récupérée avec succès.",
                StatusCode = 200
            });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateEntreeReactif([FromBody] EntreeReactifDto entreeReactifDto)
        {
            var createdEntreeReactif = await _entreeReactifService.CreateEntreeReactifAsync(entreeReactifDto);
            return CreatedAtAction(nameof(GetEntreeReactif), new { id = createdEntreeReactif.IdEntreeReactif }, new ApiResponse
            {
                Data = createdEntreeReactif,
                ViewBag = null,
                IsSuccess = true,
                Message = "Entrée de réactif créée avec succès.",
                StatusCode = 201
            });
        }

        [HttpGet("depenses/mois/{annee}")]
        public async Task<ActionResult<ApiResponse>> GetDepensesParMois(int annee)
        {
            var depenses = await _entreeReactifService.GetDepensesParMoisAsync(annee);
            return Ok(new ApiResponse
            {
                Data = depenses,
                ViewBag = null,
                IsSuccess = true,
                Message = "Dépenses des réactifs par mois récupérées avec succès.",
                StatusCode = 200
            });
        }
    }
}