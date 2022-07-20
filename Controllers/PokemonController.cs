using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class PokemonController : ControllerBase {

    private readonly demoRestContext _context;

    public PokemonController(demoRestContext context){
        _context = context;
    }

    [HttpGet("all")]
    public IEnumerable<Pokemon> getAllPokemons(){
        var pokemons = _context.Pokemon.ToList();
        return pokemons;
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pokemon))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult getPokemon(long id){
        var pokemon = _context.Pokemon.Find(id);
        if(pokemon == null){
            return NotFound();
        }
        return Ok(pokemon);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pokemon))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult editPokemon(long id, Pokemon pokemon){
        if(pokemon == null){
            return NotFound();
        }
        _context.Entry(pokemon).State = EntityState.Modified;

        try {
            _context.SaveChanges();
        } catch (DbUpdateException){
            if(_context.Pokemon.Find(id) == null){
                return NotFound();
            }
            throw;
        }
        return Ok(pokemon);
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Pokemon))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult createPokemon(Pokemon pokemon){
        _context.Pokemon.Add(pokemon);
        _context.SaveChanges();
        return CreatedAtAction("getPokemon", new { id = pokemon.Id }, pokemon );
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult deletePokemon(long id){
        var pokemon = _context.Pokemon.Find(id);
        if (pokemon == null){
            return NotFound();
        }
        _context.Pokemon.Remove(pokemon);
        _context.SaveChanges();
        return NoContent();
    }

}