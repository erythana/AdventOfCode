using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using AdventOfCodeLib.Core;
using Avalonia.Controls;
using Avalonia.Data;
using DynamicData.Binding;
using ReactiveUI;

namespace AdventOfCode.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Puzzle _selectedPuzzle;
        private MethodInfo _selectedMethod;
        private string _puzzleInput;
        private string? _puzzleResult;

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
        }

        private void ExecuteRunPuzzleCommand()
        {
            var instance = Activator.CreateInstance(SelectedPuzzle.PuzzleType);
            var input = PuzzleInput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                PuzzleResult = SelectedMethod.Invoke(instance, new object?[]{input})?.ToString();
            }
            catch (Exception e)
            {
                PuzzleResult = e.InnerException?.ToString();
            }
            
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

        #endregion

    }
}
