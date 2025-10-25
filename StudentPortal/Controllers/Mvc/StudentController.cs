using Microsoft.AspNetCore.Mvc;
using StudentPortal.Contracts.Requests;
using StudentPortal.Contracts.Responses;
using StudentPortal.Data;
using StudentPortal.Services;

namespace StudentPortal.Controllers.Mvc
{
    [Route("Student")]
    public class StudentController(IStudentService service) : Controller
    {
        private readonly IStudentService _service = service;

        // 목록
        [HttpGet("")]
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var result = await _service.GetAllAsync(ct);
            var list = result.Data ?? Array.Empty<StudentResponse>();
            return View(list);
        }

        // 생성 폼 
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View(new StudentCreateRequest());
        }

        // 생성 처리
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentCreateRequest req, CancellationToken ct)
        {
            if (!ModelState.IsValid) return View(req);

            var create = await _service.CreateAsync(req, ct);
            if (!create.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, create.Error ?? "생성 실패");
                return View(req);
            }

            // create.Data 에 새로 생성된 Id(int)가 들어있음
            return RedirectToAction(nameof(Index));
        }

        // 수정 폼
        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id, CancellationToken ct)
        {
            var found = await _service.GetByIdAsync(id, ct);
            if (!found.IsSuccess || found.Data is null) return NotFound();

            var s = found.Data;
            var vm = new StudentUpdateRequest(Id: s.Id, Name: s.Name, Age: s.Age);
            return View(vm);
        }

        // 수정 처리
        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentUpdateRequest req, CancellationToken ct)
        {
            if (id != req.Id) return BadRequest();
            if (!ModelState.IsValid) return View(req);

            var updated = await _service.UpdateAsync(req, id, ct);
            if (!updated.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, updated.Error ?? "수정 실패");
                return View(req);
            }

            return RedirectToAction(nameof(Index));
        }

        // 삭제
        [HttpPost("Delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var deleted = await _service.DeleteAsync(id, ct);
            if (!deleted.IsSuccess)
                TempData["Error"] = deleted.Error ?? "삭제 실패";

            return RedirectToAction(nameof(Index));
        }
    }
}