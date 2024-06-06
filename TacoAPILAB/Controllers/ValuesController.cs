using Microsoft.AspNetCore.Mvc;
using System.Data;
using TacoAPILAB.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TacoAPILAB.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();
		

		public IActionResult GetAll(string? category = null)
		{
			List<Taco> result = dbContext.Tacos.ToList();
			if (category != null)
			{
				result = result.Where(s => s.Name == name).ToList();
			}

			return Ok(result); //Return status code and respons obj
		}

		//Post is for adding
		[HttpPost()]
		//from body takes the text from the body of the request
		public IActionResult Addtaco([FromBody] Taco newTaco)
		{
			dbContext.Tacos.Add(newTaco);
			dbContext.SaveChanges();
			//The string is where they can find the object they just created
			//we link them back to the get by id method
			return Created($"/api/staff/{newTaco.Id}", newTaco);
		}
		//Put is for updating
		[HttpPut("{id}")]
		public IActionResult UpdateMenu([FromBody] Taco targetStaff, int id)
		{
			if (id != targetStaff.Id) { return BadRequest(); }
			if (!dbContext.Tacos.Any(b => b.Id == id)) { return NotFound(); }

			dbContext.Tacos.Update(targetStaff);
			dbContext.SaveChanges();
			return NoContent();
		}
		[HttpDelete("{id}")]
		public IActionResult DeleteTaco(int id)
		{
			Taco s = dbContext.Tacos.Find(id);
			if (s == null)
			{
				return NotFound();
			}
			dbContext.Tacos.Remove(s);
			dbContext.SaveChanges();
			//Common to return nothing when deleting
			return NoContent();
		}
	}
}
