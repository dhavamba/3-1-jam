using UnityEngine;

public abstract class AsynchronousEvent : MonoBehaviour
{
    private int asynchronous;

    public void AddControlFinishWork()
    {
        asynchronous++;
        if (asynchronous >= 2)
        {
            asynchronous = 0;
            FinishAsynchronousWork();
        }
    }

    private void SendAsynchronous(Collider2D collider)
    {
        collider.GetComponent<AsynchronousEvent>()?.AddControlFinishWork();
    }

    protected void ControlFinishWork(Collider2D collider)
    {
        if (collider.GetComponent<IAsynchronous>() == null)
        {
            asynchronous++;
        }

        SendAsynchronous(collider);
        AddControlFinishWork();
    }

    private void OnDisable()
    {
        asynchronous = 0;
    }

    protected abstract void FinishAsynchronousWork();
}