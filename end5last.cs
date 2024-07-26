using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackOpening
{
    interface CardType
    {
        public int BoostStats(Defender def) { return 0; }
        public int BoostStats(Attacker att) { return 0; }
        public int BoostStats(Midfielder mid) { return 0; }
    }
    class None : CardType
    {
        private None() { }
        private static None instance = null;
        public static None Instance()
        {
            if (instance == null)
            {
                instance = new None();
            }
            return instance;
        }

        public int BoostStats(Defender def) { return 0; }
        public int BoostStats(Attacker att) { return 0; }
        public int BoostStats(Midfielder mid) { return 0; }
    }
    class POTW : CardType
    {
        private POTW() { }
        private static POTW instance = null;
        public static POTW Instance()
        {
            if (instance == null)
            {
                instance = new POTW();
            }
            return instance;
        }

        public int BoostStats(Defender def) { return 4; }
        public int BoostStats(Attacker att) { return 2; }
        public int BoostStats(Midfielder mid) { return 3; }
    }
    class POTY : CardType
    {
        private POTY() { }
        private static POTY instance = null;
        public static POTY Instance()
        {
            if (instance == null)
            {
                instance = new POTY();
            }
            return instance;
        }

        public int BoostStats(Defender def) { return 4; }
        public int BoostStats(Attacker att) { return 7; }
        public int BoostStats(Midfielder mid) { return 6; }

      
    }
    enum Nation
    {
        SPAIN,
        ENGLAND,
        BRASIL
    }
    class MyClub
    {
        private List<Card> cards = new List<Card>();
        public void JoinMyClub(Card card)
        {
            cards.Add(card);

        }
        public double AverageRating()
        {
            double s = 0;
            if (cards.Count == 0) { return 0; }
            foreach (Card c in cards)
            {
                s = s + c.Rating();
            }
            return (s / cards.Count);
        }
        public int EnglishPlayerCount()
        {
            int count = 0;
            foreach (Card c in cards)
            {
                if (c.GetPlayer().Nat() == Nation.ENGLAND) { count++; }

            }
            return count;
        }
        public List<string> AllEnglishPlayers()
        {
            List<string> names = new List<string>();
            foreach (Card c in cards)
            {
                if (c.GetPlayer().Nat() == Nation.ENGLAND)
                {
                    names.Add(c.GetPlayer().Name());
                }
            }
            return names;
        }
        public (bool, string) BestSpanishPlayer()
        {
            bool l = false;
            Card elem = null;
            int _ = 0;
            int max = 0;
            foreach (Card c in cards)
            {
                if (c.GetPlayer().Nat() == Nation.SPAIN)
                {
                    _ = c.Rating();
                }
                if (_ > max)
                {
                    l = true;
                    max = _;
                    elem = c;
                }
            }
            if (l)
            {
                return (l, elem.GetPlayer().Name());
            }
            else
            {
                return (l, "");
            }

        }
        public bool ExpensivePlayer()
        {
            bool l = false;
            foreach (Card c in cards)
            {
                if (c.Cost() > 1000000)
                {
                    l = true;
                    break;
                }
            }
            return l;
        }
        public (bool, int) MostExpensiveSpecialCost()
        {
            bool l = false;
            int _ = 0;
            int max = 0;
            foreach (Card c in cards)
            {
                if (c.Type() != None.Instance())
                {
                    _ = c.Cost();
                }
                if (_ > max)
                {
                    max = _;
                    l = true;
                }
            }
            return (l, max);

        }

    }
    class Card
    {
        protected Player player;
        protected int cost;
        protected int defense;
        protected int passes;
        protected int shots;
        protected CardType type;
        protected Card(int c, int d, int p, int s, Player pl, CardType t)
        {
            if (c < 0 || d < 0 || p < 0 || s < 0)
            {
                throw new Exception();
            }
            cost = c;
            defense = d;
            passes = p;
            shots = s;
            player = pl;
            type = t;

        }
        public CardType Type()
        {
            return type;
        }
        public virtual int Rating()
        {
            return 0;
        }
        public virtual string GetType()
        {
            return "";
        }
        public Player GetPlayer()
        {
            return player;
        }
        public int Cost()
        {
            return cost;
        }

    }
    class Midfielder : Card
    {
        public Midfielder(int c, int d, int p, int s, Player pl, CardType t) : base(c, d, p, s, pl, t)
        {
            int a = type.BoostStats(this);
            cost += a;
            passes += a;
        }

        public override int Rating()
        {
            return passes;
        }
        public override string GetType()
        {
            return "midfielder";
        }
    }
    class Attacker : Card
    {
        public Attacker(int c, int d, int p, int s, Player pl, CardType t) : base(c, d, p, s, pl, t)
        {
            int a = type.BoostStats(this);
            cost += a;
            shots += a;
        }

        public override int Rating()
        {
            return shots;
        }
        public override string GetType()
        {
            return "attacker";
        }

    }
    class Defender : Card
    {
        public Defender(int c, int d, int p, int s, Player pl, CardType t) : base(c, d, p, s, pl, t)
        {
            int a = type.BoostStats(this);
            cost += a;
            defense += a;
        }

        public override int Rating()
        {
            return defense;
        }
        public override string GetType()
        {
            return "defender";
        }
    }
    class Player
    {
        private Nation nationality;
        private string name;

        public Player(string n, Nation nat)
        {
            name = n;
            nationality = nat;
        }
        public string Name()
        {
            return name;
        }
        public Nation Nat()
        {
            return nationality;

        }

    }
}
