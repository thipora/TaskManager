
namespace BO;

public class Engineer
{
    public required int ID { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public EngineerLevelEnum EngineerLevel { get; set; }
    public float PriceOfHour { get; set; }
    public TaskIdNickname? Task { get; set; }
}
