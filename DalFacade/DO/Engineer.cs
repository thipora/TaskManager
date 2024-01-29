
namespace DO;
/// <summary>
/// Details for each engineer
/// </summary>
/// <param name="ID">id of the engineer</param>
/// <param name="Name">Last name of the engineer</param>
/// <param name="Email">email of the engineer</param>
/// <param name="EngineerLevel">level of the engineer</param>
/// <param name="PriceOfHour">price of hour that pay for this engineer</param>
public record Engineer
(
    int ID,
    string Name,
    string Email,
    EngineerLevelEnum EngineerLevel,
    float PriceOfHour
)
{
    public Engineer() : this(0, "", "", 0, 0) { }
}
