using Microsoft.Xna.Framework.Graphics;
using Patrik.GameProject;
using Microsoft.Xna.Framework;
using System.IO;
using System;
using Game1.Datastructures.ADT;
using Patrik.GameProject.Datastructures.Implementations;
using Game1.Datastructures.Implementations;

class Globals
{
    public static int GAME_WIDTH = 1280, GAME_HEIGHT = 720;
    public static string TITLE = "Applied Data Structures and Algorithms Project";

    public static Texture2D player, gun, dot, create, bullet;
    public static Map map;

    public static IMap<char, TileData> tileTable = new Hashtable<char, TileData>(4);

    public static void LoadContent(MainGame game)
    {
        player = game.Content.Load<Texture2D>("player");
        gun = game.Content.Load<Texture2D>("gun");
        dot = game.Content.Load<Texture2D>("dot");
        create = game.Content.Load<Texture2D>("create");
        bullet = game.Content.Load<Texture2D>("bullet");

        tileTable.Put('O', new TileData(ETileType.FLOOR, dot, Color.LightGray));
        tileTable.Put('W', new TileData(ETileType.WALL, dot, Color.Black));
        tileTable.Put('S', new TileData(ETileType.SPAWN, dot, Color.Red));
        tileTable.Put('C', new TileData(ETileType.CREATE, create, Color.Brown));

        loadMap(game);
    }

    private static void loadMap(MainGame game)
    {
        IList<string> strings = new LinkedList<string>();
        StreamReader sr = new StreamReader("Content/level.map");
        while (!sr.EndOfStream)
            strings.Add(sr.ReadLine());
        sr.Close();
        int width = strings[0].Length;
        int height = strings.Count;

        Console.WriteLine(width + " " + height);

        char[,] charMap = new char[width, height];
        int x = 0;
        int y = 0;
        foreach (string s in strings)
        {
            foreach (char c in s)
            {
                charMap[x, y] = c;
                x++;
            }
            y++;
            x = 0;
        }
        map = new Map(width, height, charMap);
    }
}
