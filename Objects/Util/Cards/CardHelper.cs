using Mercury.Snapshot.Objects.Structures.Cards;
using Mercury.Snapshot.Objects.Structures.Personalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Snapshot.Objects.Util.Cards
{
    public class CardHelper
    {
        public CardHelper(List<ICard> Cards, MercuryProfile Profile)
        {
            List<List<EmbedFieldBuilder>> CardsBuilder = new();
            foreach(ICard Card in Cards)
            {
                IReadOnlyList<EmbedFieldBuilder> ThisSet = Card.Render(Profile);
                CardsBuilder.Add(ThisSet.ToList());
            }
            this.cards = CardsBuilder;
        }

        private readonly List<List<EmbedFieldBuilder>> cards;
        public List<List<EmbedFieldBuilder>> Cards => this.cards;


        public List<EmbedFieldBuilder> CorrectWhitespacing()
        {
            List<EmbedFieldBuilder> Finished = new();
            foreach(List<EmbedFieldBuilder> Set in this.Cards)
            {
                if(Set != this.Cards.Last())
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
            return Finished;
        }
    }
}
