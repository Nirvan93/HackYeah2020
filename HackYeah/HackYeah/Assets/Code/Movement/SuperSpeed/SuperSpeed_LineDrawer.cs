using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpeed_LineDrawer : MonoBehaviour
{
    public static SuperSpeed_LineDrawer Instance;
    private void Awake() { Instance = this; }

    public float MaxDrawingTime = 4f;
    public float DrawPointInterval = 0.25f;
    public float OnePointMoveDur = 0.05f;
    public LineRenderer line;

    private bool drawing = false;
    private float drawElapsed = 0f;
    private float drawInterval = 0f;
    private int drawingPoints = 0;

    private List<Vector3> drawedPath;

    private void Start()
    {
        drawedPath = new List<Vector3>();
    }

    void Update()
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

        drawedPath.Add(GetCursorPosition());
        drawedPath.Add(GetCursorPosition());
        drawingPoints = 2;
    }

    void StopDrawing()
    {
        drawing = false;
        line.enabled = false;

        StartCoroutine(CApplyPathToPlayer());
    }

    Vector3 GetCursorPosition()
    {
        var mouse = Input.mousePosition; mouse.z = (PlayerController.Instance.transform.position.z - Camera.main.transform.position.z);
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        mouse.z = PlayerController.Instance.transform.position.z;
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
            Vector3 initDir = ((drawedPath[i] + off) - (startJumpPos + off)).normalized;
            float elapsed = 0f;

            while (true)
            {
                Vector3 nextPos;
                if (i < path.Length - 1) nextPos = path[i + 1];
                else nextPos = path[i] + (path[i] - path[i - 1]);

                elapsed += Time.deltaTime;
                float progress = elapsed / OnePointMoveDur;
                Vector3 veloPoint = Vector3.Lerp(startJumpPos, nextPos, progress);

                Debug.DrawRay(veloPoint, Vector3.forward, Color.red, 0.25f);

                //PlayerController.Instance.R.MovePosition(veloPoint);
                PlayerController.Instance.Motor.targetPos = veloPoint;
                //PlayerController.Instance.Motor.RushAcceleration(Vector3.Distance(startJumpPos, veloPoint));

                if (progress > 1f) break;
                yield return null;
            }
        }

        PlayerController.Instance.Motor.targetPos = Vector3.zero;
        PlayerController.Instance.SwitchOffGravity = false;
    }
}
