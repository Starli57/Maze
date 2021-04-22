using System;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Action onMapChanged;

    public bool[,] map { get; private set; }  // false - wall, true - road

    public void GenerateMaze(int width, int height, float gaps)
    {
        map = _mazeGenerator.Generate(width, height, gaps);
        _mazeVisualizer.Visualise(map);

        PlaceHero();

        onMapChanged?.Invoke();
    }

#pragma warning disable 649
    [SerializeField] private MazeVisualizer _mazeVisualizer;
    [SerializeField] private MazeConfiguration _defaultConfiguration;

    [Space]
    [SerializeField] private Transform _hero;
#pragma warning restore 649

    private MazeGenerator _mazeGenerator;
    
    private void Awake()
    {
        _mazeGenerator = new MazeGenerator(new PrimsMazeGenerator());

        GenerateMaze(
            _defaultConfiguration.width, 
            _defaultConfiguration.height, 
            _defaultConfiguration.gapsChance);
    }
    
    private void PlaceHero()
    {
        int x = UnityEngine.Random.Range(0, map.GetLength(1));
        int y = UnityEngine.Random.Range(0, map.GetLength(0));

        var closestRoad = MazeHelper.GetClosestRoad(map, x, y);

        _hero.position = new Vector3(closestRoad.Item1, 0, closestRoad.Item2);
    }
}
