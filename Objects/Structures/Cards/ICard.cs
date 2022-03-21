using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;

namespace Mercury.Snapshot.Objects.Structures.Cards
{
    public interface ICard
    {
        IReadOnlyList<EmbedFieldBuilder> Render(MercuryUser Profile);
    }
}
