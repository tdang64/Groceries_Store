using AutoMapper;
using Group4_Project.DTOs;
using Group4_Project.Models;
using Group4_Project.Repository.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Group4_Project.Data;

namespace Group4_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly GroceryDbContext _context;

        // ⭐ UPDATED CONSTRUCTOR — now includes DbContext
        public ProductsController(
            IProductRepository repository,
            IMapper mapper,
            GroceryDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductReadDTO>>(items));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
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

            return CreatedAtAction(nameof(GetById), new { id = model.Id },
                _mapper.Map<ProductReadDTO>(model));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ProductUpdateDTO dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchProduct(int id, [FromBody] JsonPatchDocument<Product> patchDoc)
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        // ⭐ NEW REQUIRED ENDPOINT — FIXES FRONTEND
        // GET api/Products/byCategoryId/13
        [HttpGet("byCategoryId/{categoryId:int}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var products = await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();

            return Ok(products);
        }
        [HttpGet("search/{query}")]
        public async Task<IActionResult> Search(string query)
        {
            var results = await _context.Products
                .Where(p => p.Name.Contains(query) || p.Description.Contains(query))
                .ToListAsync();

            return Ok(results);
        }

    }
}
