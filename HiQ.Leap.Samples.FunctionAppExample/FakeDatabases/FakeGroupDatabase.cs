using System.Collections.Generic;
using System.Threading.Tasks;
using HiQ.Leap.Samples.FunctionAppExample.Models;

namespace HiQ.Leap.Samples.FunctionAppExample.FakeDatabases;

public class FakeGroupDatabase
{
    private readonly Dictionary<int, Group> _groups;
    private int CurrentKey;

    public FakeGroupDatabase()
    {
        _groups = new Dictionary<int, Group>();
    }

    public async Task<Group> CreateGroupAsync(CreateGroupRequest request)
    {
        var id = ++CurrentKey;
        var group = new Group
        {
            Id = id,
            Name = request.Name
        };
        _groups.Add(id, group);
        return await Task.FromResult(group);
    }

    public async Task AddMemberToGroupAsync(int groupId, Person person)
    {
        var group = _groups[groupId];
        group.Members.Add(person);
        await Task.CompletedTask;
    }

    public async Task<Group> GetGroupAsync(int groupId)
    {
        if (!_groups.TryGetValue(groupId, out var group))
        {
            return null;
        }

        return await Task.FromResult(group);
    }
}