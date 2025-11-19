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
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierRepository _repository;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<SupplierReadDTO>>(items));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(_mapper.Map<SupplierReadDTO>(item));
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierCreateDTO dto)
        {
            var model = _mapper.Map<Supplier>(dto);
            await _repository.AddAsync(model);

            return CreatedAtAction(nameof(GetById), new { id = model.Id },
                _mapper.Map<SupplierReadDTO>(model));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, SupplierUpdateDTO dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing);

            return NoContent();
        }


        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchSupplier(int id, [FromBody] JsonPatchDocument<Supplier> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var supplier = await _repository.GetByIdAsync(id);
            if (supplier == null)
                return NotFound();

            patchDoc.ApplyTo(supplier, ModelState);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repository.UpdateAsync(supplier);

            return Ok(_mapper.Map<SupplierReadDTO>(supplier));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}
