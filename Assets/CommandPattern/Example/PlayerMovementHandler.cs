using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementHandler : MonoBehaviour
{
    public float moveDistance = 10f;
    public GameObject objectToMove;

    private List<Command> commands = new List<Command>();
    private int currentCommandNum = 0;

    void Start()
    {
        if (objectToMove == null)
        {
            Debug.LogError("objectToMove must be assigned via inspector");
            this.enabled = false;
        }
    }

    private void Move(MoveDirection direction)
    {
        MoveCommand moveCommand = new MoveCommand(direction, moveDistance, objectToMove);
        moveCommand.Execute();
        commands.Add(moveCommand);
        currentCommandNum++;
    }

    public void Undo()
    {
        if (currentCommandNum > 0)
        {
            currentCommandNum--;
            MoveCommand moveCommand = (MoveCommand)commands[currentCommandNum];
            moveCommand.UnExecute();
            commands.Remove(moveCommand);
        }
    }




    //Simple move commands to attach to UI buttons
    public void MoveUp() { Move(MoveDirection.up); }
    public void MoveDown() { Move(MoveDirection.down); }
    public void MoveLeft() { Move(MoveDirection.left); }
    public void MoveRight() { Move(MoveDirection.right); }

}
