

namespace Warpath_frontend.AppState.Models;



public class MapGame
{
    public List<TileMap> allTiles;

    public MapGame()
    {
        allTiles = new List<TileMap>();
    }
}




public class TileMap
{
    public int? index;

    public TileMap()
    {
        index = null;
    }
}