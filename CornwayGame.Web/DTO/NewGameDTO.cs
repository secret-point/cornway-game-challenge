namespace CornwayGame.Web.DTO
{
    public class NewGameDTO
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public required int[][] LiveCellCoordinates { get; set; }
    }
}
