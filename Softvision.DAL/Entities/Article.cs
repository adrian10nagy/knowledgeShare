using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
	public class Article
	{
		public int Id;
		public string  Title { get; set; }
		public DateTime CreatedDate;
		public int Vote;
		public ArticleFlags Flags;
		public string AnonymousName;
		public string AnonymousEmail;
		public string InternalRep;
		public string HTMLRep;
        public Language Language;
		
		public int IdUser;
		public int IdSubCategory;
		public string SubCategory;

		public int IdCategory { get; set; }

		public List<Comment> Comments;
		
		public User User;
	}

	[Flags]
	public enum ArticleFlags
	{
		TopArticle = 1,
		Irreleavnt = 2,
		Blocked = 4,
		NotConfirmed = 8
	}
}