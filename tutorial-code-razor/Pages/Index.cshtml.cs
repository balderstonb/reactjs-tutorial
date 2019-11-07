using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using tutorial_code_razor.Models;

namespace tutorial_code_razor.Pages
{
    public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;

		public List<CommentModel> Comments { get; set; }

		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;

            Comments = new List<CommentModel>();
        }

		public void OnGet()
		{

		}

		public JsonResult OnGetComments()
		{
            var comments = HttpContext.Session.GetComments("comments");

			return new JsonResult(comments);
		}

		public IActionResult OnPostAddComment(CommentModel comment)
		{
            var comments = HttpContext.Session.GetComments("comments") ?? new List<CommentModel>();

            comment.Id = comments.Count + 1;
            comments.Add(comment);

            HttpContext.Session.SetComments("comments", comments);

			return Content("Success");
		}
	}
}
