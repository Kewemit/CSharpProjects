//using System.Diagnostics.Tracing;
using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
using System.Text.Json;
//using System.Text.Json.Nodes;
//using System.Text.Json.Serialization.Metadata;
//using System.Threading.Tasks.Dataflow;
//using JetBrains.Annotations;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using static System.Net.WebRequestMethods;

class GithubUserActivity
{

    public static async Task Main()
    {
        bool ReRun = true;
        while (ReRun)
        {
            Console.Write("What user's activity do you want to see? "); // Asks the user for who they would like to look up
            string username = Console.ReadLine(); // Awaits for user input
            Console.WriteLine(); // Write new empty line for easier reading

            var client = new HttpClient(); // Reference httpclient as client
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0"); // Add a header
            var targetUrl = new Uri($"https://api.github.com/users/{username}/events"); // The url for the users activity
            var results = client.GetAsync(targetUrl).Result; // gets the result from the site and stores it in a variable
            var resultsJson = await results.Content.ReadAsStringAsync(); // Gets the json data and turns it into a string; await waits for the ReadAsString to load
            var newValue = JsonDocument.Parse(resultsJson); // Converts the Json string into a document for easier access
            if (results.StatusCode == HttpStatusCode.NotFound)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No user found");
                Console.WriteLine();
                Console.ResetColor();
            }
            else if (results.StatusCode == HttpStatusCode.Forbidden)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Forbidden");
                Console.WriteLine();
                Console.ResetColor();
            }
            else if (results.StatusCode == HttpStatusCode.OK)
            {
                JsonElement JsonRoot = newValue.RootElement; // used to get the root element of the json so you can use methods like GetProperty()
                if (JsonRoot.ToString() == "[]")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No Recent activity");
                    Console.ResetColor();
                }
                if (JsonRoot.ValueKind == JsonValueKind.Array) // Checks if the root is an array with ValueKind
                {
                    foreach (JsonElement eventItem in JsonRoot.EnumerateArray()) // For each individual item in the array iterate elements. This is especially helpful when processing collections of data, such as a list of GitHub events 
                    {
                        string type = eventItem.GetProperty("type").ToString(); // gets the property of string
                        string repo = eventItem.GetProperty("repo").GetProperty("name").ToString(); // gets the name property from within repo
                        string message = ""; // makes the string message empty and accessible so you can add values like i did below
                        switch (type) // get types from the variable and then do a case with each
                        {
                            case "PushEvent": // if the type is PushEvent then do this
                                int commitAmount = eventItem.GetProperty("payload").GetProperty("size").GetInt32();
                                message = $"Pushed {commitAmount} to: {repo}"; // Adds this string to the message i made earlier
                                break;
                            case "PullRequestEvent": // if the case is PullRequestEvent then do this
                                message = $"Called for a PullRequest in: {repo}";
                                break;
                            case "WatchEvent":
                                message = $"Starred: {repo}";
                                break;
                            case "IssueCommentEvent":
                                message = $"Opened a new issue in: {repo}";
                                break;
                            default:
                                message = $"Performed {type} in: {repo}";
                                break;
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(message); // This will output the new message
                        Console.ResetColor();
                    }
                }


                Console.WriteLine();
                Console.WriteLine("Do you want to run this again? (Y/N)");
                string userInput = Console.ReadLine();
                userInput = userInput.ToUpper();


                if (userInput == "Y") ReRun = true;
                else ReRun = false;
            }
        }
        Console.WriteLine("Thanks!");
        Console.WriteLine("Press enter twice to exit!");

    }
}
