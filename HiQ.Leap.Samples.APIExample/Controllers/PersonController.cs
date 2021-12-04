﻿using HiQ.Leap.Samples.Common.Models;
using HiQ.Leap.Samples.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HiQ.Leap.APIExample.Controllers;

[ApiController]
[Route("controller]")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly IStorageService _storageService;

    public PersonController(ILogger<PersonController> logger, IStorageService storageService)
    {
        _logger = logger;
        _storageService = storageService;
    }

    [HttpPost(Name = "PostPerson")]
    public ActionResult<Person> Post([FromBody] Person person)
    {
        var result = _storageService.SavePerson(person);
        return Created(nameof(person), result);
    }

    [HttpGet("{name}", Name = "GetPerson")]
    public ActionResult<Person> GetPerson([FromRoute] string name)
    {
        var result = _storageService.GetPerson(name);
        return Ok(result);
    }

    [HttpGet("", Name = "GetPersons")]
    public ActionResult<List<Person>> GetPersons()
    {
        var result = _storageService.GetPersons();
        return Ok(result);
    }
}