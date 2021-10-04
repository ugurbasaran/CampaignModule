namespace CampaignModule.Command
{
    public interface ICommand
    {
        void Execute(ICommandRequest commandRequest);
    }
}