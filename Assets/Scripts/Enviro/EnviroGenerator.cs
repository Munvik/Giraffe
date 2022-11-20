using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviroGenerator : MonoBehaviour
{
    [SerializeField] private List<EnviroChunk> chunks;

    [Header("Movement Params")]
    public int chunkPhysicalSize;
    public int chunkDeletePosition;
    public float enviromentMovementSpeed;
    public int preGenereteChunks;
    public bool generateBothWays;

    private List<EnviroChunk> currentChunks = new List<EnviroChunk>();
    public int currentChunkIndex = 0;
    private int pointer = 0;

    public bool testMode;

    private void Start()
    {
        if (testMode)
            PreGenerate();
    }

    private void Update()
    {
        if (testMode)
            UpdateEnviro();
    }

    public void PreGenerate()
    {
        if (generateBothWays)
        {
            for (int i = -preGenereteChunks; i < 0; i++)
            {
                AddChunk(i * chunkPhysicalSize ,false);
            }
        }

        for(int i = 0; i < preGenereteChunks; i++)
        {
            AddChunk(currentChunkIndex);
        }
    }

    public void AddChunk(int position, bool addIndex = true)
    {
        EnviroChunk chunk = Instantiate(chunks[pointer % chunks.Count], transform);
        chunk.transform.localPosition = new Vector3(0f, 0f, position);
        currentChunks.Add(chunk);
        currentChunkIndex += addIndex ? chunkPhysicalSize : 0;
        pointer++;
    }

    public void RemoveChun()
    {
        EnviroChunk chunk = currentChunks[0];
        currentChunks.Remove(chunk);

        GameObject.Destroy(chunk.gameObject);
    }

    public void UpdateEnviro()
    {
        transform.localPosition += new Vector3(0f, 0f, -enviromentMovementSpeed * Time.deltaTime);
        if (currentChunks[0].transform.position.z <= chunkDeletePosition)
        {
            AddChunk(currentChunkIndex);
            RemoveChun();
        }
    }
}
