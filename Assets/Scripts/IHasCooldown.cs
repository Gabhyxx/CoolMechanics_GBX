using UnityEngine;

public interface IHasCooldown
{
    int Id { get; }
    float CooldownDuration { get; }
}
