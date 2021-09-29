using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    [SerializeField] private CharacterControllerScript player1;
    [SerializeField] private CharacterControllerScript player2;

    private bool _paused;
    
    // Update is called once per frame
    void Update()
    {
        MovePlayer1();
        MovePlayer2();
    }

    private void MovePlayer1()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            player1.MoveForward();
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            player1.MoveLeft();
                
        }
        else if (Input.GetKey(KeyCode.D))
        {
            player1.MoveRight();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            player1.MoveBackward();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player1.PlaceBomb();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            _paused = !_paused;
            PauseMenu.OnPauseMenu(_paused);
        }
    }
        
    private void MovePlayer2()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            player2.MoveForward();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            player2.MoveLeft();
                
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            player2.MoveRight();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            player2.MoveBackward();
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            player2.PlaceBomb();
        }
    }
}