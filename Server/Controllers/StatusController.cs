using System.Collections.Immutable;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class StatusController : Controller
{
	readonly IWebHostEnvironment environment;
	readonly IConfiguration config;

	public StatusController(IWebHostEnvironment environment, IConfiguration config)
	{
		this.environment = environment;
		this.config = config;
	}

	[HttpGet("status")]
	public IActionResult GetStatus()
	{
		var rootFiles = environment
			.WebRootFileProvider.GetDirectoryContents("")
			.Select(f => f.Name)
			.ToImmutableArray();

		return Ok(
			new
			{
				environment.EnvironmentName,
				environment.WebRootPath,
				rootFiles,
				WwwRootStaticFiles = config.GetValue<string>("WwwRootStaticFiles")
			}
		);
	}
}
