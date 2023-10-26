using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>();
            {
                commands.Add(new Command { Id = 1, HowTo = "Boil an egg", Line = "Boli water", Platform = "Kettle&Pan" });
                commands.Add(new Command { Id = 2, HowTo = "Cut bread", Line = "Get a knife", Platform = "Knife&Chopping board" });
                commands.Add(new Command { Id = 3, HowTo = "Make cup of tea", Line = "Boli water", Platform = "Kettle&Cup" });
            }
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Boil an egg", Line = "Boli water", Platform = "Kettle&Pan" };
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            throw new NotImplementedException();
        }
    }
}
