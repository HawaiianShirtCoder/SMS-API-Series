using Microsoft.AspNetCore.Mvc;
using SMS.Shared.BLL;
using SMS.Shared.DTOs.BarStaff;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarStaffController : ControllerBase
    {
        private readonly ISMSLogic _businessLogic;

        public BarStaffController(ISMSLogic businessLogic)
        {
            _businessLogic = businessLogic;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllBarStaffAssignments()
        {
            try
            {
                var assignments = await _businessLogic.GetAllBarStaffAssignments();
                return Ok(assignments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        [Route("fixture/{fixtureId}")]
        [HttpGet]
        public async Task<ActionResult> GetBarStaffAssignmentsByFixture(int fixtureId)
        {
            try
            {
                var assignments = await _businessLogic.GetBarStaffAssignmentsByFixture(fixtureId);
                if (!assignments.Any())
                {
                    return NotFound("No bar staff assignments found for this fixture");
                }
                return Ok(assignments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

    
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult> GetBarStaffAssignment(int id)
        {
            try
            {
                var assignment = await _businessLogic.GetBarStaffAssignment(id);
                if (assignment is null)
                {
                    return NotFound("Bar staff assignment not found");
                }
                return Ok(assignment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateBarStaffAssignment(AddBarStaffAssignmentDto assignment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _businessLogic.SaveBarStaffAssignment(assignment);
                if (response.ExecutionStatus != Shared.Enums.ExecuteCommandEnum.Ok)
                {
                    return StatusCode((int)response.ExecutionStatus, response.ErrorMessage);
                }
                return CreatedAtAction(nameof(GetAllBarStaffAssignments), new { }, "Bar staff assignment created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult> UpdateBarStaffAssignment(int id, BarStaffAssignmentDto assignment)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existing = await _businessLogic.GetBarStaffAssignment(id);
                if (existing is null)
                {
                    return NotFound("Bar staff assignment not found");
                }

                var response = await _businessLogic.UpdateBarStaffAssignment(id, assignment);
                if (response.ExecutionStatus != Shared.Enums.ExecuteCommandEnum.Ok)
                {
                    return StatusCode((int)response.ExecutionStatus, response.ErrorMessage);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteBarStaffAssignment(int id)
        {
            try
            {
                //var existing = await _businessLogic.GetBarStaffAssignment(id);
                //if (existing is null)
                //{
                //    return NotFound("Bar staff assignment not found");
                //}

                var response = await _businessLogic.DeleteBarStaffAssignment(id);
                if (response.ExecutionStatus != Shared.Enums.ExecuteCommandEnum.Ok)
                {
                    return StatusCode((int)response.ExecutionStatus, response.ErrorMessage);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }

        [Route("{id}/confirm")]
        [HttpPatch]
        public async Task<ActionResult> ConfirmBarStaffAssignment(int id)
        {
            try
            {
                var existing = await _businessLogic.GetBarStaffAssignment(id);
                if (existing is null)
                {
                    return NotFound("Bar staff assignment not found");
                }

                var response = await _businessLogic.ConfirmBarStaffAssignment(id);
                if (response.ExecutionStatus != Shared.Enums.ExecuteCommandEnum.Ok)
                {
                    return StatusCode((int)response.ExecutionStatus, response.ErrorMessage);
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred", details = ex.Message });
            }
        }
    }
}
