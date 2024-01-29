namespace BlImplementation;
using BlApi;
using BO;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public Milestone ReadAll(int id)
    {
        throw new NotImplementedException();
    }

    public Milestone Update(int id)
    {
        throw new NotImplementedException();
    }
}
