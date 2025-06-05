namespace Lefish.Application.Commands.AttackEvents.RemoveAttackEvents;

public class RemoveAttackEventsCommandModel
{
    public int? EmailTargetId { get; set; }

    public bool Confirmed { get; set; }
}
