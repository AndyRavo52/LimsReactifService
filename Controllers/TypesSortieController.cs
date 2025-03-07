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
    [Route("api/typesSortie")]
    public class TypesSortieController : ControllerBase
    {
        private readonly ITypeSortieService _typeSortieService;

        // Injection du service via le constructeur
        public TypesSortieController(ITypeSortieService typeSortieService)
        {
            _typeSortieService = typeSortieService;
        }

        // Récupère le nombre total de types de sortie
        [HttpGet("total")]
        public async Task<ActionResult<ApiResponse>> GetTotalTypesSortie()
        {
            int total = await _typeSortieService.CountTypesSortieAsync();
            return Ok(new ApiResponse
            {
                Data = total,
                ViewBag = null,
                IsSuccess = true,
                Message = "Total des types de sortie récupéré avec succès.",
                StatusCode = 200
            });
        }

        // Récupère une liste paginée de types de sortie
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetTypesSortie(int position = 1, int pageSize = 10)
        {
            if (position < 1) position = 1; // Position minimale : 1
            if (pageSize < 1) pageSize = 10; // Taille de page minimale : 1, valeur par défaut : 10

            // Récupère les données paginées
            var typesSortie = await _typeSortieService.GetTypesSortieAsync(position, pageSize);

            // Calcule les informations de pagination
            int total = await _typeSortieService.CountTypesSortieAsync();
            var viewBag = new Dictionary<string, object>
            {
                { "nbrPerPage", pageSize },
                { "TotalCount", total },
                { "nbrLinks", (int)Math.Ceiling((double)total / pageSize) },
                { "position", position }
            };

            return Ok(new ApiResponse
            {
                Data = typesSortie,
                ViewBag = viewBag,
                IsSuccess = true,
                Message = "Types de sortie récupérés avec succès.",
                StatusCode = 200
            });
        }

        // Récupère un type de sortie par son ID
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetTypeSortie(int id)
        {
            var typeSortie = await _typeSortieService.GetTypeSortieByIdAsync(id);
            if (typeSortie == null)
            {
                return NotFound(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = "Type de sortie non trouvé.",
                    StatusCode = 404
                });
            }

            return Ok(new ApiResponse
            {
                Data = typeSortie,
                ViewBag = null,
                IsSuccess = true,
                Message = "Type de sortie récupéré avec succès.",
                StatusCode = 200
            });
        }

        // Crée un nouveau type de sortie
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateTypeSortie([FromBody] TypeSortieDto typeSortieDto)
        {
            var createdTypeSortie = await _typeSortieService.CreateTypeSortieAsync(typeSortieDto);
            return CreatedAtAction(nameof(GetTypeSortie), new { id = createdTypeSortie.IdTypeSortie }, new ApiResponse
            {
                Data = createdTypeSortie,
                ViewBag = null,
                IsSuccess = true,
                Message = "Type de sortie créé avec succès.",
                StatusCode = 201
            });
        }

        // Met à jour un type de sortie existant
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateTypeSortie(int id, [FromBody] TypeSortieDto typeSortieDto)
        {
            if (id != typeSortieDto.IdTypeSortie)
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

            var updatedTypeSortie = await _typeSortieService.UpdateTypeSortieAsync(id, typeSortieDto);
            return Ok(new ApiResponse
            {
                Data = updatedTypeSortie,
                ViewBag = null,
                IsSuccess = true,
                Message = "Type de sortie mis à jour avec succès.",
                StatusCode = 200
            });
        }

        // Supprime un type de sortie
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteTypeSortie(int id)
        {
            bool result = await _typeSortieService.DeleteTypeSortieAsync(id);
            if (!result)
            {
                return NotFound(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = "Type de sortie non trouvé.",
                    StatusCode = 404
                });
            }

            return NoContent();
        }
    }
}
