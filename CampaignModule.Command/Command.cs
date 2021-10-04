namespace CampaignModule.Command
{
    public abstract class Command<T> : ICommand where T : ICommandRequest
    {
        public void Execute(ICommandRequest commandRequest)
        {
            ExecuteTemplate((T)commandRequest);
        }

        protected abstract void ExecuteTemplate(T commandRequest);
    }
}