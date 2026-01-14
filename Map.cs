using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGameWPF.Models
{
	public class Map
	{
		public int Width { get; }
		public int Height { get; }
		private Tile[,] Tiles { get; }
		public Position ExitPos { get; private set; }
		public Map(int width, int height)
		{
			Width = width;
			Height = height;
			Tiles = new Tile[width, height];
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					Tiles[x, y] = new Tile(TileType.Floor);
				}
			}
		}
		public bool InBounds(Position pos)
		{
			return pos.X >= 0 && pos.X < Width && pos.Y >= 0 && pos.Y < Height;
		}
		public bool IsWalkable(Position pos)
		{
			if (!InBounds(pos))
				return false;
			var tile = Tiles[pos.X, pos.Y];
			return tile.Type != TileType.Wall;
		}
		public void GenerateRandom(int seed)
		{
			var rand = new Random(seed);
			// Place walls randomly
			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					Tiles[x, y].Type = TileType.Floor;
				}
			}
			for (int x = 0; x < Width; x++)
			{
				Tiles[x, 0].Type = TileType.Wall;
				Tiles[x, Height - 1].Type = TileType.Wall;
			}
			for (int y = 0; y < Height; y++)
			{
				Tiles[0, y].Type = TileType.Wall;
				Tiles[Width - 1, y].Type = TileType.Wall;
			}
			int wallCount = (Width * Height) / 7;
			for (int i = 0; i < wallCount; i++)
			{
				int wallX = rand.Next(1, Width - 1);
				int wallY = rand.Next(1, Height - 1);
				if ((wallX==1&&wallY==1)||(wallX==Width-2 && wallY==Height-2))
				{
					continue;
				}
				Tiles[wallX, wallY].Type = TileType.Wall;
			}
			ExitPos = new Position(Width - 2, Height - 2);
			Tiles[ExitPos.X, ExitPos.Y].Type = TileType.Exit;
			Tiles[1, 1].Type = TileType.Floor;
		}
	}
}
