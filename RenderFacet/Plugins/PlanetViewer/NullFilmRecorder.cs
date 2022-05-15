using Image = SixLabors.ImageSharp.Image;

namespace PlanetViewer
{
    public sealed class NullFilmRecorder : IFilmRecorder
    {
        public void Reset()
        {
        }

        public void AddFrame(Image frameImage)
        {
        }

        public void RemoveFrame()
        {
        }

        public void SaveAs(string filePath)
        {
        }
    }
}