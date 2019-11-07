using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
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
			return new JsonResult(Comments);
		}

		public IActionResult OnPostAddComment(CommentModel comment)
		{
			comment.Id = Comments.Count + 1;
            Comments.Add(comment);

			return Content("Success :)");
		}
	}
}
