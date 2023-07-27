﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Patrick_WebAPI.Models.Domain;
using Patrick_WebAPI.Models.DTO;
using Patrick_WebAPI.Repositories;
using System.Data.Entity;

namespace Patrick_WebAPI.Controllers
{

	// api/walks
	[Route("api/[controller]")]
	[ApiController]
	public class WalksController : ControllerBase
	{
		private readonly IMapper mapper;
		private readonly IWalkRepository walkRepository;

		public WalksController(IMapper mapper, IWalkRepository walkRepository)
		{
			this.mapper = mapper;
			this.walkRepository = walkRepository;
		}
		[HttpPost]
		public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
		{
			// Map DTO to Domain Model

			var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
			await walkRepository.CreateAsync(walkDomainModel);


			// Mapping Domain Model to DTO
			var walkDto = mapper.Map<WalkDto>(walkDomainModel);
			return Ok(walkDto);
		}


		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var walksDomainModel = await walkRepository.GetAllAsync();
			//Map Domain Model to Dto
			//var walkDto = new List<WalkDto>();

			//foreach (var walk in walksDomainModel)
			// {
			//	var walkDtoTemp = new WalkDto()
			//	{
			//		Name = walk.Name,
			//		Description = walk.Description,
			//		LengthInKm = walk.LengthInKm,
			//		WalkImageUrl = walk.WalkImageUrl,
			//		DifficultyId = walk.DifficultyId,
			//		RegionId = walk.RegionId
			//	};
			//	 walkDto.Add(walkDtoTemp);
			//}	
			return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
		}

		[HttpGet]
		[Route("{id:Guid}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			var walkDomainModel = await walkRepository.GetByIdAsync(id);
			if(walkDomainModel == null)
				return NotFound();
			var walkDto = mapper.Map<WalkDto>(walkDomainModel);
			return Ok(walkDto);
		}
	}
}
