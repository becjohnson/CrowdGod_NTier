using CrowdGod.Model.AnswerModel;
using CrowdGod.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrowdGod.WebMVC.Controllers
{
    public class AnswerController : Controller
    {
        private readonly IAnswerService _svc;

        public AnswerController(IAnswerService answerService)
        {
            _svc = answerService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            return View(await _svc.GetAllAsync(searchString));
        }
        
        [HttpGet]
        public async Task<IActionResult> _AnswerIndexPartial()
        {
            return View(await _svc.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var answer = await _svc.Details(id);
            if (answer == null)
            {
                return NotFound();
            }
            return View(answer);
        }

        // GET: Answers/Create
        [NonAction]
        public IActionResult Create()
        {
            return Ok("Thanks for creating.");
        }

        // POST: Answers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> _AnswerCreatePartial(int questionId, [Bind("AnswerId,Content")] AnswerCreate answer)
        {
            if (ModelState.IsValid)
            {
                var result = await _svc.Create(questionId, answer);
                if (result == true)
                {
                    return Ok("Answer successfully created.");
                }
            }
            return BadRequest("Please make sure to fill in all required fields.");
        }

        // GET: Answers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var answer = await _svc.Edit(id);
            if (answer == null)
            {
                return NotFound();
            }
            return View(answer);
        }

        // POST: Answers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnswerId,Content,ModifiedUtc,QuestionId")] AnswerEdit answer)
        {
            if (id != answer.AnswerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await _svc.Edit(id, answer))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(answer);
        }

        // GET: Answers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var answer = await _svc.Delete(id);
            if (answer == null)
            {
                return NotFound();
            }
            return View(answer);
        }

        // POST: Answers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _svc.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
