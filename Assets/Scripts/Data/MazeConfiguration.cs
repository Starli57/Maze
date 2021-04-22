using UnityEngine;

[CreateAssetMenu()]
public class MazeConfiguration : ScriptableObject
{
    public int width;
    public int height;
    public float gapsChance;
}
