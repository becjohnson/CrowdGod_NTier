using CrowdGod.Model.ReplyModel;
using CrowdGod.Service;
using Microsoft.AspNetCore.Mvc;

namespace CrowdGod.WebMVC.Controllers
{
    public class ReplyController : Controller
    {
        private readonly IReplyService _svc;

        public ReplyController(IReplyService replyService)
        {
            _svc = replyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _svc.GetAllAsync());
        }

        [HttpGet]
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

            var reply = await _svc.Details(id);
            if (reply == null)
            {
                return NotFound();
            }
            return View(reply);
        }

        // GET: Replies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Replies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int answerId, [Bind("ReplyId,Content,CreatedUtc,AnswerId")] ReplyCreate reply)
        {
            if (ModelState.IsValid)
            {
                var result = await _svc.Create(answerId, reply);
                if (result == true)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(reply);
        }

        // GET: Replies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var reply = await _svc.Edit(id);
            if (reply == null)
            {
                return NotFound();
            }
            return View(reply);
        }

        // POST: Replies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReplyId,Content,ModifiedUtc,AnswerId")] ReplyEdit reply)
        {
            if (id != reply.ReplyId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (await _svc.Edit(id, reply))
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(reply);
        }

        // GET: Replies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var reply = await _svc.Delete(id);
            if (reply == null)
            {
                return NotFound();
            }
            return View(reply);
        }

        // POST: Replies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _svc.DeleteConfirmed(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
