using Application.SearchEngine.CreateSearch;
using Application.SearchEngine.GetSearch;
using Application.SearchEngine.GetSearchSuggestions;
using Application.SearchEngine.UpdateSearch;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models.SearchEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/SearchEngine")]
    public class SearchEngineController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SearchEngineController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet("Get")]
        public async Task<IActionResult> GetAsync(string searchId, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = new GetSearchEngineCommand{ SearchId = searchId };
                var result = await _mediator.Send(query, cancellationToken);

                if (result.SearchEngine != null)
                {
                    return Ok(result.SearchEngine);
                }
                else
                {
                    return NotFound($"Search engine with ID {searchId} not found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Suggestions")]
        //[Authorize]
        public async Task<IActionResult> GetSearchEngineSuggestions(CancellationToken cancellationToken = default)
        {
            try
            {
             
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return BadRequest("User ID is not available.");
                }

             
                var command = new GetSearchEngineSuggestionsCommand { UserId = userId };
                var result = await _mediator.Send(command, cancellationToken);

                if (result.SearchQueries != null)
                {
                    return Ok(result.SearchQueries);
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync(CreateSearchEngineDTO model, CancellationToken cancellationToken = default)
        {
            try
            {
                var searchEngine = _mapper.Map<SearchEngine>(model);
                var command = new CreateSearchEngineCommand() {SearchEngine=searchEngine};
                searchEngine.UserId= HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                searchEngine.SearchDate = DateTime.Now;

                var result = await _mediator.Send(command, cancellationToken);


                if (result.SearchEngine != null)
                {
                    return Ok(result.SearchEngine);
                }
                else
                    return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateSearchEngineDTO model, CancellationToken cancellationToken = default)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var searchEngine = _mapper.Map<SearchEngine>(model);
                var command = new UpdateSearchEngineCommand { SearchEngine =searchEngine };
                var result = await _mediator.Send(command, cancellationToken);

                if (result.SearchEngine != null)
                {
                    return Ok(result.SearchEngine);
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
