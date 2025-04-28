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
    [Route("api/objetSortieReactif")]
    public class ObjetSortieReactifController : ControllerBase
    {
        private readonly IObjetSortieReactifService _objetSortieReactifService;

        public ObjetSortieReactifController(IObjetSortieReactifService objetSortieReactifService)
        {
            _objetSortieReactifService = objetSortieReactifService;
        }

        [HttpGet("total")]
        public async Task<ActionResult<ApiResponse>> GetTotalObjetsSortieReactif()
        {
            int total = await _objetSortieReactifService.CountObjetsSortieReactifAsync();
            return Ok(new ApiResponse
            {
                Data = total,
                ViewBag = null,
                IsSuccess = true,
                Message = "Total des objets de sortie réactif récupéré avec succès.",
                StatusCode = 200
            });
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetObjetsSortieReactif(int position = 1, int pageSize = 10)
        {
            if (position < 1) position = 1;
            if (pageSize < 1) pageSize = 10;

            var objetsSortieReactif = await _objetSortieReactifService.GetObjetsSortieReactifAsync(position, pageSize);
            int total = await _objetSortieReactifService.CountObjetsSortieReactifAsync();
            var viewBag = new Dictionary<string, object>
            {
                { "nbrPerPage", pageSize },
                { "TotalCount", total },
                { "nbrLinks", (int)Math.Ceiling((double)total / pageSize) },
                { "position", position }
            };

            return Ok(new ApiResponse
            {
                Data = objetsSortieReactif,
                ViewBag = viewBag,
                IsSuccess = true,
                Message = "Objets de sortie réactif récupérés avec succès.",
                StatusCode = 200
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetObjetSortieReactif(int id)
        {
            var objetSortieReactif = await _objetSortieReactifService.GetObjetSortieReactifByIdAsync(id);
            if (objetSortieReactif == null)
            {
                return NotFound(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = "Objet de sortie réactif non trouvé.",
                    StatusCode = 404
                });
            }

            return Ok(new ApiResponse
            {
                Data = objetSortieReactif,
                ViewBag = null,
                IsSuccess = true,
                Message = "Objet de sortie réactif récupéré avec succès.",
                StatusCode = 200
            });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateObjetSortieReactif([FromBody] ObjetSortieReactifDto objetSortieReactifDto)
        {
            var createdObjetSortieReactif = await _objetSortieReactifService.CreateObjetSortieReactifAsync(objetSortieReactifDto);
            return CreatedAtAction(nameof(GetObjetSortieReactif), new { id = createdObjetSortieReactif.IdObjetSortieReactif }, new ApiResponse
            {
                Data = createdObjetSortieReactif,
                ViewBag = null,
                IsSuccess = true,
                Message = "Objet de sortie réactif créé avec succès.",
                StatusCode = 201
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateObjetSortieReactif(int id, [FromBody] ObjetSortieReactifDto objetSortieReactifDto)
        {
            if (id != objetSortieReactifDto.IdObjetSortieReactif)
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

            var updatedObjetSortieReactif = await _objetSortieReactifService.UpdateObjetSortieReactifAsync(id, objetSortieReactifDto);
            return Ok(new ApiResponse
            {
                Data = updatedObjetSortieReactif,
                ViewBag = null,
                IsSuccess = true,
                Message = "Objet de sortie réactif mis à jour avec succès.",
                StatusCode = 200
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteObjetSortieReactif(int id)
        {
            bool result = await _objetSortieReactifService.DeleteObjetSortieReactifAsync(id);
            if (!result)
            {
                return NotFound(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = "Objet de sortie réactif non trouvé.",
                    StatusCode = 404
                });
            }

            return NoContent();
        }
    }
}
