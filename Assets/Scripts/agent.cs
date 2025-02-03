using System.Collections;
using System.Diagnostics.Tracing;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private enum State
    {
        Idle,
        Walk,
        Sick,
        Death
    }

    private State currentState = State.Idle;

    private Vector2 startpos = Vector2.zero;
    private Vector2 target = Vector2.zero;

    public Camera cam = null;

    private void Start()
    {
        // Sätter initial position och startar Idle state
        startpos = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 5f));
        transform.position = startpos;
        ChangeState(State.Idle);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Tryckt");
            StartCoroutine(Countdown2(1.0f));
        }

        switch (currentState)
        {
            case State.Idle:
                // Agenten står still
                break;

            case State.Walk:
                // Agenten rör sig mot målet
                transform.position = Vector2.MoveTowards(transform.position, target, 5 * Time.deltaTime);
                if ((Vector2)transform.position == target)
                {
                    ChangeState(State.Idle); // Återgår till Idle när målet nås
                }
                break;

            case State.Sick:
                // Implementera logik för "Sick"-state
                // T.ex. sakta ner rörelsen eller spela en animation
                break;

            case State.Death:
                // Implementera logik för "Death"-state
                // T.ex. spela dödsanimation eller avaktivera objektet
                break;
        }
    }

    private void ChangeState(State newState)
    {
        //StopAllCoroutines(); // Stoppar alla pågående coroutines när vi byter state
        currentState = newState;

        switch (currentState)
        {
            case State.Idle:
                StartCoroutine(IdleStateRoutine());
                break;

            case State.Walk:
                target = new Vector2(Random.Range(-9f, 9f), Random.Range(-5f, 5f));
                //Debug.Log($"New target: {target}");
                break;

            case State.Sick:
                // Här kan du lägga in logik som startar animationer eller ändrar agentens beteende
                //Debug.Log("Agent is sick.");
                break;

            case State.Death:
                // Avsluta agentens livscykel här
                //Debug.Log("Agent has died.");
                break;
        }
    }

    private IEnumerator IdleStateRoutine()
    {
        //Debug.Log("Idle state");
        yield return new WaitForSeconds(1.0f);
        ChangeState(State.Walk); // Går över till Walk efter att ha varit i Idle ett tag
    }

    // Publika metoder för att ändra tillstånd externt
    public void SetSick()
    {
        ChangeState(State.Sick);
    }

    public void SetDeath()
    {
        ChangeState(State.Death);
    }
    private IEnumerator Countdown(float sec)
    {
        float counter = sec;
        Debug.Log($"Start Countdown: {sec}");
        while (counter > 0)
        {
            Debug.Log(sec);
            yield return new WaitForSeconds(sec);
            Debug.Log($"Countdown: DONE");
            counter--;
        }
        
        
    }
    private IEnumerator Countdown2(float sec)
    {
        Debug.Log($"Start Countdown: {sec}");
        yield return new WaitForSeconds(sec);
        Debug.Log($"Countdown: DONE");
    }


    
}
