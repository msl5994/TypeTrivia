using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypeTrivia.Models;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace TypeTrivia.ViewModels
{
    public class QuestionViewModel
    {
        // Custom Variables
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

        public FormattedString Question { get; set; }
        public String QuestionPart1 { get; set; }
        public String QuestionPart2 { get; set; }
        public ElementType QuestionElement { get; set; }
        public Command SelectAnswerCommand { get; }
        public List<ElementType> Elements { get; set; }
        public ElementType SelectedElement { get; set; }
        public String Answer { get; set; }
        public List<ElementType> PossibleAnswers { get; set; }
        public int NumCorrectAnswers { get; set; }
        public bool GameOver { get; set; }
        public QuestionViewModel()
        {
            // defaults
            NumCorrectAnswers = 0;
            GameOver = false;

            // create list of elements
            Elements = new List<ElementType>();
            PossibleAnswers = new List<ElementType>();
            CreateElements();
            CreateRelationLists();
            // start with the first question
            GenerateNextQuestion();
            SelectAnswerCommand = new Command((object sender) => 
            { 
                Button button = (Button)sender;
                SelectedElement = Elements.Where(e => e.ElementName == button.Text.ToString()).ToList().FirstOrDefault();
                CheckAnswer();
            });
        }

        public void CheckAnswer()
        {
            if(SelectedElement.ElementName == Answer)
            {
                NumCorrectAnswers++;
                // generate next question
                GenerateNextQuestion();
            }
            else
            {
                GameOver = true;
            }
        }

        public void GenerateNextQuestion()
        {
            Random rand = new Random();
            int ElementIndex = rand.Next(Elements.Count);
            QuestionElement = Elements[ElementIndex];
            Question = new FormattedString 
            {
                Spans =
                {
                    new Span { Text = "When battling against ", TextColor = Color.Black},
                    new Span { Text = QuestionElement.ElementName, TextColor = QuestionElement.ElementColor},
                    new Span { Text = " types, which type of the ones below does the most effective damage?", TextColor = Color.Black},
                }
            };
            QuestionPart1 = "When battling against ";
            QuestionPart2 = "Which type of the ones below does the most effective damage?";

            // Select 4 possible answers, 1 of which must be correct
            ElementIndex = rand.Next(QuestionElement.VulnerableTo.Count);
            Answer = QuestionElement.VulnerableTo[ElementIndex];
            ElementType AnswerElement = Elements.Where(e => e.ElementName == Answer).SingleOrDefault();
            AnswerElement.IsInQuestion = true;
            PossibleAnswers.Add(AnswerElement);
            // loop to populate other answers
            while(PossibleAnswers.Count < 4)
            {
                ElementIndex = rand.Next(Elements.Count);
                // skip if already in the list of answers
                if(Elements[ElementIndex].IsInQuestion 
                    || PossibleAnswers.Contains(Elements[ElementIndex]) 
                    || QuestionElement.VulnerableTo.Contains(Elements[ElementIndex].ElementName)) // or if the element is another possible correct answer
                {
                    continue;
                }
                else
                {
                    Elements[ElementIndex].IsInQuestion = true;
                    PossibleAnswers.Add(Elements[ElementIndex]);
                }
            }
        }

        public void CreateElements()
        {
            ElementType NormalElement = new ElementType("Normal", ElementType.ElementID.Normal, Color.Tan);
            Elements.Add(NormalElement);

            ElementType FightingElement = new ElementType("Fighting", ElementType.ElementID.Fighting, Color.SaddleBrown);
            Elements.Add(FightingElement);

            ElementType FlyingElement = new ElementType("Flying", ElementType.ElementID.Flying, Color.LightSlateGray);
            Elements.Add(FlyingElement);

            ElementType PoisonElement = new ElementType("Poison", ElementType.ElementID.Poison, Color.Purple);
            Elements.Add(PoisonElement);

            ElementType GroundElement = new ElementType("Ground", ElementType.ElementID.Ground, Color.Goldenrod);
            Elements.Add(GroundElement);

            ElementType RockElement = new ElementType("Rock", ElementType.ElementID.Rock, Color.SandyBrown);
            Elements.Add(RockElement);

            ElementType BugElement = new ElementType("Bug", ElementType.ElementID.Bug, Color.LightGreen);
            Elements.Add(BugElement);

            ElementType GhostElement = new ElementType("Ghost", ElementType.ElementID.Ghost, Color.MediumPurple);
            Elements.Add(GhostElement);

            ElementType SteelElement = new ElementType("Steel", ElementType.ElementID.Steel, Color.Gray);
            Elements.Add(SteelElement);

            ElementType FireElement = new ElementType("Fire", ElementType.ElementID.Fire, Color.OrangeRed);
            Elements.Add(FireElement);

            ElementType WaterElement = new ElementType("Water", ElementType.ElementID.Water, Color.Aqua);
            Elements.Add(WaterElement);

            ElementType GrassElement = new ElementType("Grass", ElementType.ElementID.Grass, Color.Green);
            Elements.Add(GrassElement);

            ElementType ElectricElement = new ElementType("Electric", ElementType.ElementID.Electric, Color.Yellow);
            Elements.Add(ElectricElement);

            ElementType PsychicElement = new ElementType("Psychic", ElementType.ElementID.Psychic, Color.HotPink);
            Elements.Add(PsychicElement);

            ElementType IceElement = new ElementType("Ice", ElementType.ElementID.Ice, Color.LightBlue);
            Elements.Add(IceElement);

            ElementType DragonElement = new ElementType("Dragon", ElementType.ElementID.Dragon, Color.BlueViolet);
            Elements.Add(DragonElement);

            ElementType DarkElement = new ElementType("Dark", ElementType.ElementID.Dark, Color.DarkGray);
            Elements.Add(DarkElement);

            ElementType FairyElement = new ElementType("Fairy", ElementType.ElementID.Fairy, Color.Pink);
            Elements.Add(FairyElement);
        }
        public void CreateRelationLists()
        {
            foreach(ElementType element in Elements)
            {
                for(int x = 0; x < Elements.Count; x++)
                {
                    if (TypeChartValues[(int)element.ElementIDnum, x] == 2f)
                    {
                        element.StrongAgainst.Add(Elements[x].ElementName);
                    }
                    if (TypeChartValues[(int)element.ElementIDnum, x] <= 0.5f) // include 0x in weak against category
                    {
                        element.WeakAgainst.Add(Elements[x].ElementName);
                    }
                    if (TypeChartValues[x, (int)element.ElementIDnum] == 2f)
                    {
                        element.VulnerableTo.Add(Elements[x].ElementName);
                    }
                    if (TypeChartValues[x, (int)element.ElementIDnum] == 0.5f)
                    {
                        element.ResistantTo.Add(Elements[x].ElementName);
                    }
                    if (TypeChartValues[x, (int)element.ElementIDnum] == 0f)
                    {
                        element.ImmuneTo.Add(Elements[x].ElementName);
                    }
                }
            }
        }
    }
}
