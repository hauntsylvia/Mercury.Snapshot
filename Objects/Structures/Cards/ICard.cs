using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;

namespace Mercury.Snapshot.Objects.Structures.Cards
{
    public interface ICard
    {
        Task<IReadOnlyCollection<EmbedFieldBuilder>> RenderAsync(MercuryUser Profile);
    }
}
