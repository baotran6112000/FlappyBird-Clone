using System.Collections.Generic;
using UnityEngine;

public class PipePool : SingletonMonoBehaviour<PipePool>
{
    [SerializeField] var_Spawn spawn;
    [SerializeField] var_Player player;
    [SerializeField] GameObject pipePrefab;
    [SerializeField] GameObject parrent;
    int poolSize = 2;


    private List<GameObject> pipes;

    public List<GameObject> Pipes { get => pipes; set => pipes = value; }

    private void Start()
    {
        Pipes = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            var pipe = Instantiate(pipePrefab);
            pipe.transform.parent = parrent.transform;
            pipe.SetActive(false);
            Pipes.Add(pipe);
        }
        spawn.timer = spawn.maxTime;
    }

    private void Update()
    {
        if (spawn.timer > spawn.maxTime)
        {
            SpawnPipe();
            spawn.timer = 0;
        }
        spawn.timer += Time.deltaTime;
    }

    private void SpawnPipe()
    {
        var pipe = GetPipeFromPool();
        if (pipe != null)
        {
            pipe.transform.position = new Vector3(transform.position.x, transform.position.y + Random.Range(-spawn.height, spawn.height), transform.position.z);
            pipe.SetActive(true);
            player.isChange = false;
        }
    }


    private GameObject GetPipeFromPool()
    {
        foreach (var pipe in Pipes)
        {
            if (!pipe.activeInHierarchy)
            {
                return pipe;
            }
        }
        var newPipe = Instantiate(pipePrefab);
        newPipe.SetActive(false);
        Pipes.Add(newPipe);
        return newPipe;
    }

    public void ReturnPipeToPool(GameObject pipe)
    {
        pipe.SetActive(false);
    }
}
