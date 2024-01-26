using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawingPanel : MonoBehaviour
{
    public EndGameUI endGameUI;
    public List<GameObject> Runners;
    public int RunnersCount;
    public GameObject RunnerPrefab;
    public float FixedHeight;
    public Transform SpawnPlane;
    public Image DrawingImage; // �������� ������ Image
    public int CrystalsCollected = 0;


    private LineRenderer _lineRenderer;
    private List<Vector3> _positions;

    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _positions = new List<Vector3>();
        HideLineRenderer();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // ���������, �������� �� ��� � ���� ���������
            if (IsTouchInDrawingArea(touch.position))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    StartDrawing(touch.position);
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    ContinueDrawing(touch.position);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    EndDrawing();
                }
            }
        }
        if (RunnersCount <= 0)
        {
            endGameUI.ShowEndGameUI(false);
        }
    }

    // �������� ����� ��� �������� ��������� ���� � ���� ���������
    bool IsTouchInDrawingArea(Vector2 touchPosition)
    {
        RectTransform drawingRectTransform = DrawingImage.rectTransform;
        Vector3[] corners = new Vector3[4];
        drawingRectTransform.GetWorldCorners(corners);

        Rect drawingRect = new Rect(corners[0], new Vector2(corners[2].x - corners[0].x, corners[1].y - corners[0].y));

        return drawingRect.Contains(touchPosition);
    }

    void StartDrawing(Vector3 position)
    {
        _positions.Clear();
        _positions.Add(ProjectToSpawnPlane(position));
        _lineRenderer.positionCount = 1;
        _lineRenderer.SetPosition(0, _positions[0]);
        ShowLineRenderer();
    }

    void ContinueDrawing(Vector3 position)
    {
        Vector3 projectedPosition = ProjectToSpawnPlane(position);
        _positions.Add(projectedPosition);

        _lineRenderer.positionCount = _positions.Count;
        _lineRenderer.SetPositions(_positions.ToArray());
    }

    void EndDrawing()
    {
        // ������ �������� ������� ��� �������� ������� �� �������
        CreateRunners(RunnersCount);

        // �������� ����� ���������
        HideLineRenderer();
    }

    Vector3 ProjectToSpawnPlane(Vector3 position)
    {
        // �������� ���������� ����� �� ��������� ������
        Ray ray = Camera.main.ScreenPointToRay(position);
        Plane spawnPlane = new Plane(SpawnPlane.up, SpawnPlane.position);
        float distance;
        spawnPlane.Raycast(ray, out distance);
        Vector3 projectedPoint = ray.GetPoint(distance);

        return projectedPoint;
    }

    void CreateRunners(int numberOfRunners)
    {
        if (numberOfRunners <= 0)
        {
            Debug.LogError("���������� ������� ������ ���� ������������� ������.");
            return;
        }

        // ������� ���������� ������� ����� ��������� �����
        ClearRunners();

        // ����������� ��� ����� ������� �������, ����� ������������ ������� ����������
        float step = (float)(_positions.Count - 1) / (float)(numberOfRunners - 1);

        // ��������� �� ���� ������ ������� � ���������� �������
        for (int i = 0; i < numberOfRunners; i++)
        {
            // ����������� ������ ����� ������� ��� �������� ������
            int splineIndex = Mathf.RoundToInt(i * step);

            // ������ �� ������ �� ������� �������
            splineIndex = Mathf.Clamp(splineIndex, 0, _positions.Count - 1);

            // ����� ����������� ���������� �� _positions[splineIndex] ��� ���������� ������ � ������ �������
            Vector3 runnerPosition = new Vector3(_positions[splineIndex].x, FixedHeight, _positions[splineIndex].z);

            // �������������� ������������ �����, ���� ��������
            GameObject runner = GetOrCreateRunner(i);

            // ������� ������� � ������ ��������� ������
            runner.transform.position = runnerPosition;

            // ������� ������ �������� �������� � ���� ������
            runner.transform.SetParent(SpawnPlane);

            // ������ �������� ��������� ����� � ������
            Runners.Add(runner);
        }
    }

    void ClearRunners()
    {
        // ������� ���� ���������� �������
        foreach (var runner in Runners)
        {
            Destroy(runner);
        }

        // ������� ������
        Runners.Clear();
    }

    GameObject GetOrCreateRunner(int index)
    {
        // �������������� ������������ �����, ���� ������ �� ��������� ���������� ������������ �������
        if (index < Runners.Count)
        {
            return Runners[index];
        }
        else
        {
            // ����� ������� ����� �����
            return Instantiate(RunnerPrefab);
        }
    }

    void ShowLineRenderer()
    {
        _lineRenderer.enabled = true;
    }

    void HideLineRenderer()
    {
        _lineRenderer.enabled = false;
    }
}
