using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        // Replace with your URL
        string apiUrl = "http://ergast.com/api/f1/2023/constructors.json";

        List<string> constructorIds = await GetConstructorIds(apiUrl);
        foreach (string constructorId in constructorIds)
        {
            string CircuitURL = $"https://ergast.com/api/f1/constructors/{constructorId}/results/1.json";
            List<string> circuitNames = await ManageCircuitID(CircuitURL);

            Console.WriteLine($"Constructor: {constructorId}");
            foreach (string circuitName in circuitNames)
            {
                Console.WriteLine($"Circuit Name: {circuitName}");
            }
            Console.WriteLine();
        }
    }

    static async Task<List<string>> GetConstructorIds(string apiUrl)
    {
        List<string> constructorIds = new List<string>();

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject data = JsonConvert.DeserializeObject<JObject>(json);

                    JArray constructors = data["MRData"]["ConstructorTable"]["Constructors"] as JArray;
                    if (constructors != null)
                    {
                        foreach (var constructor in constructors)
                        {
                            string constructorId = constructor["constructorId"]?.ToString();
                            if (!string.IsNullOrEmpty(constructorId))
                            {
                                constructorIds.Add(constructorId);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error: Unable to fetch data from the API.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        return constructorIds;
    }

    static async Task<List<string>> ManageCircuitID(string apiUrl)
    {
        List<string> CircuitNames = new List<string>();

        using (HttpClient client1 = new HttpClient())
        {
            try
            {
                HttpResponseMessage response1 = await client1.GetAsync(apiUrl);

                if (response1.IsSuccessStatusCode)
                {
                    string json = await response1.Content.ReadAsStringAsync();
                    JObject data = JsonConvert.DeserializeObject<JObject>(json);

                    JArray Races = data["MRData"]["RaceTable"]["Races"] as JArray;
                    if (Races != null)
                    {
                        foreach (var race in Races)
                        {
                            string CircuitName = race["Circuit"]["circuitName"]?.ToString();
                            if (!string.IsNullOrEmpty(CircuitName))
                            {
                                CircuitNames.Add(CircuitName);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Error: Unable to fetch data from the API.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        return CircuitNames;
    }
}
