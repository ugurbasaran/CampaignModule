namespace CampaignModule.Command
{
    public class CommandExecutor
    {
        public void Execute(string commandQuery)
        {
            CommandQueryFinder commandQueryFinder = new CommandQueryFinder();
            ICommandQuery command = commandQueryFinder.FindRelevantCommandQuery(commandQuery);
            command.Execute(commandQuery);
        }
    }
}