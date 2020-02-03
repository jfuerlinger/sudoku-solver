using SudokuSolver.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Drawing;

namespace SudokuSolver.Core
{
    public class OcrProcessor
    {
        private const string LANGUAGE = "de";
        private const string ORIENTATION = "up";

        private const string MODE = "Handwritten";

        // Add your Computer Vision subscription key and endpoint to your environment variables.
        static string subscriptionKey = Environment.GetEnvironmentVariable("COMPUTER_VISION_SUBSCRIPTION_KEY");

        static string endpoint = Environment.GetEnvironmentVariable("COMPUTER_VISION_ENDPOINT");

        // the OCR method endpoint
        static string uriBase = endpoint + "vision/v2.1/recognizeText";

        /// <summary>
        /// Gets the text visible in the specified image file by using
        /// the Computer Vision REST API.
        /// </summary>
        /// <param name="imageFilePath">The image file with printed text.</param>
        public static async Task<IEnumerable<RecognitionEntry>> ProcessImage(string imageFilePath)
        {
            try
            {
                HttpClient client = new HttpClient();

                // Request headers.
                client.DefaultRequestHeaders.Add(
                    "Ocp-Apim-Subscription-Key", subscriptionKey);

                // Request parameters. 
                // The language parameter doesn't specify a language, so the 
                // method detects it automatically.
                // The detectOrientation parameter is set to true, so the method detects and
                // and corrects text orientation before detecting text.
                //string requestParameters = $"language={LANGUAGE}&orientation={ORIENTATION}";// &detectOrientation=true";
                string requestParameters = $"mode={MODE}";// &detectOrientation=true";

                // Assemble the URI for the REST API method.
                string uri = uriBase + "?" + requestParameters;

                Console.WriteLine(uri.ToString());

                HttpResponseMessage response;

                // Read the contents of the specified local image
                // into a byte array.
                byte[] byteData = GetImageAsByteArray(imageFilePath);

                // Add the byte array as an octet stream to the request body.
                using (ByteArrayContent content = new ByteArrayContent(byteData))
                {
                    // This example uses the "application/octet-stream" content type.
                    // The other content types you can use are "application/json"
                    // and "multipart/form-data".
                    content.Headers.ContentType =
                        new MediaTypeHeaderValue("application/octet-stream");

                    // Asynchronously call the REST API method.
                    response = await client.PostAsync(uri, content);
                }

                string operationLocation = response.Headers.GetValues("Operation-Location").FirstOrDefault();

                Console.WriteLine($"OperationLocation='{operationLocation}'");

                Console.WriteLine("Waiting ...");
                await Task.Delay(1500);

                var operationResponse = await client.GetAsync(operationLocation);
                if (operationResponse.IsSuccessStatusCode)
                {
                    // Asynchronously get the JSON response.
                    string contentString = await operationResponse.Content.ReadAsStringAsync();

                    JToken root = JToken.Parse(contentString);
                    var recognizedTextFragments =
                        root
                            .SelectTokens("$..words")
                            .Select(token => token.FirstOrDefault())
                            .Select(token =>
                                new RecognitionEntry
                                {
                                    BoundingBox = new BoundingBox()
                                    {
                                        TopLeft = new Point(token["boundingBox"].ElementAt(0).Value<int>(), token["boundingBox"].ElementAt(1).Value<int>()),
                                        TopRight = new Point(token["boundingBox"].ElementAt(2).Value<int>(), token["boundingBox"].ElementAt(3).Value<int>()),
                                        BottomRight = new Point(token["boundingBox"].ElementAt(4).Value<int>(), token["boundingBox"].ElementAt(5).Value<int>()),
                                        BottumLeft = new Point(token["boundingBox"].ElementAt(6).Value<int>(), token["boundingBox"].ElementAt(7).Value<int>()),
                                    },
                                    Text = token["text"].Value<string>()
                                });

                    return recognizedTextFragments;

                    // Display the JSON response.
                    //Console.WriteLine("\nResponse:\n\n{0}\n",
                    //    JToken.Parse(contentString).ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("\n" + e.Message);
            }

            return null;
        }

        /// <summary>
        /// Returns the contents of the specified file as a byte array.
        /// </summary>
        /// <param name="imageFilePath">The image file to read.</param>
        /// <returns>The byte array of the image data.</returns>
        static byte[] GetImageAsByteArray(string imageFilePath)
        {
            // Open a read-only file stream for the specified file.
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                // Read the file's contents into a byte array.
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }
    }
}
