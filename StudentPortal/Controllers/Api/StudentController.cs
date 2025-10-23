using Microsoft.AspNetCore.Mvc;
using StudentPortal.Contracts.Requests;
using StudentPortal.Contracts.Responses;
using StudentPortal.Services;

namespace StudentPortal.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController(IStudentService service) : ControllerBase
    {
        private readonly IStudentService _service = service;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentResponse>>> GetAll(CancellationToken ct)
        {
            var r = await _service.GetAllAsync(ct);
            return Ok(r.Data); // 실패할 일이 사실상 없지만, 패턴 맞춤
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StudentResponse>> GetOne(int id, CancellationToken ct)
        {
            var r = await _service.GetByIdAsync(id, ct);
            if (!r.IsSuccess && r.Error == "NotFound") return NotFound();
            return Ok(r.Data);
        }   

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] StudentCreateRequest req, CancellationToken ct)
        {
            // FluentValidation 실패 시 자동으로 400 반환됨 (Program.cs 설정 기준)
            var r = await _service.CreateAsync(req, ct);
            return CreatedAtAction(nameof(GetOne), new { id = r.Data }, new { id = r.Data });
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<StudentResponse>> Update(int id, [FromBody] StudentUpdateRequest req, CancellationToken ct)
        {
            var r = await _service.UpdateAsync(req,id,ct);
            if (!r.IsSuccess && r.Error == "NotFound") return NotFound();
            return Ok(r.Data); // NoContent()도 가능하지만, 최신 상태를 리턴하는 편을 데모에선 선호
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id, CancellationToken ct)
        {
            var r = await _service.DeleteAsync(id, ct);
            if (!r.IsSuccess && r.Error == "NotFound") return NotFound();
            return NoContent();
        }
    }
}
