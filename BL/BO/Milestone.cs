
namespace BO;

public class Milestone
{
    public int ID { get; init; }
    public required string Nickname { get; set; }
    public required string Description { get; set; }
    public required DateTime EstimatedStartDate { get; set; }
    public DateTime? AcualStartNate { get; set; }
    public required DateTime EstimatedEndDate { get; set; }
    public required DateTime deadline { get; set; }
    public DateTime? AcualEndNate { get; set; }
    public required Status TaskStatus { get; set; }
    public int? ComplationPercentage { get; set; }
    public string? Remaeks { get; set; }
    public List<TaskInList>? taskInLists { get; set; }
}