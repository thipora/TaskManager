
namespace BO;

internal class MilestoneInList
{
    public string? Nickname { get; set; }
    public string? Description { get; set; }
    public required DateTime Production { get; set; }
    public Status? Status { get; set; }
    public int? ComplationPercentage { get; set; }
}
