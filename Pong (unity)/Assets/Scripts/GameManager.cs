using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ball ball;

    public Text player_score;
    public Text comp_score;

    public Paddle player_paddle;
    public Paddle comp_paddle;

    private int player_scr;
    private int computer_scr;

    public void PlayerScores()
    {
        player_scr++;
        this.player_score.text = player_scr.ToString();
        ResetAll();
    }

    public void ComputerScores()
    {
        computer_scr++;
        this.comp_score.text = computer_scr.ToString();
        ResetAll();
    }

    private void ResetAll()
    {
        this.player_paddle.ResetPosition();
        this.comp_paddle.ResetPosition();
        this.ball.ResetPosition();
        this.ball.AddStartingForce();
    }
}
