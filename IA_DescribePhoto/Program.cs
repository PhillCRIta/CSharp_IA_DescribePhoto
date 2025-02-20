using Azure;
using Azure.AI.Vision.ImageAnalysis;
using System.Drawing;

class Program
{
	static void Main(string[] args)
	{
		string endpoint = "https://xx.cognitiveservices.azure.com/";
		string key = "xx";

		ImageAnalysisClient client = new ImageAnalysisClient(
			new Uri(endpoint),
			new AzureKeyCredential(key));

		BinaryData imageBin = BinaryData.FromBytes(File.ReadAllBytes(@"tempin\tuscany.jpg"));

		ImageAnalysisResult result = client.Analyze(
			imageBin,
			VisualFeatures.DenseCaptions,
			new ImageAnalysisOptions { GenderNeutralCaption = false, Language = "en" });

		List<string> listCaptions = new List<string>();

		foreach (DenseCaption caption in result.DenseCaptions.Values)
		{
			listCaptions.Add(caption.Text);
		}

		File.WriteAllLines(@"tempout\out.txt", listCaptions);
	}
}