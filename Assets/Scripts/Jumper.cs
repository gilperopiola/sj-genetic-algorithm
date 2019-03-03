using UnityEngine;

public class Jumper : MonoBehaviour
{
    //variables
    private int timer = 0;
    private int totalFrames = 0;
    private float speed = 0;
    private int nJumps = 0;
    private bool isJumping = false;
    private Individual individual;

    //constants
    private Rigidbody2D rigidBody;
    private float gravity = -0.02f;
    private float startingSpeed = 0.3f;


    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Globals.isPaused)
        {
            return;
        }

        timer++;
        totalFrames++;

        if (timer >= individual.dna.jumpFrames[nJumps])
        {
            timer = int.MinValue;
            StartJump();
        }

        if (isJumping)
        {
            rigidBody.MovePosition(new Vector3(0, transform.position.y + speed, 0));
            speed += gravity;
            speed = Mathf.Round(speed * 100f) / 100f; //2 decimals

            if (speed <= -startingSpeed)
            {
                EndJump();
            }
        }
    }

    public void SetIndividual(Individual newIndividual)
    {
        individual = newIndividual;
    }

    void StartJump()
    {
        ScoreManager.Add(0.5f);
        speed = startingSpeed;
        isJumping = true;
    }

    void EndJump()
    {
        timer = 0;
        nJumps++;
        ScoreManager.Add(1);
        ResetPosition();
    }

    void ResetPosition()
    {
        rigidBody.MovePosition(new Vector3(0, -1.44f, 0));
        speed = 0;
        isJumping = false;
    }

    void ResetVariables()
    {
        timer = 0;
        totalFrames = 0;
        nJumps = 0;
    }

    void Lose()
    {
        Globals.isPaused = true;
        individual.fitness = ScoreManager.score + ((float)totalFrames / (float)50);
        ResetPosition();
        ResetVariables();
        GameObject.Find("Ball").GetComponent<Ball>().ResetRotation();
        ScoreManager.Reset();
        PopulationHelper.Advance();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Lose();
    }
}
