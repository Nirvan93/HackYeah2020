using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpeed : MonoBehaviour
{
    public static PlayerController Player { get { return PlayerController.Instance; } }

    public float MaxDrawingTime = 4f;
    public float DrawPointInterval = 0.25f;
    public float OnePointMoveDur = 0.05f;
    public LineRenderer line;
    public MovementMotor Motor;

    private bool drawing = false;
    private float drawElapsed = 0f;
    private float drawInterval = 0f;
    private int drawingPoints = 0;

    private List<Vector3> drawedPath;

    public void Start()
    {
        drawedPath = new List<Vector3>();
    }

    public void Update()
    {
        if (!drawing)
            if (Input.GetMouseButtonDown(0))
                StartDrawing();

        if (drawing)
        {
            if (Input.GetMouseButtonUp(0)) StopDrawing();
            else UpdateDrawing();
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

                Vector3 currentDir = (veloPoint - Player.transform.position).normalized;
                float dot = Vector3.Dot(initDir, currentDir);
                if (dot < 0.2f) break;

                //PlayerController.Instance.Motor.RushAcceleration(Vector3.Distance(startJumpPos, veloPoint));
                Motor.targetPos = veloPoint;
                Motor.Update(Player.transform.position);
                Player.OverrideVelocity(Motor.Output * 14f);

                if (progress > 1f) break;
                yield return null;
            }
        }

        Motor.Reset();
        Motor.targetPos = Vector3.zero;
        PlayerController.Instance.SwitchOffGravity = false;
    }

}
