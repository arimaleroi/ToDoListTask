using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using ToDoListTestTask.Data;
using ToDoListTestTask.Models;

namespace ToDoListTestTask.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tasks = _context.Tasks.ToList();
            return View(tasks);
        }

        public IActionResult FilterByStatus(bool? isCompleted)
        {
            var tasks = _context.Tasks.AsQueryable();

            if (isCompleted.HasValue)
            {
                tasks = tasks.Where(t => t.IsCompleted == isCompleted.Value);
            }

            return View("Index", tasks.ToList());
        }

        [HttpPost]
        public IActionResult Create(TaskModel task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return PartialView("_TaskItem", task);
            }

            return BadRequest(ModelState);
        }

        public IActionResult ToggleComplete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                task.IsCompleted = !task.IsCompleted;
                _context.SaveChanges();
            }

            return Json(new { taskId = task.Id, isCompleted = task.IsCompleted });
        }

        [HttpPost]
        public IActionResult UpdateTitle(int id, string newTitle)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                task.Title = newTitle;
                _context.SaveChanges();
                return Ok();
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
                return Ok();
            }

            return NotFound();
        }
    }
}
