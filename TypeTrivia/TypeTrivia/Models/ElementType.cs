using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TypeTrivia.Models
{
    public class ElementType
    {
        public string ElementName { get; set; }
        public ElementID ElementIDnum { get; set; }
        public List<String> StrongAgainst { get; set; }
        public List<String> WeakAgainst { get; set; }
        public List<String> VulnerableTo { get; set; }
        public List <String> ResistantTo { get; set; }
        public List<String> ImmuneTo { get; set; }
        public Color ElementColor { get; set; }
        public bool IsInQuestion { get; set; }

        /*
        public float[,] TypeChartValues = new float[,]
        {
            { 1f, 1f, 1f, 1f, 1f, 0.5f, 1f, 0f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f}, // Normal
            { 2f, 1f, 0.5f, 0.5f, 1, 2, 0.5f, 0, 2, 1, 1, 1, 1, 0.5f, 2, 1, 2, 0.5f}, // Fighting
            { 1f, 2f, 1f, 1f, 1f, 0.5f, 2f, 1f, 0.5f, 1f, 1f, 2f, 0.5f, 1f, 1f, 1f, 1f, 1f}, // Flying 
            { 1f, 1f, 1f, 0.5f, 0.5f, 0.5f, 1f, 0.5f, 0f, 1f, 1f, 2f, 1f, 1f, 1f, 1f, 1f, 2f}, // Poison 
            { 1f, 1f, 0f, 2f, 1f, 2f, 0.5f, 1f, 2f, 2f, 1f, 0.5f, 2f, 1f, 1f, 1f, 1f, 1f}, // Ground
            { 1f, 0.5f, 2f, 1f, 0.5f, 1f, 2f, 1f, 0.5f, 2f, 1f, 1f, 1f, 1f, 2f, 1f, 1f, 1f}, // Rock
            { 1f, 0.5f, 0.5f, 0.5f, 1f, 1f, 1f, 0.5f, 0.5f, 0.5f, 1f, 2f, 1f, 2f, 1f, 1f, 2f, 0.5f}, // Bug
            { 0f, 1f, 1f, 1f, 1f, 1f, 1f, 2f, 1f, 1f, 1f, 1f, 1f, 2f, 1f, 1f, 0.5f, 1f}, // Ghost
            { 1f, 1f, 1f, 1f, 1f, 2f, 1f, 1f, 0.5f, 0.5f, 0.5f, 1f, 0.5f, 1f, 2f, 1f, 1f, 2f}, // Steel
            { 1f, 1f, 1f, 1f, 1f, 0.5f, 2f, 1f, 2f, 0.5f, 0.5f, 2f, 1f, 1f, 2f, 0.5f, 1f, 1f}, // Fire
            { 1f, 1f, 1f, 1f, 2f, 2f, 1f, 1f, 1f, 2f, 0.5f, 0.5f, 1f, 1f, 1f, 0.5f, 1f, 1f}, // Water
            { 1f, 1f, 0.5f, 0.5f, 2f, 2f, 0.5f, 1f, 0.5f, 0.5f, 2f, 0.5f, 1f, 1f, 1f, 0.5f, 1f ,1f}, // Grass
            { 1f, 1f, 2f, 1f, 0f, 1f, 1f, 1f, 1f, 1f, 2f, 0.5f, 0.5f, 1f, 1f, 0.5f, 1f ,1f}, // Electric
            { 1f, 2f, 1f, 2f, 1f, 1f, 1f, 1f, 0.5f, 1f, 1f, 1f, 1f, 0.5f, 1f, 1f, 0f, 1f}, // Psychic
            { 1f, 1f, 2f, 1f, 2f, 1f, 1f, 1f, 0.5f, 0.5f, 0.5f, 2f, 1f, 1f, 0.5f, 2f, 1f, 1f}, // Ice
            { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f, 1f, 1f, 1f, 1f, 1f, 1f, 2f, 1f, 0f}, // Dragon
            { 1f, 0.5f, 1f, 1f, 1f, 1f, 1f, 2f, 1f, 1f, 1f, 1f, 1f, 2f, 1f, 1f, 0.5f, 0.5f}, // Dark
            { 1f, 2f, 1f, 0.5f, 1f, 1f, 1f, 1f, 0.5f, 0.5f, 1f, 1f, 1f, 1f, 1f, 2f, 2f, 1f}, // Fairy
        };
        */
        public enum ElementID
        {
            Normal,
            Fighting,
            Flying,
            Poison,
            Ground,
            Rock,
            Bug,
            Ghost,
            Steel,
            Fire,
            Water,
            Grass,
            Electric,
            Psychic,
            Ice,
            Dragon,
            Dark,
            Fairy
        }
    }
}
