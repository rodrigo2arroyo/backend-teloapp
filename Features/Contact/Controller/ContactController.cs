using Microsoft.AspNetCore.Mvc;
using TeloApi.Features.Contact.DTOs;
using TeloApi.Features.Contact.Services;

namespace TeloApi.Features.Contact.Controller;

[ApiController]
[Route("api/[controller]")]
public class ContactController(IContactService contactService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateContact([FromBody] CreateContact request)
    {
        var result = await contactService.CreateContact(request);
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateContact([FromBody] UpdateContact request)
    {
        var result = await contactService.UpdateContact(request);
        return Ok(result);
    }
    
    [HttpDelete("{contactId}")]
    public async Task<IActionResult> DeleteReview(int contactId)
    {
        var result = await contactService.DeleteContact(contactId);
        return Ok(result);
    }
}