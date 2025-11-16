using AutoMapper;
using Group4_Project.DTOs;
using Group4_Project.Models;
using Group4_Project.Repository.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Group4_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductReadDTO>>(items));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(_mapper.Map<ProductReadDTO>(item));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDTO dto)
        {
            var model = _mapper.Map<Product>(dto);
            await _repository.AddAsync(model);

            return CreatedAtAction(nameof(GetById), new { id = model.ProductId },
                _mapper.Map<ProductReadDTO>(model));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, ProductUpdateDTO dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);

            await _repository.UpdateAsync(existing);
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProduct(string id, [FromBody] JsonPatchDocument<Product> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            patchDoc.ApplyTo(product, ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.UpdateAsync(product);

            return Ok(product);
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
