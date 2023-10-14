// See https://aka.ms/new-console-template for more information
using HomeWorkReflexion;
using Newtonsoft.Json;
using System.Diagnostics;
using System;
var n = 10000;

Console.WriteLine("Hello, World!");
var serializableModel = new SerializableModel();
var testModel = serializableModel.Get();
var timer = new Stopwatch();
var serializationTimes = new List<TimeSpan>();
var deSerializationTimes = new List<TimeSpan>();
Console.WriteLine("Start Calculation Newtonsoft.Json Methods");

for (int i = 0; i < n; i++)
{
    timer.Start();
    var str = JsonConvert.SerializeObject(testModel);
    timer.Stop();
    var resultTime = timer.Elapsed;

    serializationTimes.Add(resultTime);
    timer.Reset();

    if (i == n - 1)
    {
        Console.WriteLine("last serialization result: {0}", str);
    }

    /* */

    timer.Start();
    var obj = JsonConvert.DeserializeObject<SerializableModel>(str);
    timer.Stop();
    var resultTime2 = timer.Elapsed;
    deSerializationTimes.Add(resultTime2);
    timer.Reset();

}

var avgSerializeTime = serializationTimes.Average(t => t.Ticks);
var avgDeserializeTime = deSerializationTimes.Average(t => t.Ticks);

Console.WriteLine("End of Calculation");
var averageSerializeTimeSpan = new TimeSpan(Convert.ToInt64(avgSerializeTime));
var averageDeSerializeTimeSpan = new TimeSpan(Convert.ToInt64(avgDeserializeTime));

var elapsedTimeS = string.Format("{0:00}:{1:00}:{2:00}.{3:000}:{4:00}",
        averageSerializeTimeSpan.Hours,
        averageSerializeTimeSpan.Minutes,
        averageSerializeTimeSpan.Seconds,
        averageSerializeTimeSpan.Milliseconds,
        averageSerializeTimeSpan.Nanoseconds);
var elapsedTimeD = string.Format("{0:00}:{1:00}:{2:00}.{3:000}:{4:00}",
        averageDeSerializeTimeSpan.Hours,
        averageDeSerializeTimeSpan.Minutes,
        averageDeSerializeTimeSpan.Seconds,
        averageDeSerializeTimeSpan.Milliseconds,
        averageDeSerializeTimeSpan.Nanoseconds);
Console.WriteLine("NewTonJsonSoft SerializeObject avg time: {0}", averageSerializeTimeSpan);
Console.WriteLine("NewTonJsonSoft DeSerializeObject avg time: {0}", averageDeSerializeTimeSpan);

/* -- */
serializationTimes = new List<TimeSpan>();
deSerializationTimes = new List<TimeSpan>();
Console.WriteLine("Start Calculation ReflexionSerializator");

for (int i = 0; i < n; i++)
{
    timer.Start();
    var serializator = new ReflexionSerializator();
    var str = serializator.Serialize(testModel);
    timer.Stop();
    var resultTime = timer.Elapsed;

    serializationTimes.Add(resultTime);
    timer.Reset();

    if (i == n - 1)
    {
        Console.WriteLine("last serialization result: {0}", str);
    }
    /* */

    timer.Start();
    var strCsv = "5,4,3,2,1";
    var obj = serializator.Deserialize<SerializableModel>(strCsv, ',');
    timer.Stop();
    var resultTime2 = timer.Elapsed;
    deSerializationTimes.Add(resultTime2);
    timer.Reset();

}

avgSerializeTime = serializationTimes.Average(t => t.Ticks);
//var avgDeserializeTime = deSerializationTimes.Average(t => t.Ticks);

Console.WriteLine("End of Calculation");
averageSerializeTimeSpan = new TimeSpan(Convert.ToInt64(avgSerializeTime));
//var averageDeSerializeTimeSpan = new TimeSpan(Convert.ToInt64(avgDeserializeTime));

elapsedTimeS = string.Format("{0:00}:{1:00}:{2:00}.{3:000}:{4:00}",
        averageSerializeTimeSpan.Hours,
        averageSerializeTimeSpan.Minutes,
        averageSerializeTimeSpan.Seconds,
        averageSerializeTimeSpan.Milliseconds,
        averageSerializeTimeSpan.Nanoseconds);
elapsedTimeD = string.Format("{0:" +
    "00}:{1:00}:{2:00}.{3:000}:{4:00}",
        averageDeSerializeTimeSpan.Hours,
        averageDeSerializeTimeSpan.Minutes,
        averageDeSerializeTimeSpan.Seconds,
        averageDeSerializeTimeSpan.Milliseconds,
        averageDeSerializeTimeSpan.Nanoseconds);
Console.WriteLine("ReflexionSerializator SerializeObject avg time: {0}", averageSerializeTimeSpan);
Console.WriteLine("ReflexionSerializator DeSerializeObject avg time: {0}", averageDeSerializeTimeSpan);