using Microsoft.AspNetCore.Mvc;
using PGSystem.ResponseType;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_Service.Admin;
using PGSystem_Service.Members;
using PGSystem_Service.Memberships;
using System.Runtime.InteropServices;
[Route("api/Members")]
[ApiController]
public class MembersController : Controller
{
    private readonly IMembershipService _membershipService;

    public MembersController(IMembershipService membershipService)
    {
        _membershipService = membershipService;
    }


    //[HttpGet]
    //public async Task<IActionResult> GetAllMembers()
    //{
    //    try
    //    {
    //        var members = await _membersService.GetAllMembersAsync();
    //        return Ok(members);
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(new { message = ex.Message });
    //    }
    //}

    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetMemberById(int id)
    //{
    //    try
    //    {
    //        var member = await _membersService.GetMemberByIdAsync(id);
    //        return Ok(member);
    //    }
    //    catch (KeyNotFoundException ex)
    //    {
    //        return NotFound(new { message = ex.Message });
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(new { message = ex.Message });
    //    }
    //}



    //[HttpPost("addMembers")]
    //public async Task<IActionResult> RegisterMember([FromBody] MemberRequest request)
    //{
    //    try
    //    {
    //        var result = await _membersService.RegisterMemberAsync(request);
    //        return CreatedAtAction(nameof(RegisterMember), new { id = result.MemberID }, result);
    //    }
    //    catch (KeyNotFoundException ex)
    //    {
    //        return NotFound(new { message = ex.Message });
    //    }
    //    catch (InvalidOperationException ex)
    //    {
    //        return BadRequest(new { message = ex.Message });
    //    }
    //} 
    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateMember(int id, [FromBody] MemberRequest request)
    //{
    //    try
    //    {
    //        var result = await _membersService.UpdateMemberAsync(id, request);
    //        if (!result)
    //        {
    //            return StatusCode(500, "Update failed.");
    //        }

    //        return Ok("Member updated successfully.");
    //    }
    //    catch (KeyNotFoundException ex)
    //    {
    //        return NotFound(new { message = ex.Message });
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(new { message = ex.Message });
    //    }
    //}



    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteMember(int id)
    //{
    //    try
    //    {
    //        var result = await _membersService.DeleteMemberAsync(id);
    //        if (!result)
    //        {
    //            return StatusCode(500, "Failed to delete member.");
    //        }

    //        return Ok("Member deleted successfully.");
    //    }
    //    catch (KeyNotFoundException ex)
    //    {
    //        return NotFound(new { message = ex.Message });
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(new { message = ex.Message });
    //    }
    //}

}









