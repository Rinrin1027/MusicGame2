using System.Collections.Generic;
using UnityEngine;

public class UnityMainThreadDispatcher : MonoBehaviour
{
    private static readonly Queue<System.Action> executionQueue = new Queue<System.Action>();

    public static UnityMainThreadDispatcher Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        lock (executionQueue)
        {
            while (executionQueue.Count > 0)
            {
                executionQueue.Dequeue()?.Invoke();
            }
        }
    }

    public void Enqueue(System.Action action)
    {
        lock (executionQueue)
        {
            executionQueue.Enqueue(action);
        }
    }
}
