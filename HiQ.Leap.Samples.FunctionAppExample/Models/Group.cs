using System.Collections.Generic;

namespace HiQ.Leap.Samples.FunctionAppExample.Models;

public class Group
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Person> Members { get; set; } = new List<Person>();
}