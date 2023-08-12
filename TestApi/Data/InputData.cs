using Newtonsoft.Json;
using System.IO;
using System.Net.Http.Json;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using TestApi.Models;
using TestApi.Services.IServices;

namespace TestApi.Data;

public class InputData
{
    public string jsonString;

    public InputData()
    {
        ReadJsonFile();
    }

    

    public List<Reservation> ReadJsonFile()
    {

        jsonString = File.ReadAllText(@"D:\New folder\reservation - Copy.json");
        var reservation = System.Text.Json.JsonSerializer.Deserialize<List<Reservation>>(jsonString);
        
        return reservation;
    }
}












