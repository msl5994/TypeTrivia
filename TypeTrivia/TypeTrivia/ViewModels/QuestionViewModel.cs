using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TypeTrivia.Models;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading;

namespace TypeTrivia.ViewModels
{
    public class QuestionViewModel : INotifyPropertyChanged
    {
        #region properties
        // Custom Matrix
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

        private Question _question;
        public Question DisplayQuestion
        {
            get => _question;
            set
            {
                _question = value;
                OnPropertyChanged("DisplayQuestion");
            }
        }

        private string _questionPart1;
        public String QuestionPart1
        {
            get { return _questionPart1; }
            set
            {
                if (_questionPart1 != value)
                {
                    _questionPart1 = value;
                    OnPropertyChanged("QuestionPart1");
                }
            }
        }
        private string _questionPart2;
        public String QuestionPart2
        {
            get { return _questionPart2; }
            set
            {
                _questionPart2 = value;
                OnPropertyChanged("QuestionPart2");
            }
        }

        private ElementType _questionElement;
        public ElementType QuestionElement 
        {
            get { return _questionElement; } 
            set 
            {
                if (_questionElement != value)
                {
                    _questionElement = value;
                    OnPropertyChanged("QuestionElement");
                }
            }
        }

        private ElementType _questionElement2;
        public ElementType QuestionElement2
        {
            get { return _questionElement2; }
            set
            {
                if (_questionElement2 != value)
                {
                    _questionElement2 = value;
                    OnPropertyChanged("QuestionElement2");
                }
            }
        }

        private List<ElementType> _elements;
        public List<ElementType> Elements
        {
            get { return _elements; }
            set
            {
                _elements = value;
                OnPropertyChanged("Elements");
            }
        }
        public ElementType SelectedElement { get; set; }
        public String Answer { get; set; }
        private List<ElementType> _possibleAnswers;
        public List<ElementType> PossibleAnswers
        {
            get { return _possibleAnswers; }
            set
            {
                _possibleAnswers = value;
                OnPropertyChanged("PossibleAnswers");
            }
        }
        private int _numCorrectAnswers;
        public int NumCorrectAnswers
        {
            get { return _numCorrectAnswers; }
            set
            {
                if (_numCorrectAnswers != value)
                {
                    _numCorrectAnswers = value;
                    OnPropertyChanged("NumCorrectAnswers");
                }
            }
        }
        public int LongestAttackStreak
        {
            get => Preferences.Get(nameof(LongestAttackStreak), 0);
            set
            {
                Preferences.Set(nameof(LongestAttackStreak), value);
                OnPropertyChanged(nameof(LongestAttackStreak));
            }
        }

        public int LongestDefenseStreak
        {
            get => Preferences.Get(nameof(LongestDefenseStreak), 0);
            set
            {
                Preferences.Set(nameof(LongestDefenseStreak), value);
                OnPropertyChanged(nameof(LongestDefenseStreak));
            }
        }
        private bool _gameOver;
        public bool GameOver
        {
            get { return _gameOver; }
            set
            {
                if (_gameOver != value)
                {
                    _gameOver = value;
                    OnPropertyChanged("GameOver");
                }
            }
        }
        private bool _gameRunning;
        public bool GameRunning
        {
            get { return _gameRunning; }
            set
            {
                if (_gameRunning != value)
                {
                    _gameRunning = value;
                    OnPropertyChanged("GameRunning");
                }
            }
        }
        private int _timeDisplay;
        public int TimeDisplay
        { 
            get => _timeDisplay; 
            set
            {
                if (_timeDisplay != value)
                {
                    _timeDisplay = value;
                    OnPropertyChanged("TimeDisplay");
                }
            }
        }
        private const int StartingMaxTime = 30;
        private int _maxTime;
        public int MaxTime
        {
            get => _maxTime;
            set
            {
                if (_maxTime != value)
                {
                    _maxTime = value;
                    OnPropertyChanged("MaxTime");
                }
            }
        }

        private bool _pauseTimer;
        public bool PauseTimer
        {
            get => _pauseTimer;
            set
            {
                if (_pauseTimer != value)
                {
                    _pauseTimer = value;
                    OnPropertyChanged("PauseTimer");
                }
            }
        }
        #endregion

        // Commands
        public Command SelectAnswerCommand { get; }
        public Command TryAgainCommand { get; }

        // Constructor
        public QuestionViewModel()
        {
            MessagingCenter.Subscribe<MainPageViewModel, int>(this, "QuestionType", (sender, arg) =>
            {
                Device.BeginInvokeOnMainThread(() => // might need this to set the type before generating the first question
                {
                    DisplayQuestion.Type = (Question.QuestionType)arg;
                    // start with the first question after the type has been set
                    GenerateNextQuestion();
                });
                MessagingCenter.Unsubscribe<MainPageViewModel, Question>(this, "QuestionType");
            });

            // defaults
            MaxTime = StartingMaxTime;
            TimeDisplay = StartingMaxTime;
            NumCorrectAnswers = 0;
            GameRunning = true;
            GameOver = false;
            PauseTimer = false;
            DisplayQuestion = new Question();

            // create list of elements
            Elements = new List<ElementType>();
            PossibleAnswers = new List<ElementType>();
            CreateElements();
            CreateRelationLists();
            SelectAnswerCommand = new Command((object sender) => 
            { 
                Button button = (Button)sender;
                SelectedElement = Elements.Where(e => e.ElementName == button.Text.ToString()).ToList().FirstOrDefault();
                CheckAnswer();
            });
            TryAgainCommand = new Command(() => 
            {
                ResetGame();
            });
            CreateGameTimer();
        }

        #region Gameplay Methods
        public void CheckAnswer()
        {
            if(SelectedElement.ElementName == Answer)
            {
                // add some kind of fun visual feedback
                // store the original colors
                List<Color>originalColors = new List<Color>();
                foreach(ElementType elementType in PossibleAnswers)
                {
                    Color colorTemp = elementType.ElementColor;
                    originalColors.Add(colorTemp);
                    // set all colors in the answers list to white
                    Elements.Where(e => e.ElementName == elementType.ElementName).FirstOrDefault().ElementColor = Color.LightGray;
                }
                ElementType tempAnswerElement = Elements.Where(e => e.ElementName == SelectedElement.ElementName).SingleOrDefault();
                Color tempColor;
                if(tempAnswerElement != null)
                {
                    tempColor = tempAnswerElement.ElementColor;
                    tempAnswerElement.ElementColor = Color.Green;
                }
                OnPropertyChanged("Elements");
                //Thread.Sleep(1000);
                NumCorrectAnswers++;
                // lower the timer
                if(MaxTime > 3)
                {
                    MaxTime--;
                }
                // reset the display time
                TimeDisplay = MaxTime;
                // generate next question
                CreateAnswerAnimationTimer(originalColors);
                //GenerateNextQuestion();
            }
            else
            {
                EndGame();
            }
        }

        public void EndGame()
        {
            GameRunning = false;
            GameOver = true;
            // update longest streak if neccessary
            if(DisplayQuestion.Type == Question.QuestionType.Attacking)
            {
                if (NumCorrectAnswers > LongestAttackStreak)
                {
                    LongestAttackStreak = NumCorrectAnswers;
                }
            }
            else if(DisplayQuestion.Type == Question.QuestionType.Defending)
            {
                if (NumCorrectAnswers > LongestDefenseStreak)
                {
                    LongestDefenseStreak = NumCorrectAnswers;
                }
            }
            else if (DisplayQuestion.Type == Question.QuestionType.DualType)
            {
                if (NumCorrectAnswers > LongestDefenseStreak)
                {
                    LongestDefenseStreak = NumCorrectAnswers;
                }
            }

            // show them the correct answer from the question they just got wrong
            foreach(ElementType element in PossibleAnswers)
            {
                if(element.ElementName == SelectedElement?.ElementName)
                {
                    Elements.Where(e => e.ElementName == element.ElementName).ToList().SingleOrDefault().ElementColor = Color.FromHex("#E61B23");
                }
                else if(element.ElementName != Answer)
                {
                    Elements.Where(e => e.ElementName == element.ElementName).ToList().SingleOrDefault().ElementColor = Color.FromHex("#F2F2F2");
                }
                else
                {
                    Elements.Where(e => e.ElementName == element.ElementName).ToList().SingleOrDefault().ElementColor = Color.FromHex("#44A063");
                }
                OnPropertyChanged("Elements");
            }
        }

        public void ResetGame()
        {
            // reset streak, list of answers, and elements statuses
            NumCorrectAnswers = 0;
            GameOver = false;
            GameRunning = true;
            ResetAnswersList();
            // re-create element list and connections from base to reset colors
            Elements.Clear();
            CreateElements();
            CreateRelationLists();
            // create the first question
            GenerateNextQuestion();
            // restart the timer
            MaxTime = StartingMaxTime;
            TimeDisplay = StartingMaxTime;
            // start a timer for the page
            CreateGameTimer();
        }

        public void CreateGameTimer()
        {
            // start a timer for the page
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (!PauseTimer)
                {
                    // update the display timer every second
                    TimeDisplay--;
                }
                // if the display time every hits zero or drops below, end the game
                if (TimeDisplay <= 0)
                {
                    EndGame();
                    return false;
                }
                if (GameOver) // if a wrong answer was selected, stop the timer
                {
                    return false;
                }
                return true; // true to repeat the timer, false to stop
            });
        }

        public void CreateAnswerAnimationTimer(List<Color> colors)
        {
            PauseTimer = true;
            int animationTime = 3;
            Device.StartTimer(TimeSpan.FromSeconds(1), () => 
            {
                animationTime--;
                if(animationTime <= 0)
                {
                    // give the animation a second to play before setting up the next question
                    ReinstateOriginalColors(colors);
                    return false;
                }
                return true;
            });
        }

        public void ReinstateOriginalColors(List<Color> colors)
        {
            for(int x = 0; x < PossibleAnswers.Count; x++)
            {
                ElementType temp = Elements.Where(e => e.ElementName == PossibleAnswers[x].ElementName).FirstOrDefault();
                if(temp != null)
                {
                    Elements.Where(e => e.ElementName == PossibleAnswers[x].ElementName).FirstOrDefault().ElementColor = colors[x];
                }
            }
            OnPropertyChanged("Elements");
            // after colors are restored generate the next question
            GenerateNextQuestion();
            PauseTimer = false;
        }

        public void ResetAnswersList()
        {
            // reset the list of possible answers
            if (PossibleAnswers.Count > 0)
            {
                foreach (ElementType answer in PossibleAnswers)
                {
                    // reset the visual of each button to be false
                    Elements.Where(e => e.ElementName == answer.ElementName).ToList().FirstOrDefault().IsInQuestion = false;
                }
                PossibleAnswers.Clear();
                OnPropertyChanged("PossibleAnswers");
                OnPropertyChanged("Elements");
            }
        }

        public void GenerateNextQuestion()
        {
            // reset the list of possible answers
            ResetAnswersList();

            Random rand = new Random();
            int ElementIndex = rand.Next(Elements.Count);
            QuestionElement = Elements[ElementIndex];
            ElementType AnswerElement = new ElementType() { 
                ElementName = ElementType.ElementID.Fire.ToString(), 
                ElementIDnum = ElementType.ElementID.Fire, 
                ElementColor = Color.Gray 
            };

            // Select 4 possible answers, 1 of which must be correct based on the question type
            switch (DisplayQuestion.Type)
            {
                case Question.QuestionType.Attacking:
                    QuestionPart1 = "When ATTACKING against ";
                    QuestionPart2 = "Which type of the ones below does the most effective damage?";
                    // choose the answer from the vulnerable to element list
                    ElementIndex = rand.Next(QuestionElement.VulnerableTo.Count);
                    Answer = QuestionElement.VulnerableTo[ElementIndex];
                    AnswerElement = Elements.Where(e => e.ElementName == Answer).SingleOrDefault();
                    AnswerElement.IsInQuestion = true;
                    PossibleAnswers.Add(AnswerElement);
                    // loop to populate other answers
                    while (PossibleAnswers.Count < 4)
                    {
                        ElementIndex = rand.Next(Elements.Count);
                        // skip if already in the list of answers
                        if (PossibleAnswers.Contains(Elements[ElementIndex])
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
                    break;
                case Question.QuestionType.Defending:
                    QuestionPart1 = "When DEFENDING against ";
                    QuestionPart2 = "Which type of the ones below resists the type of incoming damage?";
                    // choose the answer from the resistant to element list
                    ElementIndex = rand.Next(QuestionElement.WeakAgainst.Count);
                    Answer = QuestionElement.WeakAgainst[ElementIndex];
                    AnswerElement = Elements.Where(e => e.ElementName == Answer).SingleOrDefault();
                    AnswerElement.IsInQuestion = true;
                    PossibleAnswers.Add(AnswerElement);
                    // loop to populate other answers
                    while (PossibleAnswers.Count < 4)
                    {
                        ElementIndex = rand.Next(Elements.Count);
                        // skip if already in the list of answers
                        if (PossibleAnswers.Contains(Elements[ElementIndex])
                            || QuestionElement.WeakAgainst.Contains(Elements[ElementIndex].ElementName)) // or if the element is another possible correct answer
                        {
                            continue;
                        }
                        else
                        {
                            Elements[ElementIndex].IsInQuestion = true;
                            PossibleAnswers.Add(Elements[ElementIndex]);
                        }
                    }
                    break;
                case Question.QuestionType.DualType:
                    QuestionPart1 = "When ATTACKING against ";
                    QuestionPart2 = "Which type of the ones below does the most effective damage?";
                    // choose the answer from the vulnerable to element list
                    //ElementIndex = rand.Next(QuestionElement.VulnerableTo.Count);
                    int Element2Index = ElementIndex;
                    while(ElementIndex == Element2Index)
                    {
                        Element2Index = rand.Next(Elements.Count);
                    }
                    QuestionElement2 = Elements[Element2Index];
                    //Element2Index = rand.Next(QuestionElement2.VulnerableTo.Count);                    
                    // precalc
                    float highestResult = 0.0f;
                    int highestResultIndex = -1;
                    // loop through element options
                    for(int i = 0; i < Elements.Count -1; i++)
                    {
                        float val1 = TypeChartValues[i, ElementIndex];
                        float val2 = TypeChartValues[i, Element2Index];
                        float result = val1 * val2;
                        if(result > highestResult)
                        {
                            highestResult = result;
                            highestResultIndex = i;
                        }
                    }
                    AnswerElement = Elements[highestResultIndex];
                    AnswerElement.IsInQuestion = true;
                    Answer = AnswerElement.ElementName;
                    PossibleAnswers.Add(AnswerElement);
                    // loop to populate other answers
                    while (PossibleAnswers.Count < 4)
                    {
                        int PossibleElementIndex = rand.Next(Elements.Count);
                        // check for duplicate correct answers via values
                        float val1 = TypeChartValues[PossibleElementIndex, ElementIndex];
                        float val2 = TypeChartValues[PossibleElementIndex, Element2Index];
                        float result = val1 * val2;
                        // skip if already in the list of answers
                        if (PossibleAnswers.Contains(Elements[PossibleElementIndex]))
                        {
                            continue;
                        }
                        // skip if better answer than previous or tied with it
                        else if (result >= highestResult)
                        {
                            continue;
                        }
                        else
                        {
                            Elements[PossibleElementIndex].IsInQuestion = true;
                            PossibleAnswers.Add(Elements[PossibleElementIndex]);
                        }
                    }
                    break;
            }
            OnPropertyChanged(nameof(QuestionPart2));
            OnPropertyChanged("PossibleAnswers");
            OnPropertyChanged("Elements");
        }

        public void CreateElements()
        {
            ElementType NormalElement = new ElementType("Normal", ElementType.ElementID.Normal, Color.FromHex("#B4966D"));
            Elements.Add(NormalElement);

            ElementType FightingElement = new ElementType("Fighting", ElementType.ElementID.Fighting, Color.FromHex("#FF6468"));
            Elements.Add(FightingElement);

            ElementType FlyingElement = new ElementType("Flying", ElementType.ElementID.Flying, Color.FromHex("#828CC7"));
            Elements.Add(FlyingElement);

            ElementType PoisonElement = new ElementType("Poison", ElementType.ElementID.Poison, Color.FromHex("#B0669F"));
            Elements.Add(PoisonElement);

            ElementType GroundElement = new ElementType("Ground", ElementType.ElementID.Ground, Color.FromHex("#E2B666"));
            Elements.Add(GroundElement);

            ElementType RockElement = new ElementType("Rock", ElementType.ElementID.Rock, Color.FromHex("#AAA063"));
            Elements.Add(RockElement);

            ElementType BugElement = new ElementType("Bug", ElementType.ElementID.Bug, Color.FromHex("#97AC3A"));
            Elements.Add(BugElement);

            ElementType GhostElement = new ElementType("Ghost", ElementType.ElementID.Ghost, Color.FromHex("#826F97"));
            Elements.Add(GhostElement);

            ElementType SteelElement = new ElementType("Steel", ElementType.ElementID.Steel, Color.FromHex("#8BB4BE"));
            Elements.Add(SteelElement);

            ElementType FireElement = new ElementType("Fire", ElementType.ElementID.Fire, Color.FromHex("#FB794F"));
            Elements.Add(FireElement);

            ElementType WaterElement = new ElementType("Water", ElementType.ElementID.Water, Color.FromHex("#4FC8DB"));
            Elements.Add(WaterElement);

            ElementType GrassElement = new ElementType("Grass", ElementType.ElementID.Grass, Color.FromHex("#79CB5D"));
            Elements.Add(GrassElement);

            ElementType ElectricElement = new ElementType("Electric", ElementType.ElementID.Electric, Color.FromHex("#EFCE12"));
            Elements.Add(ElectricElement);

            ElementType PsychicElement = new ElementType("Psychic", ElementType.ElementID.Psychic, Color.FromHex("#FF427B"));
            Elements.Add(PsychicElement);

            ElementType IceElement = new ElementType("Ice", ElementType.ElementID.Ice, Color.FromHex("#6DDCD2"));
            Elements.Add(IceElement);

            ElementType DragonElement = new ElementType("Dragon", ElementType.ElementID.Dragon, Color.FromHex("#5A63B0"));
            Elements.Add(DragonElement);

            ElementType DarkElement = new ElementType("Dark", ElementType.ElementID.Dark, Color.FromHex("#5A504F"));
            Elements.Add(DarkElement);

            ElementType FairyElement = new ElementType("Fairy", ElementType.ElementID.Fairy, Color.FromHex("#FE77AE"));
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
        #endregion

        // INotifyPropertyChanged Event Handler and Method
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
