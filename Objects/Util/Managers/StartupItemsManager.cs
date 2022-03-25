using Mercury.Snapshot.Objects.Structures.MercurySnapshot;
using Mercury.Unification.IO.File.Records;
using Mercury.Unification.IO.File.Registers;

namespace Mercury.Snapshot.Objects.Util.Managers
{
    internal class StartupItemsManager
    {
        internal StartupItemsManager(Register<StartupItems> RegisterReference, string Key)
        {
            this.RegisterReference = RegisterReference;
            this.Key = Key;
        }

        internal Register<StartupItems> RegisterReference { get; }

        internal string Key { get; }

        internal StartupItems? GetStartupItems()
        {
            Record<StartupItems>? Record = this.RegisterReference.GetRecord(this.Key);
            return Record?.ObjectToStore;
        }

        internal void SaveStartupItems(StartupItems StartupItemsToSave)
        {
            this.RegisterReference.SaveRecord(this.Key, new Record<StartupItems>(StartupItemsToSave));
        }
    }
}
