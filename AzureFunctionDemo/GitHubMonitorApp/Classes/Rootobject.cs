namespace GitHubMonitorApp.Classes
{
    public class Rootobject
    {
        public string zen { get; set; }
        public int hook_id { get; set; }
        public Hook hook { get; set; }
        public Repository repository { get; set; }
        public Sender sender { get; set; }
    }
}
