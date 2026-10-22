using UnityEngine;

public static class ExpeditionMaps
{
    // # rock, . path, @ robot, * seed, ! hazard, C console, ? field notebook.
    public static readonly string[][] Levels = {
        new[] { "#########", "#@..*...#", "#.#..#..#", "#.?.....#", "#..#.*..#", "#...#...#", "#.*...C.#", "#.......#", "#########" },
        new[] { "#########", "#@.*....#", "#..##...#", "#..!..*.#", "#.#.?...#", "#...#!..#", "#.*...C.#", "#.......#", "#########" },
        new[] { "#########", "#@..#.*.#", "#.*.#...#", "#..!...?#", "##..#...#", "#...#!..#", "#.*...C.#", "#.......#", "#########" }
    };
    public static readonly string[] Titles = { "Le jardin oublie", "Les canaux perdus", "La source des nuages" };
    public static readonly string[] Journals = {
        "Carnet : l'eau et la lumiere aident une graine. La fumee ne la nourrit pas.",
        "Carnet : gauche = 1 litre ; milieu = deux fois gauche ; total = 6 litres.",
        "Carnet : la chaleur fait monter l'eau ; elle se condense, puis retombe."
    };
    public static Vector3 World(Vector2Int c) { return new Vector3(c.x-4,0,4-c.y); }
}
