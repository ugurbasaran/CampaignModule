namespace CampaignModule.Command
{
    public abstract class CommandQuery<T> : Command<T>, ICommandQuery where T : ICommandRequest
    {
        /// <summary>
        /// Executes command query
        /// </summary>
        /// <param name="commandQuery">Command query input</param>
        public void Execute(string commandQuery)
        {
            string[] splittedCommandQuery = commandQuery.Split(" ");
            if (CommandQueryIsValid(splittedCommandQuery))
            {
                T commandRequest = GetCommandRequestByQuery(splittedCommandQuery);
                ExecuteTemplate(commandRequest);
            }
        }

        /// <summary>
        /// Command query string should be validated
        /// </summary>
        /// <param name="splittedCommandQuery">Command query string input</param>
        /// <returns>Returns true if parameter command query is valid</returns>
        protected abstract bool CommandQueryIsValid(string[] splittedCommandQuery);
        
        /// <summary>
        /// Converts commandQuery to CommandRequest
        /// </summary>
        /// <param name="splittedCommandQuery">Command query string input</param>
        /// <returns>Returns Command Request</returns>
        protected abstract T GetCommandRequestByQuery(string[] splittedCommandQuery);

    }
}