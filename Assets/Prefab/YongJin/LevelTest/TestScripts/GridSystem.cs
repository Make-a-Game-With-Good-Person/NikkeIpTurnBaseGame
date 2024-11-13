using System;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject baseGridObject;
    public Vector2 gridSize;  // �� ����� ũ��
    [SerializeField] private LayerMask exclusionLayer;
    [SerializeField] private TestUnitMovement unit;
    private int rows = 0;
    private int cols = 0;
    private float gridWidth;
    private float gridHeight;

    private Vector3 firstClickPoint;
    private Vector3 secondClickPoint;
    private bool twoPointSelected = false;
    public bool didInit = false;

    private List<GridNode> nodes = new List<GridNode>();
    
    private Vector3 currentMousePosition;
    public event EventHandler<OnSecondSelectedPointArg> OnSecondSelectedPoint;
    public class OnSecondSelectedPointArg : EventArgs{
        public List<GridNode> path;
        //전송할 이벤트 프로퍼티 설정
    }
    private void Start()
    {
        unit.OnArrive += Unit_Arrive;
        InitBaseGrid();
        
    }

    private void Update()
    {
        HandleInput();
        UpdateMouseWorldPosition();
        
    }

    private void UpdateMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            currentMousePosition = hit.point;
        }
    }

    private void InitBaseGrid()
    {
        CalculateGridSize(baseGridObject);
        InitializeGrid();
        InitializeOccupiedNodes();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (!twoPointSelected)
                {
                    if (firstClickPoint == Vector3.zero)
                    {
                        firstClickPoint = hit.point;
                        Debug.Log($"첫번째 포지션: {firstClickPoint}");
                    }
                    else if (secondClickPoint == Vector3.zero)
                    {
                        secondClickPoint = hit.point;
                        Debug.Log($"두번쨰 포지션: {secondClickPoint}");
                        twoPointSelected = true;
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            firstClickPoint = Vector3.zero;
            secondClickPoint = Vector3.zero;
            twoPointSelected = false;
        }
    }
    private void Unit_Arrive(object sender, EventArgs e)
    {
        firstClickPoint = Vector3.zero;
        secondClickPoint = Vector3.zero;
        twoPointSelected = false;
    }

    private void CalculateGridSize(GameObject baseGrid)
    {
        if (baseGrid != null)
        {
            Vector3 baseSize = baseGrid.GetComponent<Collider>().bounds.size;

            gridWidth = baseSize.x;
            gridHeight = baseSize.z;


            rows = Mathf.FloorToInt(gridWidth / gridSize.x);
            cols = Mathf.FloorToInt(gridHeight / gridSize.y);
        }
        else
        {
            Debug.LogError("baseGridObject�� �������� �ʾҽ��ϴ�.");
        }
    }

    private void InitializeGrid()
    {
        Vector3 startPosition = baseGridObject.transform.position - new Vector3(gridWidth / 2, 0, gridHeight / 2);
        

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Vector3 nodePosition = startPosition + new Vector3(i * gridSize.x + gridSize.x / 2, 0, j * gridSize.y + gridSize.y / 2);
                nodes.Add(new GridNode(nodePosition, gridSize));
            }
        }
    }
   

    private void InitializeOccupiedNodes()
    {
        foreach (GridNode node in nodes)
        {
            Collider[] colliders = Physics.OverlapSphere(node.position, gridSize.x / 2, exclusionLayer);
            node.isOccupied = colliders.Length > 0;
        }
    }

    public GridNode GetNodeAtPosition(Vector3 position)
    {
        foreach (GridNode node in nodes)
        {
            if (Vector3.Distance(position, node.position) < gridSize.x / 2)
            {
                return node;
            }
        }
        return null;
    }

    public List<GridNode> FindPath(Vector3 startPosition, Vector3 endPosition)
    {
        GridNode startNode = GetNodeAtPosition(startPosition);
        GridNode endNode = GetNodeAtPosition(endPosition);

        if (startNode == null || endNode == null)
        {
            //Debug.LogError("���� �Ǵ� �� ��尡 ��ȿ���� �ʽ��ϴ�.");
            return null;
        }

        Queue<GridNode> queue = new Queue<GridNode>();
        Dictionary<GridNode, GridNode> cameFrom = new Dictionary<GridNode, GridNode>();
        queue.Enqueue(startNode);
        cameFrom[startNode] = null;

        while (queue.Count > 0)
        {
            GridNode currentNode = queue.Dequeue();

            if (currentNode == endNode)
                break;

            foreach (GridNode neighbor in GetNeighbors(currentNode))
            {
                if (neighbor.isOccupied) continue;

                if (!cameFrom.ContainsKey(neighbor))
                {
                    queue.Enqueue(neighbor);
                    cameFrom[neighbor] = currentNode;
                }
            }
        }

        if (!cameFrom.ContainsKey(endNode))
        {
            Debug.LogWarning("��θ� ã�� �� �����ϴ�.");
            return null;
        }

        List<GridNode> path = new List<GridNode>();
        GridNode current = endNode;

        while (current != null)
        {
            path.Add(current);
            current = cameFrom.ContainsKey(current) ? cameFrom[current] : null;
        }
        path.Reverse();

        return path;
    }


    private List<GridNode> GetNeighbors(GridNode node)
    {
        List<GridNode> neighbors = new List<GridNode>();

        Vector3[] directions = {
            new Vector3(0, 0, gridSize.y),
            new Vector3(0, 0, -gridSize.y),
            new Vector3(gridSize.x, 0, 0),
            new Vector3(-gridSize.x, 0, 0)
        };

        foreach (Vector3 direction in directions)
        {
            GridNode neighbor = GetNodeAtPosition(node.position + direction);
            if (neighbor != null)
                neighbors.Add(neighbor);
        }

        return neighbors;
    }

    private void OnDrawGizmos()
    {
        if (!didInit && nodes.Count == 0) InitBaseGrid();
        didInit = true;

        if (nodes == null || nodes.Count == 0) return;

        foreach (GridNode node in nodes)
        {
            Gizmos.color = node.isOccupied ? Color.red : Color.grey;
            Gizmos.DrawWireCube(node.position, new Vector3(gridSize.x, 0.1f, gridSize.y));
        }

        if (firstClickPoint != Vector3.zero)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(firstClickPoint, 0.5f);

            if (currentMousePosition != Vector3.zero)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(firstClickPoint, currentMousePosition);

                List<GridNode> path = FindPath(firstClickPoint, currentMousePosition);
                
                if (path != null)
                {
                    Gizmos.color = Color.yellow;
                    for (int i = 0; i < path.Count - 1; i++)
                    {
                        Gizmos.DrawLine(path[i].position, path[i + 1].position);
                    }
                }
            }
        }

        if (twoPointSelected && firstClickPoint != Vector3.zero && secondClickPoint != Vector3.zero)
        {
            List<GridNode> path = FindPath(firstClickPoint, secondClickPoint);
            OnSecondSelectedPoint?.Invoke(this, new OnSecondSelectedPointArg
            {
                path = path
            });
            if (path != null)
            {
                Gizmos.color = Color.yellow;
                for (int i = 0; i < path.Count - 1; i++)
                {
                    
                    Gizmos.DrawLine(path[i].position, path[i + 1].position);
                }
            }
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(secondClickPoint, 0.5f);
        }
    }
}
