using MongoDBApi.Models;
using MongoDBApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace MongoDBApi.Controllers;

[ApiController]
[Route("api/phoneInfos")]
public class PhoneInfosController : ControllerBase
{
        private readonly MongoDBService _mongoDbService;

        public PhoneInfosController(MongoDBService mongoDbService) =>
            _mongoDbService = mongoDbService;

        [HttpGet("getInfoList")]
        public async Task<List<PhoneInfo>> GetInfoList()
        {
            return await _mongoDbService.GetAsync();
        }

       
        // [HttpGet("{id:length(24)}")]
        [HttpGet("getItem/{id:length(24)}")]
        public async Task<ActionResult<PhoneInfo>> GetItem(string id)
        {
            var phoneInfo = await _mongoDbService.GetAsync(id);
        
            return phoneInfo;
        }

        [HttpPost("addItem")]
        public async Task<IActionResult> AddItem([FromBody] PhoneInfo phoneInfo)
        {
            await _mongoDbService.CreateAsync(phoneInfo);

            return CreatedAtAction(nameof(GetItem), new { id = phoneInfo.Id }, phoneInfo);
        }

        // [HttpPost("{id:length(24)}")]
        [HttpPost("updateItem/{id:length(24)}")]
        public async Task<IActionResult> UpdateItem(string id, [FromBody] PhoneInfo updatedPhoneInfo)
        // public async Task<IActionResult> UpdateItem([FromBody] PhoneInfo updatedPhoneInfo)
        {
            var account = await _mongoDbService.GetAsync(id);
        
            if (account is null)
            {
                return NotFound();
            }
        
            updatedPhoneInfo.Id = account.Id;
        
            await _mongoDbService.UpdateAsync(id, updatedPhoneInfo);
            // await _mongoDbService.UpdateAsync(updatedPhoneInfo);
            
            return NoContent();
        }

        // [HttpDelete("{id:length(24)}")]
        [HttpPost("deleteItem/{id:length(24)}")]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var phoneInfo = await _mongoDbService.GetAsync(id);

            if (phoneInfo is null)
            {
                return NotFound();
            }

            await _mongoDbService.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost("deleteAll")]
        public async Task<ActionResult> DeleteAll()
        {
            await _mongoDbService.DeleteAsync();
            
            return NoContent();
        }
}