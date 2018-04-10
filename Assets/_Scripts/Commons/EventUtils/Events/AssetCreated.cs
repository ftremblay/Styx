namespace Assets.Commons.EventUtils.Events
{
    public class AssetCreated
    {
        public string AssetPath { get; private set; }

        public static AssetCreated Create(string path)
        {
            return new AssetCreated(path);
        }

        private AssetCreated(string path)
        {
            AssetPath = path;
        }
    }
}
