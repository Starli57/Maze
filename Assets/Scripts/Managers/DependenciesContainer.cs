
using UnityEngine;
using UnityEngine.Assertions;

public class DependenciesContainer : MonoBehaviour
{
	public static DependenciesContainer Instance
    {
        get
        {
			if (_instance == null)
            {
				_instance = FindObjectOfType<DependenciesContainer>();
            }

			return _instance;
		}
    }

	[Header("Scriptable objects")]
	[SerializeField] public MazeConfiguration mazeConfig;

	[Header("Scene objects")]
	[SerializeField] public MazeCamera mazeCamera;

	[Header("Maze")]
	[SerializeField] public Map mapBuilder;

	[Header("Pathfinding")]
	[SerializeField] public Pathfinder pathfinder;

	private static DependenciesContainer _instance;
}	
	
