using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Resources;

namespace WpfTest.Utils {
    internal class ImageProcessor {
        private string? leftSnakeGifPath = "";
        private string? rightSnakeGifPath = "";
        private string? leftUpSnakeGifPath = "";
        private string? rightUpSnakeGifPath = "";
        private string? leftDownSnakeGifPath = "";
        private string? rightDownSnakeGifPath = "";
        private string? fruitPNGPath = "";
        private string? snakeSegemnt1PNGPath = "";
        private string? snakeSegemnt2PNGPath = "";
        private string? snakeSegemnt3PNGPath = "";
        public Dictionary<string, string> SnakeGifPaths { get; private set; }
        public Dictionary<string, string> SnakeSegmentPNGPaths { get; private set; }
        public string? fruitPNGFile {get; private set;}

        public ImageProcessor() {

            Json_Reader();

            SnakeGifPaths = new Dictionary<string, string> {
                { "left", leftSnakeGifPath },
                { "right", rightSnakeGifPath },
                { "leftUp", leftUpSnakeGifPath },
                { "rightUp", rightUpSnakeGifPath },
                { "leftDown", leftDownSnakeGifPath },
                { "rightDown", rightDownSnakeGifPath }
            };

            SnakeSegmentPNGPaths = new Dictionary<string, string> {
                { "snakeSegment_1", snakeSegemnt1PNGPath },
                { "snakeSegment_2", snakeSegemnt2PNGPath },
                { "snakeSegment_3", snakeSegemnt3PNGPath },
            };

            fruitPNGFile = fruitPNGPath; 
        }

        private void Json_Reader() {
            Uri uri = new Uri("pack://application:,,,/WpfTest;component/Utils/uri.json");
            StreamResourceInfo resourceInfo = Application.GetResourceStream(uri);

            if (resourceInfo != null) {
                using (StreamReader reader = new StreamReader(resourceInfo.Stream)) {
                    string jsonContent = reader.ReadToEnd();
                    JObject jsonObject = JObject.Parse(jsonContent);

                    leftSnakeGifPath = jsonObject["snakeHead"]?["snakeHead_LEFT"]?.ToString();
                    rightSnakeGifPath = jsonObject["snakeHead"]?["snakeHead_RIGHT"]?.ToString();
                    leftUpSnakeGifPath = jsonObject["snakeHead"]?["snakeHead_LEFT_UP"]?.ToString();
                    rightUpSnakeGifPath = jsonObject["snakeHead"]?["snakeHead_RIGHT_UP"]?.ToString();
                    leftDownSnakeGifPath = jsonObject["snakeHead"]?["snakeHead_LEFT_DOWN"]?.ToString();
                    rightDownSnakeGifPath = jsonObject["snakeHead"]?["snakeHead_RIGHT_DOWN"]?.ToString();

                    snakeSegemnt1PNGPath = jsonObject["snakeSegments"]?["snakeSegment_1"]?.ToString();
                    snakeSegemnt2PNGPath = jsonObject["snakeSegments"]?["snakeSegment_2"]?.ToString();
                    snakeSegemnt3PNGPath = jsonObject["snakeSegments"]?["snakeSegment_3"]?.ToString();

                    fruitPNGPath = jsonObject["fruit"]?.ToString();
                }
            }
            else {
                Console.WriteLine("Resource not found.");
            }
        }
    }
}

