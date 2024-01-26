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
    public Image DrawingImage; // Добавлен объект Image
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

            // Проверяем, попадает ли тач в зону рисования
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

    // Добавлен метод для проверки попадания тача в зону рисования
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
        // Теперь вызываем функцию для создания бегунов по сплайну
        CreateRunners(RunnersCount);

        // Скрываем линию рисования
        HideLineRenderer();
    }

    Vector3 ProjectToSpawnPlane(Vector3 position)
    {
        // Получаем координаты точки на плоскости спавна
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
            Debug.LogError("Количество бегунов должно быть положительным числом.");
            return;
        }

        // Удаляем предыдущих бегунов перед созданием новых
        ClearRunners();

        // Рассчитайте шаг между точками сплайна, чтобы распределить бегунов равномерно
        float step = (float)(_positions.Count - 1) / (float)(numberOfRunners - 1);

        // Проходите по всем точкам сплайна и создавайте бегунов
        for (int i = 0; i < numberOfRunners; i++)
        {
            // Рассчитайте индекс точки сплайна для текущего бегуна
            int splineIndex = Mathf.RoundToInt(i * step);

            // Защита от выхода за пределы массива
            splineIndex = Mathf.Clamp(splineIndex, 0, _positions.Count - 1);

            // Здесь используйте информацию из _positions[splineIndex] для размещения бегуна в нужной позиции
            Vector3 runnerPosition = new Vector3(_positions[splineIndex].x, FixedHeight, _positions[splineIndex].z);

            // Переиспользуем существующий бегун, если возможно
            GameObject runner = GetOrCreateRunner(i);

            // Обновим позицию и другие параметры бегуна
            runner.transform.position = runnerPosition;

            // Сделаем бегуна дочерним объектом к зоне спавна
            runner.transform.SetParent(SpawnPlane);

            // Теперь добавьте созданный бегун в список
            Runners.Add(runner);
        }
    }

    void ClearRunners()
    {
        // Удаляем всех предыдущих бегунов
        foreach (var runner in Runners)
        {
            Destroy(runner);
        }

        // Очищаем список
        Runners.Clear();
    }

    GameObject GetOrCreateRunner(int index)
    {
        // Переиспользуем существующий бегун, если индекс не превышает количество существующих бегунов
        if (index < Runners.Count)
        {
            return Runners[index];
        }
        else
        {
            // Иначе создаем новый бегун
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
