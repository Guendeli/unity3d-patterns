using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirection { up, down, left, right };
class MoveCommand : Command
{
    private MoveDirection _direction;
    private float _distance;
    private GameObject _gameObject;


    //Constructor
    public MoveCommand(MoveDirection direction, float distance, GameObject gameObjectToMove)
    {
        this._direction = direction;
        this._distance = distance;
        this._gameObject = gameObjectToMove;
    }



    //Execute new command
    public override void Execute()
    {
        MoveOperation(_gameObject, _direction, _distance);
    }


    //Undo last command
    public override void UnExecute()
    {
        MoveOperation(_gameObject, InverseDirection(_direction), _distance);
    }


    //invert the direction for undo
    private MoveDirection InverseDirection(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.up:
                return MoveDirection.down;
            case MoveDirection.down:
                return MoveDirection.up;
            case MoveDirection.left:
                return MoveDirection.right;
            case MoveDirection.right:
                return MoveDirection.left;
            default:
                Debug.LogError("Unknown MoveDirection");
                return MoveDirection.up;
        }
    }

    public void MoveOperation(GameObject gameObjectToMove, MoveDirection direction, float distance)
    {
        switch (direction)
        {
            case MoveDirection.up:
                MoveZ(gameObjectToMove, distance);
                break;
            case MoveDirection.down:
                MoveZ(gameObjectToMove, -distance);
                break;
            case MoveDirection.left:
                MoveX(gameObjectToMove, -distance);
                break;
            case MoveDirection.right:
                MoveX(gameObjectToMove, distance);
                break;
        }
    }

    private void MoveZ(GameObject gameObjectToMove, float distance)
    {
        Vector3 newPos = gameObjectToMove.transform.position;
        newPos.z += distance;
        gameObjectToMove.transform.position = newPos;
    }

    private void MoveX(GameObject gameObjectToMove, float distance)
    {
        Vector3 newPos = gameObjectToMove.transform.position;
        newPos.x += distance;
        gameObjectToMove.transform.position = newPos;
    }
}
