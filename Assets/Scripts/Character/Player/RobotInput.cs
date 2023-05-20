using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class RobotInput : MonoBehaviour
{
    //private PlayerMovement _playerMovement;

    //private void Awake()
    //{
    //    _playerMovement = GetComponent<PlayerMovement>();
    //}

    //private void OnEnable()
    //{
    //    _playerMovement.MovementEnabled += PlayerMovementOnPlayerMovementEnabled;
    //}

    //private void OnDisable()
    //{
    //    _playerMovement.MovementEnabled -= PlayerMovementOnPlayerMovementEnabled;
    //}

    //private async Task PassHouse1()
    //{
    //    _playerMovement.MoveRight();
    //    await Task.Delay(360);
    //    _playerMovement.MoveForward();
    //    await Task.Delay(340);
    //    _playerMovement.MoveLeft();
    //    await Task.Delay(365);
    //    _playerMovement.MoveForward();
    //    await Task.Delay(1700);
    //}

    //private async Task PassHouse2()
    //{
    //    _playerMovement.MoveLeft();
    //    await Task.Delay(485);
    //    _playerMovement.MoveForward();
    //    await Task.Delay(300);
    //    _playerMovement.MoveBack();
    //    await Task.Delay(980);
    //    _playerMovement.MoveRight();
    //    await Task.Delay(560);
    //    _playerMovement.MoveBack();
    //    await Task.Delay(390);
    //    _playerMovement.MoveLeft();
    //    await Task.Delay(420);
    //    _playerMovement.MoveForward();
    //    await Task.Delay(300);
    //    _playerMovement.MoveRight();
    //    await Task.Delay(380);
    //    _playerMovement.MoveForward();
    //    await Task.Delay(510);
    //    _playerMovement.MoveLeft();
    //    await Task.Delay(380);
    //    _playerMovement.MoveForward();
    //    await Task.Delay(2450);
    //}

    //private async Task PassHouse3()
    //{
    //    _playerMovement.MoveLeft();
    //    await Task.Delay(300);
    //    _playerMovement.MoveRight();
    //    await Task.Delay(580);
    //    _playerMovement.MoveForward();
    //    await Task.Delay(1500);
    //    _playerMovement.MoveLeft();
    //    await Task.Delay(420);
    //    _playerMovement.MoveBack();
    //    await Task.Delay(420);
    //    _playerMovement.MoveRight();
    //    await Task.Delay(670);
    //    _playerMovement.MoveBack();
    //    await Task.Delay(500);
    //    _playerMovement.MoveLeft();
    //    await Task.Delay(450);
    //    _playerMovement.MoveForward();
    //    await Task.Delay(360);
    //    _playerMovement.MoveRight();
    //    await Task.Delay(860);
    //    _playerMovement.MoveLeft();
    //    await Task.Delay(590);
    //    _playerMovement.MoveForward();
    //    await Task.Delay(520);
    //    _playerMovement.MoveRight();
    //    await Task.Delay(550);
    //    _playerMovement.MoveBack();
    //    await Task.Delay(550);
    //    _playerMovement.MoveForward();
    //    await Task.Delay(350);
    //    _playerMovement.MoveLeft();
    //    await Task.Delay(590);
    //    _playerMovement.MoveBack();
    //    await Task.Delay(510);
    //    _playerMovement.MoveRight();
    //    await Task.Delay(420);
    //    _playerMovement.MoveForward();
    //}

    //private async void PlayerMovementOnPlayerMovementEnabled()
    //{
    //    await PassHouse1();
    //    await PassHouse2();
    //    await PassHouse3();
    //}
}