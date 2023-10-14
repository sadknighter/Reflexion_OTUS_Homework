namespace HomeWorkReflexion
{
    public class SerializableModel
    {
        public int i1, i2, i3, i4, i5;

        public SerializableModel Get() => new SerializableModel()
        {
            i1 = 1,
            i2 = 2,
            i3 = 3,
            i4 = 4,
            i5 = 5
        };
    }
}
