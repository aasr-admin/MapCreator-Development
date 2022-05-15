using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FFMpegCore;
using FFMpegCore.Enums;
using SixLabors.ImageSharp;
using Image = SixLabors.ImageSharp.Image;

namespace PlanetViewer
{
    public sealed class FfmpegFilmRecorder : IFilmRecorder
    {
        private readonly IList<Image> _frames;

        public FfmpegFilmRecorder()
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
            if (!_frames.Any())
            {
                return;
            }

            var tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDirectory);

            var fileNames = _frames.Select((frame, index) =>
            {
                var frameFilePath = Path.Combine(tempDirectory, index.ToString().PadLeft(9, '0') + ".png");
                frame.SaveAsPng(frameFilePath);
                return frameFilePath;
            }).ToArray();

            var compileVideoSuccess = FFMpegArguments
                .FromConcatInput(fileNames)
                .OutputToFile(filePath, true, options => options
                    .WithVideoCodec(VideoCodec.LibX265)
                    .WithConstantRateFactor(21)
                    .WithVideoFilters(filterOptions => filterOptions
                        .Scale(VideoSize.Hd))
                    .WithFastStart())
                .ProcessSynchronously();

            try
            {
                foreach (var fileName in fileNames)
                {
                    File.Delete(fileName);
                }
            }
            catch
            {
                // ignored
            }
        }
    }
}