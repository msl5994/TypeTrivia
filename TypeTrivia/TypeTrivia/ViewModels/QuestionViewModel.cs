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
            ElementIndex = rand.Next(Elements.Count);
            Answer = QuestionElement.WeakAgainst[ElementIndex];
            Elements[ElementIndex].IsInQuestion = true;
            PossibleAnswers.Add(Elements[ElementIndex]);
            // loop to populate other answers
            while(PossibleAnswers.Count < 4)
            {
                ElementIndex = rand.Next(Elements.Count);
                // skip if already in the list of answers
                if(Elements[ElementIndex].IsInQuestion || PossibleAnswers.Contains(Elements[ElementIndex]))
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
            #region Normal Type
            ElementType NormalElement = new ElementType()
            {
                ElementName = "Normal",
                ElementIDnum = ElementType.ElementID.Normal,
                ElementColor = Color.Tan,
                StrongAgainst = new List<String>(){},
                WeakAgainst = new List<String>()
                {
                    "Rock",
                    "Steel"
                },
                VulnerableTo = new List<String>()
                {
                    "Fighting"
                },
                ResistantTo = new List<String>(){},
                ImmuneTo = new List<String>()
                {
                    "Ghost"
                },
                IsInQuestion = false
            };
            Elements.Add(NormalElement);
            #endregion

            #region Fighting Type
            ElementType FightingElement = new ElementType()
            {
                ElementName = "Fighting",
                ElementColor = Color.SaddleBrown,
                StrongAgainst = new List<String>()
                {
                    "Normal",
                    "Rock",
                    "Steel",
                    "Ice",
                    "Dark"
                },
                WeakAgainst = new List<String>()
                {
                    "Flying",
                    "Poison",
                    "Bug",
                    "Psychic",
                    "Fairy"
                },
                VulnerableTo = new List<String>()
                {
                    "Flying",
                    "Psychic",
                    "Fairy"
                },
                ResistantTo= new List<String>()
                {
                    "Rock",
                    "Bug",
                    "Dark"
                },
                ImmuneTo = new List<String>(){},
                IsInQuestion = false
            };
            Elements.Add(FightingElement);
            #endregion

            ElementType FlyingElement = new ElementType()
            {
                ElementName = "Flying",
                ElementColor = Color.LightSlateGray,
                StrongAgainst = new List<String>()
                {
                    "Fighting",

                },
                WeakAgainst = new List<String>()
                {

                },
                IsInQuestion = false
            };
            Elements.Add(FlyingElement);

            ElementType PoisonElement = new ElementType()
            {
                ElementName = "Poison",
                ElementColor = Color.Purple,
                StrongAgainst = new List<String>()
                {

                },
                WeakAgainst = new List<String>()
                {

                },
                IsInQuestion = false
            };
            Elements.Add(PoisonElement);

            ElementType GroundElement = new ElementType()
            {
                ElementName = "Ground",
                ElementColor = Color.Goldenrod,
                StrongAgainst = new List<String>()
                {

                },
                WeakAgainst = new List<String>()
                {

                },
                IsInQuestion = false
            };
            Elements.Add(GroundElement);

            ElementType RockElement = new ElementType()
            {
                ElementName = "Rock",
                ElementColor = Color.SandyBrown,
                StrongAgainst = new List<String>()
                {

                },
                WeakAgainst = new List<String>()
                {

                },
                IsInQuestion = false
            };
            Elements.Add(RockElement);

            ElementType BugElement = new ElementType()
            {
                ElementName = "Bug",
                ElementColor = Color.LightGreen,
                StrongAgainst = new List<String>()
                {

                },
                WeakAgainst = new List<String>()
                {

                },
                IsInQuestion = false
            };
            Elements.Add(BugElement);

            ElementType GhostElement = new ElementType()
            {
                ElementName = "Ghost",
                ElementColor = Color.MediumPurple,
                StrongAgainst = new List<String>()
                {

                },
                WeakAgainst = new List<String>()
                {

                },
                IsInQuestion = false
            };
            Elements.Add(GhostElement);

            ElementType SteelElement = new ElementType()
            {
                ElementName = "Steel",
                ElementColor = Color.Gray,
                StrongAgainst = new List<String>()
                {

                },
                WeakAgainst = new List<String>()
                {

                },
                IsInQuestion = false
            };
            Elements.Add(SteelElement);

            ElementType FireElement = new ElementType()
            {
                ElementName = "Fire",
                ElementColor = Color.OrangeRed,
                StrongAgainst = new List<String>()
                {

                },
                WeakAgainst = new List<String>()
                {

                },
                IsInQuestion = false
            };
            Elements.Add(FireElement);

            ElementType WaterElement = new ElementType()
            {
                ElementName = "Water",
                ElementColor = Color.Aqua,
                StrongAgainst = new List<String>()
                {

                },
                WeakAgainst = new List<String>()
                {

                },
                IsInQuestion = false
            };
            Elements.Add(WaterElement);

            ElementType GrassElement = new ElementType()
            {
                ElementName = "Grass",
                ElementColor = Color.Green,
                StrongAgainst = new List<String>()
                {

                },
                WeakAgainst = new List<String>()
                {

                },
                IsInQuestion = false
            };
            Elements.Add(GrassElement);

            ElementType ElectricElement = new ElementType()
            {
                ElementName = "Electric",
                ElementColor = Color.Yellow,
                StrongAgainst = new List<String>()
                {

                },
                WeakAgainst = new List<String>()
                {

                },
                IsInQuestion = false
            };
            Elements.Add(ElectricElement);

            ElementType PsychicElement = new ElementType()
            {
                ElementName = "Psychic",
                ElementColor = Color.HotPink,
                StrongAgainst = new List<String>()
                {

                },
                WeakAgainst = new List<String>()
                {

                },
                IsInQuestion = false
            };
            Elements.Add(PsychicElement);

            ElementType IceElement = new ElementType()
            {
                ElementName = "Ice",
                ElementColor = Color.LightBlue,
                StrongAgainst = new List<String>()
                {

                },
                WeakAgainst = new List<String>()
                {

                },
                IsInQuestion = false
            };
            Elements.Add(IceElement);

            ElementType DragonElement = new ElementType()
            {
                ElementName = "Dragon",
                ElementColor = Color.BlueViolet,
                StrongAgainst = new List<String>()
                {

                },
                WeakAgainst = new List<String>()
                {

                },
                IsInQuestion = false
            };
            Elements.Add(DragonElement);

            ElementType DarkElement = new ElementType()
            {
                ElementName = "Dark",
                ElementColor = Color.DarkGray,
                StrongAgainst = new List<String>()
                {

                },
                WeakAgainst = new List<String>()
                {

                },
                IsInQuestion = false
            };
            Elements.Add(DarkElement);

            ElementType FairyElement = new ElementType()
            {
                ElementName = "Fairy",
                ElementColor = Color.Pink,
                StrongAgainst = new List<String>()
                {

                },
                WeakAgainst = new List<String>()
                {

                },
                IsInQuestion = false
            };
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
