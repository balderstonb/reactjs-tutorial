using System;

namespace tutorial_code_razor.Models
{
    [Serializable]
    public class CommentModel
	{
		public int Id { get; set; }
		public string Author { get; set; }
		public string Text { get; set; }
	}
}
