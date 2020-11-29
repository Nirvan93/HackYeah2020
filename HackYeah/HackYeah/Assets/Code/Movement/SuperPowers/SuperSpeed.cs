using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpeed : MonoBehaviour
{
    public static PlayerController Player { get { return PlayerController.Instance; } }

    public float MaxDrawingTime = 4f;
    public float DrawPointInterval = 0.1f;
    public float OnePointMoveDur = 0.05f;
    public LineRenderer line;
    public MovementMotor Motor;

    private bool drawing = false;
    private float drawElapsed = 0f;
    private float drawInterval = 0f;
    private int drawingPoints = 0;

    private List<Vector3> drawedPath;
    private AudioSource src;

    public void Start()
    {
        drawedPath = new List<Vector3>();
        OnePointMoveDur = 0.085f;
        src = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (!SuperSpeedPower.SuperSpeedActivated)
            return;

        if (!drawing)
            if (Input.GetMouseButtonDown(0))
                StartDrawing();

        if (drawing)
        {
            if (Input.GetMouseButtonUp(0)) StopDrawing();
            else UpdateDrawing();
        }

        if (src)
        {
            if (src.volume > 0f) src.volume -= Time.deltaTime * 1.6f;
            src.pitch = Mathf.Lerp(src.pitch, Player.R.velocity.magnitude * Time.fixedDeltaTime * 2.25f, Time.deltaTime * 4f);
        }
    }


    void UpdateDrawing()
    {
        drawElapsed += Time.deltaTime;
        drawedPath[0] = Player.transform.position;
        drawedPath[drawingPoints - 1] = GetCursorPosition();
        line.positionCount = drawingPoints;
        line.SetPositions(drawedPath.ToArray());

        drawInterval += Time.deltaTime;

        if (drawInterval >= DrawPointInterval)
        {
            drawInterval -= DrawPointInterval;
            drawedPath.Add(GetCursorPosition());
            drawingPoints++;
        }


        if (drawElapsed > MaxDrawingTime) StopDrawing();
    }

    void StartDrawing()
    {
        line.enabled = true;

        drawedPath.Clear();
        drawing = true;
        drawElapsed = 0f;
        drawInterval = 0f;

        drawedPath.Add(Player.transform.position);
        drawedPath.Add(GetCursorPosition());
        drawingPoints = 2;
    }

    void StopDrawing()
    {
        drawing = false;
        line.enabled = false;

        Player.StartCoroutine(CApplyPathToPlayer());
    }

    Vector3 GetCursorPosition()
    {
        var mouse = Input.mousePosition; mouse.z = (Player.transform.position.z - Camera.main.transform.position.z);
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        mouse.z = Player.transform.position.z;
        return mouse;
    }



    private IEnumerator CApplyPathToPlayer()
    {
        if (drawedPath.Count < 3) yield break;

        Vector3[] path = drawedPath.ToArray();
        Vector3 off = Vector3.down / 2f;
        PlayerController.Instance.SwitchOffGravity = true;

        for (int i = 0; i < path.Length; i++)
        {
            Vector3 startJumpPos = PlayerController.Instance.transform.position + off;

            Vector3 nextPos;
            if (i < path.Length - 1) nextPos = path[i + 1];
            else nextPos = path[i] + (path[i] - path[i - 1]);

            float stepLength = Vector3.Distance(path[i], nextPos);

            nextPos = Vector3.LerpUnclamped(path[i], nextPos, 1.5f);

            Vector3 initDir = ((drawedPath[i] + off) - (startJumpPos + off)).normalized;

            Vector3 fromPointToPointDir;
            if (i == 0) fromPointToPointDir = path[0] - Player.transform.position;
            else fromPointToPointDir = nextPos - path[i];
            fromPointToPointDir.Normalize();

            float elapsed = 0f;

            while (true)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / OnePointMoveDur;
                Vector3 veloPoint = Vector3.Lerp(startJumpPos, nextPos, progress);


                if ((Player.transform.position - nextPos).magnitude < 1.5f)
                {
                    Vector3 currentDir = (nextPos - Player.transform.position).normalized;
                    float dot = Vector3.Dot(initDir, currentDir);
                    if (dot < 0.5f) break;
                }

                //Debug.Log("stepLength " + stepLength);
                Motor.Limit = stepLength;
                Motor.targetPos = veloPoint;
                Motor.Update(Player.transform.position);
                Player.OverrideVelocity(Motor.Output * 14f);
                Player.IsGrounded = false;

                if (src)
                {
                    if (src.volume < 0.4f)
                        src.volume += stepLength * Time.deltaTime * 4.25f;
                }

                if (progress > .9f) break;
                yield return null;
            }
        }

        Motor.Reset();
        Motor.targetPos = Vector3.zero;
        PlayerController.Instance.SwitchOffGravity = false;
    }

}
