using Mercury.Snapshot.Objects.Structures.MercurySnapshot;
using Mercury.Unification.IO.File.Records;
using Mercury.Unification.IO.File.Registers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Util.Managers
{
    public class StartupItemsManager
    {
        public StartupItemsManager(IRegister<StartupItems> RegisterReference, string Key)
        {
            this.RegisterReference = RegisterReference;
            this.Key = Key;
        }

        public IRegister<StartupItems> RegisterReference { get; }

        public string Key { get; }

        public StartupItems? GetStartupItems()
        {
            IRecord<StartupItems>? Record = this.RegisterReference.GetRecord(this.Key);
            return Record?.ObjectToStore;
        }

        public void SaveStartupItems(StartupItems StartupItemsToSave)
        {
            this.RegisterReference.SaveRecord(this.Key, new Record<StartupItems>(StartupItemsToSave));
        }
    }
}
