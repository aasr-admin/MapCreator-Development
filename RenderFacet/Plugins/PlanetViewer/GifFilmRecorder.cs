using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.PixelFormats;
using Color = SixLabors.ImageSharp.Color;
using Image = SixLabors.ImageSharp.Image;

namespace PlanetViewer
{
    public sealed class GifFilmRecorder : IFilmRecorder
    {
        private readonly IList<Image> _frames;

        public GifFilmRecorder()
        {
            _frames = new List<Image>(8);
        }

        public void Reset()
        {
            foreach (var frame in _frames)
            {
                frame.Dispose();
            }

            _frames.Clear();
        }

        public void AddFrame(Image frameImage)
        {
            _frames.Add(frameImage);
        }

        public void RemoveFrame()
        {
            if (_frames.Count > 1)
            {
                var frame = _frames.Last();
                _frames.Remove(frame);
                frame.Dispose();
            }
        }

        public void SaveAs(string filePath)
        {
            if (_frames.Count == 0)
            {
                return;
            }

            var firstFrame = _frames.First();
            using var gifImage = new Image<Rgba32>(firstFrame.Width, firstFrame.Height, Color.Transparent);
            foreach (var frame in _frames)
            {
                var frameMetadata = frame.Frames.RootFrame.Metadata.GetGifMetadata();
                frameMetadata.FrameDelay = 30;

                gifImage.Frames.AddFrame(frame.Frames.RootFrame);
            }

            var gifMetadata = gifImage.Metadata.GetGifMetadata();
            gifMetadata.Comments.Add("Made With Aasr-Sva's Planetoidor2000");
            gifMetadata.RepeatCount = 20;

            var gifEncoder = new GifEncoder();
            gifEncoder.Quantizer.Options.MaxColors = 64;
            gifImage.SaveAsGif(filePath, gifEncoder);
        }
    }
}