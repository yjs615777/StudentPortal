using Microsoft.AspNetCore.Mvc;
using StudentPortal.Contracts.Responses;
using StudentPortal.Data;
using StudentPortal.Services;

namespace StudentPortal.Controllers.Mvc
{
    public class StudentController(IStudentService service) : Controller
    {
        private readonly IStudentService _service = service;
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var result = await _service.GetAllAsync(ct);
            var list = result.Data ?? [];
            return View(list);
        }
    }
}