using System;
using System.Numerics;

namespace JeuDeCombat
{
    internal class Program
    {
        public abstract class Entity
        {
            public int Health;
            public int Damage;
            public bool Defend;

            public static Entity CreateEntity(int i)
            {
                if(i == 1) 
                {
                    return new Tank();
                }
                else if(i == 2)
                {
                    return new Healer();
                }
                else
                {
                    return new Damager();
                }
            }
            
            public virtual void TakeDamage(int damage, int penetration = 0, Entity attacker = null)
            {
                
                if (Defend /*même chose que si Defend == true*/) 
                {
                    Health-= penetration;

                    /*Damage -= 2; première version du code avant optimisation
                    if(Damage <= 0)
                    {
                        Damage = 0;
                    }*/
                    //Damage = Math.Clamp(Damage - 2, 0, Damage); autre manière de l'écrire
                }
                else
                {
                    Health -= damage;
                }
            }
            public void DealDamage(Entity target)
            {
                target.TakeDamage(Damage);
            }

            public abstract void Spell(Entity target);

            public virtual void StartTurn() 
            {
                Defend = false;
            }
        }

        public class Tank : Entity
        {
            public Tank()
            {
                Health = 5;
                Damage = 1;
            }
            public override void Spell (Entity target)
            {
                target.TakeDamage(2, 1);
                TakeDamage(1);
            }
        }

        public class Healer : Entity
        {
            public Healer()
            {
                Health = 4;
                Damage = 1;
            }
            public override void Spell(Entity target)
            {
                Health = Math.Clamp(Health + 2 , 0, Health);
            }
        }

        public class Damager : Entity
        {
            bool SpellActivated;
            public Damager()
            {
                Health = 3;
                Damage = 2;
            }
            public override void Spell (Entity attacker)
            {
                SpellActivated = true;
            }
            public override void TakeDamage(int damage, int penetration = 0, Entity attacker = null)
            {
                base.TakeDamage(damage, penetration, attacker);
                if (SpellActivated)
                {
                    attacker.TakeDamage(damage);
                }
            }
            public override void StartTurn()
            {
                base.StartTurn();
                SpellActivated = false;
            }
        }

        public class Randomer : Entity
        {
            bool SpellActivated;
            public Randomer()
            {
                Random rnd = new Random();
                rnd.Next();

                Health = rnd.Next(1, 6); 
                Damage = rnd.Next(1, 3);
            }

            public override void Spell(Entity target)
            {
                Random rSpell = new Random();
                int spellrnd = rSpell.Next(1, 4); 
                if(spellrnd == 1)
                {
                    target.TakeDamage(2, 1);
                    TakeDamage(1);
                }
                else if(spellrnd == 2)
                {
                    Health = Math.Clamp(Health + 2, 0, Health);
                }
                else
                {
                    SpellActivated = true;
                }
            }
            public override void TakeDamage(int damage, int penetration = 0, Entity attacker = null)
            {
                base.TakeDamage(damage, penetration, attacker);
                if (SpellActivated)
                {
                    attacker.TakeDamage(damage);
                }
            }
        }
            
        public class Bebere : Entity
        {
            List<string> Chaos;
            List<string> SpellBook = ["Fabien", "Sarah", "Chaise", "Thybault", "Corentin", "Antoine", "Nathan", "Samuel", "Hugo", "Guilhem", "Yvann", "Malaak", "Niels", "Neil", "Gabriel", "Luka", "Victor", "Julien", "Enki", "Quentin", "Waifu", "Tom", "Elliot", "Adam", "Marius"];

            public Bebere()
            {
                Health = 10;
                Damage = 0;
            }
            {
            public override void Spell(Entity target) 
            { 

            }

        }
    }
}