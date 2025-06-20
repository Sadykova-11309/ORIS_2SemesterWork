using EvadminAPI.Contracts.Abstractions;

namespace EvadminAPI.Infrastucture.Template
{
	public class ScribanTemplateEngine : ITemplateEngine
	{
		public string Render(string template, object model)
		{
			var scribanTemplate = Scriban.Template.Parse(template);
			return scribanTemplate.Render(model);
		}

		public async Task<string> RenderFromFileAsync(string templatePath, object model)
		{
			var templateContent = await File.ReadAllTextAsync(templatePath);
			return Render(templateContent, model);
		}
	}
}
