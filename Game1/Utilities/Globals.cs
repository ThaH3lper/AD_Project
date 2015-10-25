using Microsoft.Xna.Framework.Graphics;
using Patrik.GameProject;
using Microsoft.Xna.Framework;
using System.IO;
using System;
using Game1.Datastructures.ADT;
using Patrik.GameProject.Datastructures.Implementations;
using Game1.Datastructures.Implementations;
using Microsoft.Xna.Framework.Input;

class Globals
{
    public static int GAME_WIDTH = 1280, GAME_HEIGHT = 720;
    public static string TITLE = "Applied Data Structures and Algorithms Project";

    public static Texture2D player, gun, dot, create, bullet, aim;
    public static Map map;

    public static IMap<char, TileData> tileTable = new Hashtable<char, TileData>(4);

    public static IMap<Player.Commands, IList<Keys>> inputTable = new Hashtable<Player.Commands, IList<Keys>>();

    public static SpriteFont font;

    public static void LoadContent(MainGame game)
    {
        // Load assets
        player = game.Content.Load<Texture2D>("player");
        gun = game.Content.Load<Texture2D>("gun");
        dot = game.Content.Load<Texture2D>("dot");
        create = game.Content.Load<Texture2D>("create");
        bullet = game.Content.Load<Texture2D>("bullet");
        aim = game.Content.Load<Texture2D>("aim");
        font = game.Content.Load<SpriteFont>("spriteFont");

        // Init tiles 
        tileTable.Put('O', new TileData(ETileType.FLOOR, dot, Color.DarkGray));
        tileTable.Put('W', new TileData(ETileType.WALL, dot, Color.Black));
        tileTable.Put('S', new TileData(ETileType.SPAWN, dot, Color.Red));
        tileTable.Put('C', new TileData(ETileType.CRATE, create, Color.Brown));


        // Init key bindings
        inputTable.Put(Player.Commands.MoveUp, new LinkedList<Keys>() { Keys.Up, Keys.W });
        inputTable.Put(Player.Commands.MoveDown, new LinkedList<Keys>() { Keys.Down, Keys.S });
        inputTable.Put(Player.Commands.MoveLeft, new LinkedList<Keys>() { Keys.Left, Keys.A });
        inputTable.Put(Player.Commands.MoveRight, new LinkedList<Keys>() { Keys.Right, Keys.D });


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
        int y = height-1;
        foreach (string s in strings)
        {
            foreach (char c in s)
            {
                charMap[x, y] = c;
                x++;
            }
            y--;
            x = 0;
        }
        map = new Map(width, height, charMap);
    }
}
