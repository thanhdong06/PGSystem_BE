using Microsoft.AspNetCore.Mvc;
using PGSystem.ResponseType;
using PGSystem_DataAccessLayer.DTO.RequestModel;
using PGSystem_DataAccessLayer.DTO.ResponseModel;
using PGSystem_Service.Admin;
using PGSystem_Service.Members;
using System.Runtime.InteropServices;
[Route("api/Members")]
[ApiController]
public class MembersController : Controller
{
    private readonly IMembersService _membersService;

    public MembersController(IMembersService membersService)
    {
        _membersService = membersService;
    }
  

   

    //    [HttpGet("{id}")]
    //    public async Task<IActionResult> GetMemberById(int id)
    //    {
    //        try
    //        {
    //            var member = await _membersService.GetMemberByIdAsync(id);
    //            return Ok(member);
    //        }
    //        catch (KeyNotFoundException ex)
    //        {
    //            return NotFound(new { message = ex.Message });
    //        }
    //    }
    //[HttpPost("Register Member")]
    //public async Task<IActionResult> RegisterMember([FromBody] MemberRequest request)
    //{
    //    try
    //    {
    //        var newMember = await _membersService.RegisterMemberAsync(request);
    //        return CreatedAtAction(nameof(GetMemberById), new { id = newMember.MemberID }, newMember);
    //    }
    //    catch (KeyNotFoundException ex)
    //    {
    //        return NotFound(new { message = ex.Message });
    //    }
    //}

    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateMember(int id, [FromBody] MemberRequest request)
    //{
    //    try
    //    {
    //        var updatedMember = await _membersService.UpdateMemberAsync(id, request);
    //        return Ok(updatedMember);
    //    }
    //    catch (KeyNotFoundException ex)
    //    {
    //        return NotFound(new { message = ex.Message });
    //    }
    //}

    [HttpDelete("SortDelete")]
    public async Task<IActionResult> DeleteMembers(int id)
    {
        try
        {
            await _membersService.SoftDeleteMemberAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }





}





