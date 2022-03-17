using Mercury.Snapshot.Objects.Structures.Cards;
using Mercury.Snapshot.Objects.Structures.Personalization;

namespace Mercury.Snapshot.Objects.Util.Discord.EmbedFieldBuilders
{
    public class CardsBuilder
    {
        public CardsBuilder(List<ICard> Cards, MercuryProfile Profile)
        {
            List<List<EmbedFieldBuilder>> CardsBuilder = new();
            foreach (ICard Card in Cards)
            {
                IReadOnlyList<EmbedFieldBuilder> ThisSet = Card.Render(Profile);
                CardsBuilder.Add(ThisSet.ToList());
            }
            this.Cards = CardsBuilder;
        }

        public List<List<EmbedFieldBuilder>> Cards { get; }

        public List<EmbedFieldBuilder> Build()
        {
            List<EmbedFieldBuilder> Finished = new();
            foreach (List<EmbedFieldBuilder> Set in this.Cards)
            {
                if (Set.Count > 0)
                {
                    if (Set != this.Cards.Last())
                    {
                        EmbedFieldBuilder Last = Set.Last();
                        string? TrimmedValue = Last.Value.ToString()?.Trim(new[] { '\u200b', ' ', '\n' });
                        string? TrimmedName = Last.Name.ToString()?.Trim(new[] { '\u200b', ' ', '\n' });
                        Last.Value = TrimmedValue ?? Last.Value;
                        Last.Name = TrimmedName ?? Last.Name;
                        Last.Value += "\u200b\n────────⊹⊱-☿-⊰⊹────────";
                    }
                    Finished.AddRange(Set);
                }
            }

            return Finished;
        }
    }
}
