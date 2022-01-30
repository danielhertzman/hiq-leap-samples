using HiQ.Leap.Samples.Domain.Models;
using HiQ.Leap.Samples.Domain.RequestModels;
using HiQ.Leap.Samples.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HiQ.Leap.Samples.APIExample.Controllers;

[ApiController]
[Route("/[controller]")]
public class PersonsController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonsController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpPost(Name = "PostPerson")]
    public ActionResult<Person> Post([FromBody] PersonCreateRequest request)
    {
        var result = _personService.SavePerson(request);
        return Created(nameof(Person), result);
    }

    [HttpPut("{id:int}", Name = "EditPerson")]
    public ActionResult<Person> EditPerson([FromRoute] int id, [FromBody] PersonEditRequest request)
    {
        _personService.EditPerson(id, request);
        return Ok();
    }

    [HttpGet("{id:int}", Name = "GetPersonById")]
    public ActionResult<Person> GetPersonById([FromRoute] int id)
    {
        var result = _personService.GetPerson(id);
        return Ok(result);
    }

    [HttpGet(Name = "GetPersons")]
    public ActionResult<List<Person>> GetPersons()
    {
        var result = _personService.GetPersons();
        return Ok(result);
    }

    [HttpDelete("{id:int}", Name = "DeletePerson")]
    public ActionResult DeletePerson([FromRoute] int id)
    {
        _personService.DeletePerson(id);
        return Ok();
    }
}