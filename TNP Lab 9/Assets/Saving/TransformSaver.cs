using UnityEngine;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class TransformData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public TransformData(Transform t)
    {
        position = t.localPosition;
        rotation = t.localRotation;
        scale = t.localScale;
    }
}

[System.Serializable]
public class SceneTransforms
{
    public TransformData player;
    public List<TransformData> enemies = new List<TransformData>();
}

public class TransformSaver : MonoBehaviour
{
    public Transform playerTransform;
    public List<Transform> enemyTransforms;
    public EnemySpawner enemySpawner; // Add this and assign in Inspector

    private string savePath => Path.Combine(Application.persistentDataPath, "sceneTransforms.json");
    private string pointsPath => Path.Combine(Application.persistentDataPath, "points.dat");

    public void SaveTransforms()
    {
        CleanupEnemyTransforms(); // Add this line

        SceneTransforms sceneData = new SceneTransforms
        {
            player = new TransformData(playerTransform)
        };

        foreach (var enemy in enemyTransforms)
        {
            sceneData.enemies.Add(new TransformData(enemy));
        }

        string json = JsonUtility.ToJson(sceneData, true);
        File.WriteAllText(savePath, json);

        SavePointsBinary();

        Debug.Log("Transforms saved to: " + savePath);
        Debug.Log("Points saved to: " + pointsPath);
    }

    public void LoadTransforms()
    {
        CleanupEnemyTransforms(); // Add this line

        if (!File.Exists(savePath)) return;

        string json = File.ReadAllText(savePath);
        SceneTransforms sceneData = JsonUtility.FromJson<SceneTransforms>(json);

        if (sceneData.player != null)
        {
            playerTransform.localPosition = sceneData.player.position;
            playerTransform.localRotation = sceneData.player.rotation;
            playerTransform.localScale = sceneData.player.scale;
        }

        for (int i = 0; i < enemyTransforms.Count && i < sceneData.enemies.Count; i++)
        {
            enemyTransforms[i].localPosition = sceneData.enemies[i].position;
            enemyTransforms[i].localRotation = sceneData.enemies[i].rotation;
            enemyTransforms[i].localScale = sceneData.enemies[i].scale;
        }

        LoadPointsBinary();

        Debug.Log("Transforms loaded from: " + savePath);
        Debug.Log("Points loaded from: " + pointsPath);
    }

    private void SavePointsBinary()
    {
        if (enemySpawner == null) return;
        using (var fs = new FileStream(pointsPath, FileMode.Create, FileAccess.Write))
        using (var writer = new BinaryWriter(fs))
        {
            writer.Write(enemySpawner.Points);
        }
    }

    private void LoadPointsBinary()
    {
        if (enemySpawner == null || !File.Exists(pointsPath)) return;
        using (var fs = new FileStream(pointsPath, FileMode.Open, FileAccess.Read))
        using (var reader = new BinaryReader(fs))
        {
            enemySpawner.Points = reader.ReadInt32();
        }
    }

    private void CleanupEnemyTransforms()
    {
        enemyTransforms.RemoveAll(t => t == null);
    }
}
