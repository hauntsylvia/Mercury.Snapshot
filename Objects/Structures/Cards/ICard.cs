using Mercury.Snapshot.Objects.Structures.Personalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Structures.Cards
{
    public interface ICard
    {
        IReadOnlyList<EmbedFieldBuilder> Render(MercuryProfile Profile);
    }
}
