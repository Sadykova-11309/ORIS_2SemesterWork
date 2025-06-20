using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvadminAPI.Contracts.Abstractions
{
	public interface ITemplateEngine
	{
		string Render(string template, object model);
		Task<string> RenderFromFileAsync(string templatePath, object model);
	}
}
