using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public interface ISaveable
{
    string SaveID { get; }
    string SavedData { get; } // Use string to represent JSON
    void LoadFromData(string jsonData); // Accept JSON string for loading
}