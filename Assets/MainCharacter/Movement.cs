using Terresquall;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public VirtualJoystick      _joystick;
    public Character            _character;
    public Animator             _playerAnim;

    public bool                 _isIdle;
    public bool                 _isRunning;
    public bool                 _isDead;
    public bool                 _isPause;
    public bool                 _isAllowMove;

    private AnimationController _animController;

    public void Init(Character character, VirtualJoystick joys, Animator anim)
    {
        this._character = character;
        this._joystick = joys;
        this._playerAnim = anim;

        _isIdle = false;
        _isRunning = false;
        _isDead = false;
        _isPause = false;

        _animController = new AnimationController(anim);
    }

    private void FixedUpdate()
    {
        Vector2 joystickInput = this._joystick.axis;
        Vector3 movement = new Vector3(joystickInput.x, 0, joystickInput.y);
        if (_isDead || _isPause) return;
        if (movement == Vector3.zero)
        {
            if (_isIdle) return;
            _isIdle = true;
            _isRunning = false;
            _animController.MainCharacterIdle();
            return;
        }
        else
        {
            if (!_isRunning) { 
                _isRunning = true;
                _isIdle = false;
                _animController.MainCharacterRun();
            }
        }
        if (!IsWallInFront(-movement)) transform.Translate(-movement * this._character.MoveSpeed * Time.deltaTime, Space.World);
        if (joystickInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(-movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    private bool IsWallInFront(Vector3 direction)
    {
        float rayDistance = 1f;
        Ray ray = new Ray(transform.position, direction);
        return Physics.Raycast(ray, rayDistance, LayerMask.GetMask("Wall"));
    }

    public void PlayerIsDead()
    {
        _isDead = true;
    }

    public void DecreaseMoveSpeed(float speed)
    {
        this._character.MoveSpeed -= speed;
    }

    public void IncreaseMoveSpeed(float speed)
    {
        this._character.MoveSpeed += speed;
    }


}
