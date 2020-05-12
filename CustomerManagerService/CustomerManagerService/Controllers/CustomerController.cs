using CustomerManagerService.Models;
using CustomerManagerService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.Generic;
using CustomerManagerService.DTOs;

namespace CustomerManagerService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> Get()
        {
            return await _customerService.Get();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(string id)
        {
            var result = await _customerService.Get(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /*[HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            var result = await _customerService.Create(customer);
            return Ok(result);
        }*/

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerDTO dto)
        {
            var result = await _customerService.Create(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Customer request)
        {
            var existing = await _customerService.Get(id);
            if (existing == null) return NotFound();
            request.Id = existing.Id;

            var result = await _customerService.Update(id, request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existing = await _customerService.Get(id);
            if (existing == null) return NotFound();
            await _customerService.Remove(id);
            return Ok();
        }
    }
}
