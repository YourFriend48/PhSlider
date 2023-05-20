using System;

public interface IFloatParametr
{
    public float Value { get; }

    public event Action Upgraded;
    public event Action Setted;
}
