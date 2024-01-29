
namespace BO;

public class TaskInList
{
    public required int ID {  get; init; }
    public required string Nickname { get; set; }
    public required string Description { get; set; }
    public Status? Status { get; set; }
}