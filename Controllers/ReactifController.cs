// using LimsReactifService.Dtos;
// using LimsReactifService.Services;
// using LimsUtils.Api;
// using Microsoft.AspNetCore.Mvc;
// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;

// namespace LimsReactifService.Controllers
// {
//     [ApiController]
//     [Route("api/reactifs")]
//     public class ReactifController : ControllerBase
//     {
//         private readonly IReactifService _reactifService;

//         public ReactifController(IReactifService reactifService)
//         {
//             _reactifService = reactifService;
//         }

//         // Récupère le nombre total de réactifs
//         [HttpGet("total")]
//         public async Task<ActionResult<ApiResponse>> GetTotalReactifs()
//         {
//             int total = await _reactifService.CountReactifsAsync();
//             return Ok(new ApiResponse
//             {
//                 Data = total,
//                 ViewBag = null,
//                 IsSuccess = true,
//                 Message = "Total réactifs retrieved successfully.",
//                 StatusCode = 200
//             });
//         }

//         // Récupère une liste paginée de réactifs
//         [HttpGet]
//         public async Task<ActionResult<ApiResponse>> GetReactifs(int position = 1, int pageSize = 10)
//         {
//             if (position < 1) position = 1;
//             if (pageSize < 1) pageSize = 10;

//             var reactifs = await _reactifService.GetReactifsAsync(position, pageSize);
//             int total = await _reactifService.CountReactifsAsync();
//             var viewBag = new Dictionary<string, object>
//             {
//                 { "nbrPerPage", pageSize },
//                 { "TotalCount", total },
//                 { "nbrLinks", (int)Math.Ceiling((double)total / pageSize) },
//                 { "position", position }
//             };

//             return Ok(new ApiResponse
//             {
//                 Data = reactifs,
//                 ViewBag = viewBag,
//                 IsSuccess = true,
//                 Message = "Reactifs retrieved successfully.",
//                 StatusCode = 200
//             });
//         }

//         // Endpoint de recherche de réactifs
//         [HttpGet("search")]
// public async Task<ActionResult<ApiResponse>> SearchReactifs([FromQuery] string searchTerm = "")
// {
//     var results = await _reactifService.SearchReactifsAsync(searchTerm);
//     return Ok(new ApiResponse
//     {
//         Data = results,
//         ViewBag = null,
//         IsSuccess = true,
//         Message = "Recherche effectuée avec succès.",
//         StatusCode = 200
//     });
// }


//         // Récupère un réactif par son ID
//         [HttpGet("{id}")]
//         public async Task<ActionResult<ApiResponse>> GetReactif(int id)
//         {
//             var reactif = await _reactifService.GetReactifByIdAsync(id);
//             if (reactif == null)
//             {
//                 return NotFound(new ApiResponse
//                 {
//                     Data = null,
//                     ViewBag = null,
//                     IsSuccess = false,
//                     Message = "Reactif not found.",
//                     StatusCode = 404
//                 });
//             }

//             return Ok(new ApiResponse
//             {
//                 Data = reactif,
//                 ViewBag = null,
//                 IsSuccess = true,
//                 Message = "Reactif retrieved successfully.",
//                 StatusCode = 200
//             });
//         }

//         // Crée un nouveau réactif
//         [HttpPost]
//         public async Task<ActionResult<ApiResponse>> CreateReactif([FromBody] ReactifDto reactifDto)
//         {
//             var createdReactif = await _reactifService.CreateReactifAsync(reactifDto);
//             return CreatedAtAction(nameof(GetReactif), new { id = createdReactif.IdReactif }, new ApiResponse
//             {
//                 Data = createdReactif,
//                 ViewBag = null,
//                 IsSuccess = true,
//                 Message = "Reactif created successfully.",
//                 StatusCode = 201
//             });
//         }

//         // Met à jour un réactif existant
//         [HttpPut("{id}")]
//         public async Task<ActionResult<ApiResponse>> UpdateReactif(int id, [FromBody] ReactifDto reactifDto)
//         {
//             if (id != reactifDto.IdReactif)
//             {
//                 return BadRequest(new ApiResponse
//                 {
//                     Data = null,
//                     ViewBag = null,
//                     IsSuccess = false,
//                     Message = "ID mismatch.",
//                     StatusCode = 400
//                 });
//             }

//             var updatedReactif = await _reactifService.UpdateReactifAsync(id, reactifDto);
//             return Ok(new ApiResponse
//             {
//                 Data = updatedReactif,
//                 ViewBag = null,
//                 IsSuccess = true,
//                 Message = "Reactif updated successfully.",
//                 StatusCode = 200
//             });
//         }

//         // Supprime un réactif
//         [HttpDelete("{id}")]
//         public async Task<ActionResult> DeleteReactif(int id)
//         {
//             bool result = await _reactifService.DeleteReactifAsync(id);
//             if (!result)
//             {
//                 return NotFound(new ApiResponse
//                 {
//                     Data = null,
//                     ViewBag = null,
//                     IsSuccess = false,
//                     Message = "Reactif not found.",
//                     StatusCode = 404
//                 });
//             }

//             return NoContent();
//         }
//     }
// }

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
    [Route("api/reactifs")]
    public class ReactifController : ControllerBase
    {
        private readonly IReactifService _reactifService;

        public ReactifController(IReactifService reactifService)
        {
            _reactifService = reactifService;
        }

        [HttpGet("total")]
        public async Task<ActionResult<ApiResponse>> GetTotalReactifs()
        {
            int total = await _reactifService.CountReactifsAsync();
            return Ok(new ApiResponse
            {
                Data = total,
                ViewBag = null,
                IsSuccess = true,
                Message = "Total réactifs retrieved successfully.",
                StatusCode = 200
            });
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetReactifs(int position = 1, int pageSize = 10)
        {
            if (position < 1) position = 1;
            if (pageSize < 1) pageSize = 10;

            var reactifs = await _reactifService.GetReactifsAsync(position, pageSize);
            int total = await _reactifService.CountReactifsAsync();
            var viewBag = new Dictionary<string, object>
            {
                { "nbrPerPage", pageSize },
                { "TotalCount", total },
                { "nbrLinks", (int)Math.Ceiling((double)total / pageSize) },
                { "position", position }
            };

            return Ok(new ApiResponse
            {
                Data = reactifs,
                ViewBag = viewBag,
                IsSuccess = true,
                Message = "Reactifs retrieved successfully.",
                StatusCode = 200
            });
        }

        [HttpGet("search")]
        public async Task<ActionResult<ApiResponse>> SearchReactifs([FromQuery] string searchTerm = "")
        {
            var results = await _reactifService.SearchReactifsAsync(searchTerm);
            return Ok(new ApiResponse
            {
                Data = results,
                ViewBag = null,
                IsSuccess = true,
                Message = "Recherche effectuée avec succès.",
                StatusCode = 200
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> GetReactif(int id)
        {
            var reactif = await _reactifService.GetReactifByIdAsync(id);
            if (reactif == null)
            {
                return NotFound(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = "Reactif not found.",
                    StatusCode = 404
                });
            }

            return Ok(new ApiResponse
            {
                Data = reactif,
                ViewBag = null,
                IsSuccess = true,
                Message = "Reactif retrieved successfully.",
                StatusCode = 200
            });
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateReactif([FromBody] ReactifDto reactifDto)
        {
            var createdReactif = await _reactifService.CreateReactifAsync(reactifDto);
            return CreatedAtAction(nameof(GetReactif), new { id = createdReactif.IdReactif }, new ApiResponse
            {
                Data = createdReactif,
                ViewBag = null,
                IsSuccess = true,
                Message = "Reactif created successfully.",
                StatusCode = 201
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> UpdateReactif(int id, [FromBody] ReactifDto reactifDto)
        {
            if (id != reactifDto.IdReactif)
            {
                return BadRequest(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = "ID mismatch.",
                    StatusCode = 400
                });
            }

            var updatedReactif = await _reactifService.UpdateReactifAsync(id, reactifDto);
            return Ok(new ApiResponse
            {
                Data = updatedReactif,
                ViewBag = null,
                IsSuccess = true,
                Message = "Reactif updated successfully.",
                StatusCode = 200
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReactif(int id)
        {
            bool result = await _reactifService.DeleteReactifAsync(id);
            if (!result)
            {
                return NotFound(new ApiResponse
                {
                    Data = null,
                    ViewBag = null,
                    IsSuccess = false,
                    Message = "Reactif not found.",
                    StatusCode = 404
                });
            }

            return NoContent();
        }

        [HttpGet("{id}/stock-by-month/{year}")]
        public async Task<ActionResult<ApiResponse>> GetStockByMonth(int id, int year)
        {
            var stockByMonth = await _reactifService.GetStockByMonthAsync(id, year);
            return Ok(new ApiResponse
            {
                Data = stockByMonth,
                ViewBag = null,
                IsSuccess = true,
                Message = "Stock by month retrieved successfully.",
                StatusCode = 200
            });
        }
    }
}