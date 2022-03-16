using Mercury.Snapshot.Objects.Structures.Personalization;

namespace Mercury.Snapshot.Objects.Structures.Cards
{
    public interface ICard
    {
        IReadOnlyList<EmbedFieldBuilder> Render(MercuryProfile Profile);
    }
}
