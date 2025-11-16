using AutoMapper;
using Group4_Project.DTOs;
using Group4_Project.Models;
using Group4_Project.Repository.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group4_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CategoryReadDTO>>(items));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(_mapper.Map<CategoryReadDTO>(item));
        }



        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDTO dto)
        {
            var model = _mapper.Map<Category>(dto);
            await _repository.AddAsync(model);

            return CreatedAtAction(nameof(GetById), new { id = model.CategoryId },
                _mapper.Map<CategoryReadDTO>(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, CategoryUpdateDTO dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);

            await _repository.UpdateAsync(existing);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCategory(string id, [FromBody] JsonPatchDocument<Category> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

       
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
                return NotFound();

      
            patchDoc.ApplyTo(category, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           
            await _repository.UpdateAsync(category);

            return Ok(category);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _repository.DeleteAsync(id);

            return NoContent();
        }
    }
}
