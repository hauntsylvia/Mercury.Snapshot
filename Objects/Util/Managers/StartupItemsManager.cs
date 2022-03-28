using Mercury.Snapshot.Objects.Structures.MercurySnapshot;
using Mercury.Unification.IO.File.Records;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Objects.Util.Managers
{
    public class StartupItemsManager
    {
        public StartupItemsManager(Register<StartupItems> RegisterReference, string Key)
        {
            this.RegisterReference = RegisterReference;
            this.Key = Key;
        }

        public Register<StartupItems> RegisterReference { get; }

        public string Key { get; }

        public StartupItems? GetStartupItems()
        {
            Record<StartupItems>? Record = this.RegisterReference.GetRecord(this.Key);
            return Record?.ObjectToStore;
        }

        public void SaveStartupItems(StartupItems StartupItemsToSave)
        {
            this.RegisterReference.SaveRecord(this.Key, new Record<StartupItems>(StartupItemsToSave));
        }
    }
}
