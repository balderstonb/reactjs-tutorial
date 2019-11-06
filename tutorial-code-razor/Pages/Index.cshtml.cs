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
		private readonly IList<CommentModel> _comments;

		public IEnumerable<CommentModel> Comments
		{
			get
			{
				return _comments;
			}
		}


		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;

			_comments = new List<CommentModel>
			{
				new CommentModel
				{
					Id = 1,
					Author = "Daniel Lo Nigro",
					Text = "Hello ReactJS.NET World!"
				},
				new CommentModel
				{
					Id = 2,
					Author = "Pete Hunt",
					Text = "This is one comment"
				},
				new CommentModel
				{
					Id = 3,
					Author = "Jordan Walke",
					Text = "This is *another* comment"
				},
			};
		}

		public void OnGet()
		{

		}

		public JsonResult OnGetComments()
		{
			return new JsonResult(_comments);
		}

		public IActionResult OnPostAddComment(CommentModel comment)
		{
			comment.Id = _comments.Count + 1;
			_comments.Add(comment);
			return Content("Success :)");
		}
	}
}
