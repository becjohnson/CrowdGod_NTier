using CrowdGod.Model.QuestionModel;
using CrowdGod.Service;
using Microsoft.AspNetCore.Mvc;

namespace CrowdGod.WebMVC.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService _svc;

        public QuestionController(IQuestionService questionService)
        {
            _svc = questionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _svc.GetAllAsync());
        }

        [HttpGet, ActionName("SearchIndex")]
        public async Task<IActionResult> SearchIndex(string searchString)
        {
            return View(await _svc.GetAllAsync(searchString));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var question = await _svc.Details(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuestionId,Title,Content")] QuestionCreate question)
        {
            if (ModelState.IsValid)
            {
                var result = await _svc.Create(question);
                if (result == true)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(question);
        }

        public PartialViewResult _QuestionCreatePartial()
        {
            return PartialView(Create());
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _svc.Edit(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuestionId,Title,Content,ModifiedUtc")] QuestionEdit question)
        {
            if (id != question.QuestionId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (await _svc.Edit(id, question))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var question = await _svc.Delete(id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _svc.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
