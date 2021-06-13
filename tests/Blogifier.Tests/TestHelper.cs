using Almhd.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Almhd.Tests
{
	public class TestHelper
	{
		public string Slash { get { return Path.DirectorySeparatorChar.ToString(); } }
		public string ContextRoot
		{
			get
			{
				string path = Directory.GetCurrentDirectory();
				return path.Substring(0, path.IndexOf($"tests{Slash}Almhd.Tests"));
			}
		}

		public AppDbContext GetDbContext()
		{
			return new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
				 .UseSqlite(GetDataSource()).Options);
		}

		private string GetDataSource()
		{
			return $"DataSource={ContextRoot}src{Slash}Almhd{Slash}Blog.db";
		}
	}
}
