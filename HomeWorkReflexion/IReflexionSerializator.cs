namespace HomeWorkReflexion
{
    internal interface IReflexionSerializator
    {
        T Deserialize<T>(string csv, char delimeter);
        string Serialize<T>(T obj);
    }
}