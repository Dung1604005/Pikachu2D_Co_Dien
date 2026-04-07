using UnityEngine;

public class PathVisual : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        //Set up information for line

        

        
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    public void RenderLine(int pointAmount, Vector2[] positionList)
    {
        lineRenderer.positionCount = pointAmount;
        for(int i = 0; i < positionList.Length; i++)
        {
            lineRenderer.SetPosition(i, positionList[i]);
        }

        Invoke(nameof(ClearLine), 0.5f);
    }

    private void ClearLine()
    {
        lineRenderer.positionCount = 0;
    }
}
