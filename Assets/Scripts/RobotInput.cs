using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class RobotInput : MonoBehaviour
{
    private Movement _movement;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    private void OnEnable()
    {
        _movement.MovementEnabled += Movement_OnMovementEnabled;
    }

    private void OnDisable()
    {
        _movement.MovementEnabled -= Movement_OnMovementEnabled;
    }

    private async void Movement_OnMovementEnabled()
    {
        await PassHouse1();
        await PassHouse2();
    }

    private async Task PassHouse1()
    {
        _movement.MoveRight();
        await Task.Delay(360);
        _movement.MoveForward();
        await Task.Delay(340);
        _movement.MoveLeft();
        await Task.Delay(365);
        _movement.MoveForward();
        await Task.Delay(1700);
    }

    private async Task PassHouse2()
    {
        _movement.MoveLeft();
        await Task.Delay(485);
        _movement.MoveForward();
        await Task.Delay(300);
        _movement.MoveBack();
        await Task.Delay(980);
        _movement.MoveRight();
    }
}