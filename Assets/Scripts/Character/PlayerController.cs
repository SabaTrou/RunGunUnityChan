using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController
{
    private Gamepad _gamePad;
    private bool _isGamePadSet = default;
    private BasePlayerCharacter _controllCharacter;
    private bool _isCharacterSet = default;
   public void UpdateCpntroller()
    {
        
        if(!_isCharacterSet)
        {
            return;
        }
       
        if (!_isGamePadSet)
        {
            KeyBoardUpdate();
        }
        
        
    }
    private void KeyBoardUpdate()
    {
       
        Vector2 keybordMoveValue = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _controllCharacter.MoveCharacter((keybordMoveValue).normalized);
       if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            _controllCharacter.FireMainSkill();
        }
       if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            
        }
    }
    public void SetGamePad(Gamepad gamepad)
    {
        _gamePad = gamepad;
        
        _isGamePadSet=true;
    }
    public void SetCharacter(BasePlayerCharacter character)
    {
        _controllCharacter = character;
        
        _isCharacterSet=true;
    }
}
