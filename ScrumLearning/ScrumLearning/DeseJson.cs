namespace ScrumLearning
{
    internal class DeseJson
    {
        //[JsonProperty(PropertyName = "Current")]
        public Current current { get; set; }
    }

    internal class Current
    {
        public int temperature { get; set; }
    }
}