namespace PlanetViewer
{
    public interface IFilmRecorder
    {
        void Reset();

        void AddFrame(SixLabors.ImageSharp.Image frameImage);

        void RemoveFrame();

        void SaveAs(string filePath);
    }
}