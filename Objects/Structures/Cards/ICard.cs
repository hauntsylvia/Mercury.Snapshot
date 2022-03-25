using Mercury.Snapshot.Objects.Structures.UserStructures.Personalization;

namespace Mercury.Snapshot.Objects.Structures.Cards
{
    internal interface ICard
    {
        Task<IReadOnlyList<EmbedFieldBuilder>> RenderAsync(MercuryUser Profile);
    }
}
