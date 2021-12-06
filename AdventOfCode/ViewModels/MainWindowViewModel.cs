using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reflection;
using AdventOfCodeLib.Core;
using AdventOfCodePuzzles.Models;
using BenchmarkDotNet.Running;
using ReactiveUI;

namespace AdventOfCode.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Puzzle _selectedPuzzle;
        private MethodInfo _selectedMethod;
        private string _puzzleInput;
        private string _puzzleResult;

        #region Constructor
        
        public MainWindowViewModel()
        {
            var canExecuteRunPuzzleCommand = this.WhenAnyValue(
                puzInp => puzInp.PuzzleInput,
                metSel => metSel.SelectedMethod,
                (p, m) => !string.IsNullOrEmpty(p) && m is not null);
            RunPuzzleCommand = ReactiveCommand.Create(ExecuteRunPuzzleCommand, canExecuteRunPuzzleCommand);
            
            Puzzles = AdventOfCodeLib.AdventOfCodeLibrary.EnumeratePuzzles().ToList();
            SelectedPuzzle = Puzzles.First();
            SelectedMethod = SelectedPuzzle.Methods.First();
            PuzzleInput = string.Empty;
            PuzzleResult = string.Empty;
        }

        #endregion

        #region Properties

        public IList<Puzzle> Puzzles { get; }

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

        #endregion

        #region Commands
        
        public ReactiveCommand<Unit, Unit> RunPuzzleCommand { get; private set; }
        
        private void ExecuteRunPuzzleCommand()
        {
            
            var input = PuzzleInput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var instance = Activator.CreateInstance(SelectedPuzzle.PuzzleType);
            try
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();
                var result = SelectedMethod.Invoke(instance, new object?[] {input})?.ToString();
                stopWatch.Stop();
                PuzzleResult = $"{result}\nOpereation took {stopWatch.ElapsedMilliseconds}ms";
            }
            catch (Exception e)
            {
                //TODO: ValidationError in Avalonia?
                PuzzleResult = e.InnerException?.ToString();
                
            }
        }

        #endregion

    }
}