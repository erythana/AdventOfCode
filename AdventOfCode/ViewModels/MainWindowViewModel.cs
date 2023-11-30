using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AdventOfCodeLib;
using AdventOfCodeLib.Core;
using ReactiveUI;

namespace AdventOfCode.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Puzzle? _selectedPuzzle;
        private MethodInfo? _selectedMethod;
        private TimeSpan? _solveDuration;
        private string _puzzleInput;
        private string _puzzleResult;
        private bool _isManualInput;
        private string _sessionCookie;

        #region Constructor

        public MainWindowViewModel()
        {
            InitializeProperties();
        }

        private void InitializeProperties()
        {
            var canExecuteRunPuzzleCommand = this.WhenAnyValue(
                puzInp => puzInp.PuzzleInput,
                metSel => metSel.SelectedMethod,
                inputType => inputType.IsManualInput,
                sessionCookie => sessionCookie.SessionCookie,
                (puzzleInput, selectedMethod, isManualInput, sessionCookie) =>
                    !isManualInput && !string.IsNullOrWhiteSpace(sessionCookie) || //web
                    isManualInput && selectedMethod is not null && !string.IsNullOrWhiteSpace(puzzleInput)); //manual

            RunPuzzleCommand = ReactiveCommand.CreateFromTask(ExecuteRunPuzzleCommand, canExecuteRunPuzzleCommand);
            RunPuzzleCommand.ThrownExceptions.Subscribe(ex => PuzzleResult = ex.ToString());

            Puzzles = AdventOfCodeLibrary
                .EnumeratePuzzles()
                .OrderByDescending(x => x.Year)
                .ThenByDescending(x => x.Day)
                .ToList();
            SelectedPuzzle = Puzzles.First();
            SelectedMethod = SelectedPuzzle.Methods.First();
            _puzzleInput = string.Empty;
            _puzzleResult = string.Empty;
            _sessionCookie = string.Empty;
        }

        #endregion

        #region Properties

        public IEnumerable<Puzzle> Puzzles { get; private set; }

        public Puzzle SelectedPuzzle
        {
            get => _selectedPuzzle;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedPuzzle, value);
                SelectedMethod = _selectedPuzzle.Methods.First();
            }
        }

        public MethodInfo SelectedMethod
        {
            get => _selectedMethod;
            set => this.RaiseAndSetIfChanged(ref _selectedMethod, value);
        }

        public string PuzzleInput
        {
            get => _puzzleInput;
            set => this.RaiseAndSetIfChanged(ref _puzzleInput, value);
        }

        public string? PuzzleResult
        {
            get => _puzzleResult;
            private set => this.RaiseAndSetIfChanged(ref _puzzleResult, value);
        }

        public TimeSpan? SolveDuration
        {
            get => _solveDuration;
            private set => this.RaiseAndSetIfChanged(ref _solveDuration, value);
        }

        public bool IsManualInput
        {
            get => _isManualInput;
            set => this.RaiseAndSetIfChanged(ref _isManualInput, value);
        }

        public string SessionCookie
        {
            get => _sessionCookie;
            set => this.RaiseAndSetIfChanged(ref _sessionCookie, value);
        }

        #endregion

        #region Commands

        public ReactiveCommand<Unit, Unit> RunPuzzleCommand { get; private set; }

        private async Task ExecuteRunPuzzleCommand()
        {
            var input = await GetPuzzleInput();
            var instance = Activator.CreateInstance(SelectedPuzzle.PuzzleType);
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            try
            {
                var result = SelectedMethod.Invoke(instance, new object?[] { input })?.ToString();
                PuzzleResult = result;
            }
            catch (Exception e)
            {
                PuzzleResult = e.InnerException?.ToString();
            }
            finally
            {
                stopWatch.Stop();
                SolveDuration = stopWatch.Elapsed;
            }
        }

        private async Task<string[]> GetPuzzleInput()
        {
            const string regexSplitPattern = @"\r\n?|\n";
            string[] input;

            if (IsManualInput)
                input = Regex.Split(PuzzleInput, regexSplitPattern);
            else
            {
                var webResolver = new WebInputResolver(SessionCookie);
                var result = await webResolver.GetInputFor(SelectedPuzzle.Year, SelectedPuzzle.Day);
                input = Regex.Split(result, regexSplitPattern);
            }

            return input;
        }

        #endregion
    }
}